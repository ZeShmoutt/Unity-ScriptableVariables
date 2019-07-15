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
		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.Toggle(position, GUIContent.none, default(bool));
		}
	}
}
