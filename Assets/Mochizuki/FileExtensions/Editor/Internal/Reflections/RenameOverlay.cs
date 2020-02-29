/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    internal class RenameOverlay : ReflectionClass
    {
        public int UserData
        {
            get { return InvokeMember<int>("userData"); }
        }

        public RenameOverlay(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.RenameOverlay")) { }

        public bool IsRenaming()
        {
            return InvokeMethod<bool>("IsRenaming", BindingFlags.Instance | BindingFlags.Public);
        }
    }
}