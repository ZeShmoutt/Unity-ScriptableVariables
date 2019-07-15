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
		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(string)));
		}
	}
}