using System.Reflection;

using Mochizuki.Editor.Internal.Reflections.Expressions;

using UnityEditor;

namespace Mochizuki.Editor.Internal.Reflections
{
    public class ProjectBrowser : ReflectionClass
    {
        public TreeViewController AssetTree
        {
            get
            {
                var controller = InvokeMember<object>("m_AssetTree");
                return new TreeViewController(controller);
            }
        }

        public TreeViewState AssetTreeState
        {
            get
            {
                var state = InvokeMember<UnityEditor.IMGUI.Controls.TreeViewState>("m_AssetTreeState");
                return new TreeViewState(state);
            }
        }

        public ObjectListArea ListArea
        {
            get
            {
                var area = InvokeMember<object>("m_ListArea");
                return new ObjectListArea(area);
            }
        }

        public ObjectListAreaState ListAreaState
        {
            get
            {
                var state = InvokeMember<object>("m_ListAreaState");
                return new ObjectListAreaState(state);
            }
        }

        public static ProjectBrowser Instance
        {
            get
            {
                var browser = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
                return new ProjectBrowser(EditorWindow.GetWindow(browser, false, null, false));
            }
        }

        private ProjectBrowser(EditorWindow editor) : base(editor, typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ProjectBrowser")) { }

        public bool IsTwoColumns()
        {
            return InvokeMethod<bool>("IsTwoColumns", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}