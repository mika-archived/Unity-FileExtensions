using System.Reflection;

using Mochizuki.Editor.Internal.Reflections.Expressions;

using UnityEngine;

namespace Mochizuki.Editor.Internal.Reflections
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