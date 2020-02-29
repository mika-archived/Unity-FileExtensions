/*-------------------------------------------------------------------------------------------
 * Copyright (c) Fuyuno Mikazuki / Natsuneko. All rights reserved.
 * Licensed under the MIT License. See LICENSE in the project root for license information.
 *------------------------------------------------------------------------------------------*/

using Mochizuki.FileExtensions.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.FileExtensions.Editor.Internal.Reflections
{
    public class ObjectListArea : ReflectionClass
    {
        public ObjectListLocalGroup LocalAssets
        {
            get
            {
                var localAssets = InvokeMember<object>("m_LocalAssets");
                return new ObjectListLocalGroup(localAssets);
            }
        }

        public ObjectListArea(object instance) : base(instance, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ObjectListArea")) { }
    }
}