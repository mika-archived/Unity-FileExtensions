using Mochizuki.Editor.Internal.Reflections.Expressions;

namespace Mochizuki.Editor.Internal.Reflections
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