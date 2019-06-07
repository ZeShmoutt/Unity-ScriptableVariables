using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableVariable<>))]
	public abstract class ScriptableVariable_Editor<T> : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawScriptField();
			EditorGUILayout.Space();
			DrawAccessCheckbox();
			DrawVariableField();

			SaveAsset();
		}

		protected void DrawScriptField()
		{
			GUI.enabled = false;
			SerializedProperty prop = serializedObject.FindProperty("m_Script");
			EditorGUILayout.PropertyField(prop, true);
			GUI.enabled = true;
		}

		protected void DrawAccessCheckbox()
		{
			SerializedProperty fullAccess = serializedObject.FindProperty("externalControl");
			EditorGUILayout.PropertyField(fullAccess, new GUIContent("Allow external control", "Can external scripts modify the variable ?"));
		}

		protected virtual void DrawVariableField()
		{
			SerializedProperty val = serializedObject.FindProperty("value");
			EditorGUILayout.PropertyField(val);
		}

		protected void SaveAsset()
		{
			if (GUI.changed)
			{
				Undo.RecordObject(target, "Field change");
				serializedObject.ApplyModifiedProperties();
				EditorUtility.SetDirty(target);
			}
		}
	}

	public abstract class ScriptableVariable_Drawer<T> : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float halfWidth = position.width * 0.5f;
			Rect valueRect = new Rect(position.x, position.y, halfWidth - 5f, position.height);
			Rect objectRect = new Rect(position.x + halfWidth + 5f, position.y, halfWidth - 5f, position.height);

			DrawValueField(valueRect, property);
			DrawObjectField(objectRect, property);
		}

		protected virtual void DrawValueField(Rect position, SerializedProperty property)
		{
			// Alan please add override code
		}

		protected virtual void DrawObjectField(Rect position, SerializedProperty property)
		{
			EditorGUI.PropertyField(position, property, GUIContent.none);
		}
	}
}