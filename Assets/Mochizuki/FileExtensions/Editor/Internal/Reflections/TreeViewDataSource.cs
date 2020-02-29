/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public class TreeViewDataSource : ReflectionClass
    {
        public TreeViewDataSource(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.IMGUI.Controls.TreeViewDataSource")) { }

        public int GetRow(int instanceId)
        {
            return InvokeMethod<int>("GetRow", BindingFlags.Instance | BindingFlags.Public, instanceId);
        }
    }
}