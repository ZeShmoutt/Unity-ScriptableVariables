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
		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(int).ToString()));
		}
	}
}
