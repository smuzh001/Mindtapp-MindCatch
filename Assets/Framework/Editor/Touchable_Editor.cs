using UnityEditor;

namespace UnityEngine.UI.Extensions
{
    // Touchable_Editor component, to prevent treating the component as a Text object.
    [CustomEditor(typeof(Touchable))]
    public class Touchable_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Do nothing
        }
    }
}