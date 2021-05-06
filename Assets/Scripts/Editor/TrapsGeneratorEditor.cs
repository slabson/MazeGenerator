using UnityEngine;
using UnityEditor;

namespace MazeGenerator
{
    [CustomEditor(typeof(TrapsGenerator))]
    public class TrapsGeneratorEditor : Editor
    {
        private TrapsGenerator script;

        private void OnEnable()
        {
            script = target as TrapsGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate"))
            {
                script.Start();
            }

            if (GUILayout.Button("Clear"))
            {
                script.Clear();
            }
        }
    }
}