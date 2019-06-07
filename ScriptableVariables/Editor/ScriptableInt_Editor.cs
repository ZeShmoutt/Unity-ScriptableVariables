using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableInt))]
	public class ScriptableInt_Editor : ScriptableVariable_Editor<int>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableInt))]
	public class ScriptableInt_Drawer : ScriptableVariable_Drawer<int>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableInt valueProp = (ScriptableInt)property.objectReferenceValue;

			if (valueProp != null)
			{
				if (valueProp.AllowExternalControl)
				{
					valueProp.Value = EditorGUI.IntField(position, GUIContent.none, valueProp.Value);
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.IntField(position, GUIContent.none, valueProp.Value);
					GUI.enabled = true;
				}
			}
			else
			{
				GUI.enabled = false;
				EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(int).ToString()));
				GUI.enabled = true;
			}
		}
	}
}
