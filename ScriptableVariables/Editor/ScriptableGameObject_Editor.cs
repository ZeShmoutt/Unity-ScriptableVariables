using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableGameObject))]
	public class ScriptableGameObject_Editor : ScriptableVariable_Editor<GameObject>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableGameObject))]
	public class ScriptableGameObject_Drawer : ScriptableVariable_Drawer<GameObject>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableGameObject scriptableObject = (ScriptableGameObject)property.objectReferenceValue;
			SerializedObject ser = new SerializedObject(scriptableObject);
			SerializedProperty prop = ser.FindProperty("value");

			if (scriptableObject != null)
			{
				if (scriptableObject.AllowExternalControl)
				{
					EditorGUI.PropertyField(position, prop, GUIContent.none);
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.PropertyField(position, prop, GUIContent.none);
					GUI.enabled = true;
				}
			}
			else
			{
				GUI.enabled = false;
				Rect iconRect = new Rect(position.x, position.y, position.height, position.height);
				Rect labelRect = new Rect(position.x + position.height, position.y, position.width - position.height, position.height);
				EditorGUI.LabelField(iconRect, EditorGUIUtility.IconContent("Sprite Icon"));
				EditorGUI.LabelField(labelRect, GUIContent.none, new GUIContent("None (Sprite)"));
				GUI.enabled = true;
			}
		}
	}
}