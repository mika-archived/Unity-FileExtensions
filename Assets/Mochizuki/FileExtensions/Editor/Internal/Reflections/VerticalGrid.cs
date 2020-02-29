/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

using UnityEngine;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public class VerticalGrid : ReflectionClass
    {
        public VerticalGrid(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.VerticalGrid")) { }

        public Rect CalcRect(int itemIdx, float yOffset)
        {
            return InvokeMethod<Rect>("CalcRect", BindingFlags.Instance | BindingFlags.Public, itemIdx, yOffset);
        }
    }
}