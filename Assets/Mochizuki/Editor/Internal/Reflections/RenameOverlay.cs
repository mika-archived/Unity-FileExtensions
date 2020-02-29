using System.Reflection;

using Mochizuki.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.Editor.Internal.Reflections
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