/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

using UnityEngine;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public class TreeViewController : ReflectionClass
    {
        public TreeViewController(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.IMGUI.Controls.TreeViewController")) { }

        public Rect GetRectForRows(int startRow, int endRow, float rowWidth)
        {
            return InvokeMethod<Rect>("GetRectForRows", BindingFlags.Instance | BindingFlags.NonPublic, startRow, endRow, rowWidth);
        }

        public int GetRowIndex(int id)
        {
            var data = new TreeViewDataSource(InvokeMember<object>("data"));
            return data.GetRow(id);
        }
    }
}