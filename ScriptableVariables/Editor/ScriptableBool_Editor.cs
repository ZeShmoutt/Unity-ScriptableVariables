using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableBool))]
	public class ScriptableBool_Editor : ScriptableVariable_Editor<bool>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableBool))]
	public class ScriptableBool_Drawer : ScriptableVariable_Drawer<bool>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableBool valueProp = (ScriptableBool)property.objectReferenceValue;

			if (valueProp != null)
			{
				if (valueProp.AllowExternalControl)
				{
					valueProp.Value = EditorGUI.Toggle(position, GUIContent.none, valueProp.Value);
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.Toggle(position, GUIContent.none, valueProp.Value);
					GUI.enabled = true;
				}
			}
			else
			{
				GUI.enabled = false;
				EditorGUI.Toggle(position, GUIContent.none, default(bool));
				GUI.enabled = true;
			}
		}
	}
}
