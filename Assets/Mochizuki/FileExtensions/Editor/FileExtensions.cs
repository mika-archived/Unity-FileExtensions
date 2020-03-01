/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;

using Mochizuki.FileExtensions.Editor.Internal.Reflections;

using UnityEditor;

using UnityEngine;

#if UNITY_2017

using Mochizuki.FileExtensions.Editor.Internal.Extensions;

#endif

using Object = UnityEngine.Object;

namespace Mochizuki.FileExtensions.Editor
{
    [InitializeOnLoad]
    public static class FileExtensions
    {
        private static GUIStyle _activeStyle;
        private static GUIStyle _normalStyle;
        private static ProjectBrowser _browser;

        static FileExtensions()
        {
            // ReSharper disable once DelegateSubtraction
            EditorApplication.projectWindowItemOnGUI -= OnProjectWindowItemOnGui;
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGui;
        }

        private static void OnProjectWindowItemOnGui(string guid, Rect selectionRect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
#if UNITY_2018_1_OR_NEWER
            if (string.IsNullOrWhiteSpace(path))
#else
            if (path.IsNullOrWhitespace())
#endif
                return;

            var extension = Path.GetExtension(path);
#if UNITY_2018_1_OR_NEWER
            if (string.IsNullOrWhiteSpace(extension))
#else
            if (extension.IsNullOrWhitespace())
#endif
                return;

#if UNITY_2018_1_OR_NEWER
            var package = Packages.GetForAssetPath(path);
            if (package != null && package.assetPath == path)
                return;
#endif

            var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            var instanceId = asset == null ? int.MinValue : asset.GetInstanceID();

            // Maybe ScriptableObject?
            if (asset == null)
            {
                var obj = Selection.assetGUIDs.Select((w, i) => new { GUID = w, Index = i }).FirstOrDefault(w => w.GUID == guid);
                if (obj != null)
                    instanceId = Selection.instanceIDs[obj.Index];
            }

            if (_browser == null) _browser = ProjectBrowser.Instance;
            if (!_browser.IsAlive()) _browser = ProjectBrowser.Instance;

            if (_browser.IsTwoColumns())
                ShowExtensionInTwoColumn(selectionRect, path, extension, instanceId);
            else
                ShowExtensionInOneColumn(selectionRect, path, extension, instanceId);
        }

        private static void ShowExtensionInOneColumn(Rect rect, string path, string extension, int instanceId)
        {
            if (_browser.AssetTreeState.IsRenaming(instanceId))
                return;

            if (_browser.AssetTreeState.ExpandedIds.Contains(instanceId))
            {
                var controller = _browser.AssetTree;
                var row = controller.GetRowIndex(instanceId);
                var rectForRow = controller.GetRectForRows(row, row, rect.width);

                if (Math.Abs(rect.y - rectForRow.y) > 0)
                    return;
            }

            var filename = Path.GetFileNameWithoutExtension(path);
            var label = EditorStyles.label;
            var vector = label.CalcSize(new GUIContent(filename));

            rect.x += vector.x + 14;
            rect.y += 2;

            ShowLabel(rect, extension, _browser.AssetTreeState.SelectedIds.Contains(instanceId));
        }

        private static void ShowExtensionInTwoColumn(Rect rect, string path, string extension, int instanceId)
        {
            var isMultiLine = rect.height > 20;
            if (isMultiLine)
                return;

            if (_browser.ListAreaState.IsRenaming(instanceId))
                return;

            if (_browser.ListAreaState.ExpandedInstanceIds.Contains(instanceId))
            {
                var controller = _browser.ListArea;
                var index = controller.LocalAssets.IndexOf(instanceId);
                var rectForGrid = controller.LocalAssets.CalcRect(index);

                if (Math.Abs(rect.y - rectForGrid.y) > 0)
                    return;
            }

            var filename = Path.GetFileNameWithoutExtension(path);
            var label = EditorStyles.label;
            var vector = label.CalcSize(new GUIContent(filename));

            rect.x += vector.x + 16;
            rect.y += 2;

            ShowLabel(rect, extension, _browser.ListAreaState.SelectedInstanceIds.Contains(instanceId));
        }

        private static void ShowLabel(Rect rect, string extension, bool isActive)
        {
            if (_activeStyle == null)
                _activeStyle = new GUIStyle { normal = new GUIStyleState { textColor = Color.white } };
            if (_normalStyle == null)
                _normalStyle = new GUIStyle { normal = new GUIStyleState { textColor = Color.black } };

            GUI.Label(rect, extension, isActive ? _activeStyle : _normalStyle);
        }
    }
}