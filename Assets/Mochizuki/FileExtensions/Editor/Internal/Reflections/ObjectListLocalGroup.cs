/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using System.Reflection;

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

using UnityEngine;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public class ObjectListLocalGroup : ReflectionClass
    {
        public ObjectListLocalGroup(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ObjectListArea+LocalGroup")) { }

        public int IndexOf(int instanceId)
        {
            return InvokeMethod<int>("IndexOf", BindingFlags.Instance | BindingFlags.Public, instanceId);
        }

        public Rect CalcRect(int index)
        {
            var grid = new VerticalGrid(InvokeMember<object>("m_Grid"));
            return grid.CalcRect(index, 0);
        }
    }
}