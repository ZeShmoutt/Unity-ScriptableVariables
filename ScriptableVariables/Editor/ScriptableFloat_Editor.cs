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
		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(float).ToString()));
		}
	}
}
