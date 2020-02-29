/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class Deployment : MonoBehaviour
{
    [MenuItem("Mochizuki/FileExtensions/Tools/Export as UnityPackage")]
    public static void ExportAsUnityPackage()
    {
        var path = Path.Combine(Application.dataPath, "Mochizuki");
        var assets = Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                              .Where(w => Path.GetExtension(w) == ".cs" && !w.EndsWith("Deployment.cs"))
                              .Select(w => "Assets" + w.Replace(Application.dataPath, "").Replace("\\", "/"))
                              .ToArray();

        if (!Directory.Exists(Path.Combine(Application.dataPath, "Mochizuki/Packages")))
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "Mochizuki/Packages"));

        AssetDatabase.ExportPackage(assets, "./Assets/Mochizuki/Packages/FileExtensions.unitypackage", ExportPackageOptions.Default);
    }
}