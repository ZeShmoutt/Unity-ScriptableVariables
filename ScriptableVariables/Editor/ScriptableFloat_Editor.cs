using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableFloat))]
	public class ScriptableFloat_Editor : ScriptableVariable_Editor<float>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableFloat))]
	public class ScriptableFloat_Drawer : ScriptableVariable_Drawer<float>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableFloat valueProp = (ScriptableFloat)property.objectReferenceValue;

			if (valueProp != null)
			{
				if (valueProp.AllowExternalControl)
				{
					valueProp.Value = EditorGUI.FloatField(position, GUIContent.none, valueProp.Value);
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.FloatField(position, GUIContent.none, valueProp.Value);
					GUI.enabled = true;
				}
			}
			else
			{
				GUI.enabled = false;
				EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(float).ToString()));
				GUI.enabled = true;
			}
		}
	} 
}
