using UnityEditor;

[CustomEditor(typeof(TextController))]
public class TextControllerInspector : Editor
{
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("textElement"));

		EditorList.Show(serializedObject.FindProperty("listOfStates"), EditorListOption.All);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("initialState"));

		serializedObject.ApplyModifiedProperties();
	}
}
