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
		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent("None (GameObject)"));
		}
	}
}