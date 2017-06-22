using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[Flags]
public enum EditorListOption
{
	None = 0,
	ListSize = 1,
	ListLabel = 2,
	ElementLabels = 4,
	Buttons = 8,
	Default = ListSize | ListLabel | ElementLabels,
	NoElementLabels = ListSize | ListLabel,
	All = Default | Buttons
}

public static class EditorList
{
	public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default, string showElementLabelField = null)
	{
		if (!list.isArray)
		{
			EditorGUILayout.HelpBox(list.name + "(" + list.type + ") is neither an array nor a list!", MessageType.Error);
			return;
		}

		bool showListLabel = (options & EditorListOption.ListLabel) != 0;
		bool showListSize = (options & EditorListOption.ListSize) != 0;
		bool showButtons = (options & EditorListOption.Buttons) != 0;

		if (showListLabel)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(list);

			if (showButtons && GUILayout.Button(addButtonContent, EditorStyles.miniButton, miniButtonWidth))
			{
				list.arraySize += 1;
			}
			EditorGUILayout.EndHorizontal();

			EditorGUI.indentLevel += 1;
		}
		if (!showListLabel || list.isExpanded)
		{
			if (showListSize)
			{
				EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
			}
			ShowElements(list, options, showElementLabelField);
		}
		if (showListLabel)
		{
			EditorGUI.indentLevel -= 1;
		}
	}
	private static GUIContent
		moveUpButtonContent = new GUIContent("\u2191", "Move up"),
		moveDownButtonContent = new GUIContent("\u2193", "Move down"),
		duplicateButtonContent = new GUIContent("+", "Duplicate"),
		deleteButtonContent = new GUIContent("-", "Delete"),
		addButtonContent = new GUIContent("+", "Add element");

	private static void ShowElements(SerializedProperty list, EditorListOption options, string showElementLabelField)
	{
		bool showListLabel = (options & EditorListOption.ListLabel) != 0;
		bool showElementLabels = (options & EditorListOption.ElementLabels) != 0;
		bool showButtons = (options & EditorListOption.Buttons) != 0;

		for (int i = 0; i < list.arraySize; i++)
		{
			if (showButtons)
			{
				EditorGUILayout.BeginHorizontal();
			}
			if (showElementLabels)
			{
				var item = list.GetArrayElementAtIndex(i);
				string label = null;
				if (showElementLabelField != null)
				{
					label = PropertyValueToString(item, showElementLabelField);
				}
				else
				{
					var obj = item.serializedObject.targetObject;
					var path = item.propertyPath.Replace(".Array.data[", "[");
					label = GetArrayElementNamePropertyValue(obj, path);
				}

				if (label != null)
				{
					EditorGUILayout.PropertyField(item, new GUIContent(label), true);
				}
				else
				{
					EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), true);
				}
			}
			else
			{
				EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none, true);
			}

			if (showButtons)
			{
				ShowButtons(list, i);
				EditorGUILayout.EndHorizontal();
			}
		}

		if (showButtons && !showListLabel && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
		{
			list.arraySize += 1;
		}
	}

	private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);


	private static void ShowButtons(SerializedProperty list, int index)
	{
		if (GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
		{
			list.MoveArrayElement(index, index - 1);
		}
		if (GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
		{
			list.MoveArrayElement(index, index + 1);
		}
		if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
		{
			list.InsertArrayElementAtIndex(index);
		}
		if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
		{
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize)
			{
				list.DeleteArrayElementAtIndex(index);
			}
		}
	}


	private static string PropertyValueToString(SerializedProperty item, string propertyName)
	{
		var prop = item.FindPropertyRelative(propertyName);
		if (prop == null)
		{
			EditorGUILayout.HelpBox("The property " + propertyName + " does not exist", MessageType.Error);
			return null;
		}

		switch (prop.propertyType)
		{
			case SerializedPropertyType.Generic:
				break;
			case SerializedPropertyType.Integer:
				return prop.intValue.ToString();
			case SerializedPropertyType.Boolean:
				return prop.boolValue.ToString();
			case SerializedPropertyType.Float:
				return prop.floatValue.ToString();
			case SerializedPropertyType.String:
				return prop.stringValue;
			case SerializedPropertyType.Color:
				break;
			case SerializedPropertyType.ObjectReference:
				break;
			case SerializedPropertyType.LayerMask:
				return prop.stringValue;
			case SerializedPropertyType.Enum:
				return prop.enumDisplayNames[prop.enumValueIndex];
			case SerializedPropertyType.Vector2:
				break;
			case SerializedPropertyType.Vector3:
				break;
			case SerializedPropertyType.Vector4:
				break;
			case SerializedPropertyType.Rect:
				break;
			case SerializedPropertyType.ArraySize:
				break;
			case SerializedPropertyType.Character:
				break;
			case SerializedPropertyType.AnimationCurve:
				break;
			case SerializedPropertyType.Bounds:
				break;
			case SerializedPropertyType.Gradient:
				break;
			case SerializedPropertyType.Quaternion:
				break;
			case SerializedPropertyType.ExposedReference:
				break;
		}

		EditorGUILayout.HelpBox("The property " + propertyName + " is of type " + prop.type + " and is not supported", MessageType.Error);
		return null;
	}

	private static string GetArrayElementNamePropertyValue(object obj, string path)
	{
		var elements = path.Split('.');
		foreach (var element in elements)
		{
			if (element.Contains("["))
			{
				var elementName = element.Substring(0, element.IndexOf("["));
				var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
				obj = GetValue(obj, elementName, index);
			}
			else
			{
				obj = GetValue(obj, element);
			}
		}

		var prop = obj.GetType().GetProperty("ArrayElementName");
		if (prop != null)
		{
			var val = prop.GetValue(obj, null);

			return val.ToString();
		}

		return null;
	}

	public static object GetValue(object source, string name)
	{
		if (source == null)
			return null;
		var type = source.GetType();
		var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		if (f == null)
		{
			var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (p == null)
				return null;
			return p.GetValue(source, null);
		}
		return f.GetValue(source);
	}

	public static object GetValue(object source, string name, int index)
	{
		var enumerable = GetValue(source, name) as IEnumerable;
		var enm = enumerable.GetEnumerator();
		while (index-- >= 0)
			enm.MoveNext();
		if (index == -1)
			return null;
		return enm.Current;
	}
}
