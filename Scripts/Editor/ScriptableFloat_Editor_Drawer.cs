using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableFloat))]
	public class ScriptableFloat_Editor : ScriptableVariable_Editor<float>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableFloat))]
	public class ScriptableFloat_Drawer : ScriptableVariable_Drawer<float>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(float).ToString()));
		}

		#endregion
	}
}