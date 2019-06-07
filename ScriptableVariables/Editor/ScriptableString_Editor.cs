using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableString))]
	public class ScriptableString_Editor : ScriptableVariable_Editor<string>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableString))]
	public class ScriptableString_Drawer : ScriptableVariable_Drawer<string>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableString valueProp = (ScriptableString)property.objectReferenceValue;

			if (valueProp != null)
			{
				if (valueProp.AllowExternalControl)
				{
					valueProp.Value = EditorGUI.TextField(position, GUIContent.none, valueProp.Value);
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.TextField(position, GUIContent.none, valueProp.Value);
					GUI.enabled = true;
				}
			}
			else
			{
				GUI.enabled = false;
				EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(string)));
				GUI.enabled = true;
			}
		}
	}
}