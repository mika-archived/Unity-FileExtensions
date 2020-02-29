using System.Reflection;

using Mochizuki.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.Editor.Internal.Reflections
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