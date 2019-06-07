using UnityEditor;
using UnityEngine;

namespace ScriptableVariablesEditor
{
	[CustomEditor(typeof(ScriptableSprite))]
	public class ScriptableSprite_Editor : ScriptableVariable_Editor<Sprite>
	{
	}

	[CustomPropertyDrawer(typeof(ScriptableSprite))]
	public class ScriptableSprite_Drawer : ScriptableVariable_Drawer<Sprite>
	{
		protected override void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableSprite valueProp = (ScriptableSprite)property.objectReferenceValue;

			if (valueProp != null)
			{
				if (valueProp.AllowExternalControl)
				{
					valueProp.Value = EditorGUI.ObjectField(position, GUIContent.none, valueProp.Value, typeof(Sprite), false) as Sprite;
				}
				else
				{
					GUI.enabled = false;
					EditorGUI.ObjectField(position, GUIContent.none, valueProp.Value, typeof(Sprite), false);
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