using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Transition))]
public class TransitionPropertyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) + 0;
	}

	public override void OnGUI(Rect position, SerializedProperty transition, GUIContent label)
	{

		EditorGUI.BeginProperty(position, label, transition);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		var keyRect = new Rect(position.x + position.width * 0.46f,
								position.y,
								position.width * 0.2f - 5,
								position.height);
		var stateLabelRect = new Rect(position.x + position.width * 0.66f,
								position.y,
								position.width * 0.05f,
								position.height);
		var stateRect = new Rect(position.x + position.width * 0.71f,
								position.y,
								position.width * 0.29f,
								position.height);

		var key = transition.FindPropertyRelative("key");
		var state = transition.FindPropertyRelative("state");

		EditorGUI.PropertyField(keyRect, key, GUIContent.none);

		EditorGUI.LabelField(stateLabelRect, "=>");
		EditorGUI.PropertyField(stateRect, state, GUIContent.none);

		/*
		EditorGUI.DrawRect(keyLabelRect, new Color(1, 0, 0, .5f));
		EditorGUI.DrawRect(keyRect, new Color(0, 1, 0, .5f));
		EditorGUI.DrawRect(stateLabelRect, new Color(0, 0, 1, .5f));
		EditorGUI.DrawRect(stateRect, new Color(1, 1, 0, .5f));
		*/

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}
}
