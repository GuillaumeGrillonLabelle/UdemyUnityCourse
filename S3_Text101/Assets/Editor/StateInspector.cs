using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(State))]
public class StateInspector : PropertyDrawer
{
	private static float lineHeight = 16;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		if (property.isExpanded)
		{
			GUILayout.BeginVertical();
			var propRect = new Rect(position.x, position.y, position.width, lineHeight);
			EditorGUILayout.PropertyField(property.FindPropertyRelative("state"));

			propRect.y += propRect.height;
			EditorGUI.PropertyField(propRect, property.FindPropertyRelative("blurb"));

			propRect.y += propRect.height;
			EditorGUI.PropertyField(propRect, property.FindPropertyRelative("commands"));

			GUILayout.EndVertical();

			position.height = propRect.y + propRect.height;
		}
		EditorGUI.EndProperty();


		/*
		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("state"), new GUIContent("poop"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("blurb"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("commands"));

		EditorList.Show(serializedObject.FindProperty("transitions"), EditorListOption.All);

		serializedObject.ApplyModifiedProperties();
		*/
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) +
			(property.isExpanded ? 4 * lineHeight : 0);
	}
}
