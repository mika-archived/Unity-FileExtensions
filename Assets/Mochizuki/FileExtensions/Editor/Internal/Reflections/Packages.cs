using System;
using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

using UnityEditor.PackageManager;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public static class Packages
    {
        private static readonly Type PackageClass = typeof(PackageInfo).Assembly.GetType("UnityEditor.PackageManager.Packages");

        public static PackageInfo GetForAssetPath(string assetPath)
        {
            return ReflectionStaticClass.InvokeMethod<PackageInfo>(PackageClass, "GetForAssetPath", BindingFlags.Public, assetPath);
        }
    }
}