using UnityEngine;
using UnityEditor;

namespace MazeGenerator
{
    [CustomEditor(typeof(MazeGenerator))]
    public class MazeGeneratorEditor : Editor
    {
        private MazeGenerator script;

        private void OnEnable()
        {
            script = target as MazeGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate"))
            {
                script.Clear();
                script.Start();
            }

            if (GUILayout.Button("Clear"))
            {
                script.Clear();
            }
        }
    }
}