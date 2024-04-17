using UnityEditor;

[CustomEditor(typeof(ObjectDestructionTracker))]
public class ObjectDestructionTrackerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        ObjectDestructionTracker objectDestructionTracker = target as ObjectDestructionTracker;
        objectDestructionTracker.trackSpecificObjects = EditorGUILayout.Toggle("Track Specific Objects", objectDestructionTracker.trackSpecificObjects);

        EditorGUI.indentLevel++;

        EditorGUILayout.Space();

        if (objectDestructionTracker.trackSpecificObjects)
        {
            // "Track specific objects" checkbox is enabled
            using (var group = new EditorGUILayout.FadeGroupScope(1))
            {
                SerializedProperty objectsToTrack = serializedObject.FindProperty("objectsToTrack");
                EditorGUILayout.PropertyField(objectsToTrack, true);
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel--;
            return;
        }

        // "Track specific objects" checkbox is disabled
        SerializedProperty objectParent = serializedObject.FindProperty("objectParent");
        EditorGUILayout.PropertyField(objectParent, true);
        serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel--;

    }
}