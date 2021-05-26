using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableInt))]
	public class ScriptableInt_Editor : ScriptableVariable_Editor<int>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableInt))]
	public class ScriptableInt_Drawer : ScriptableVariable_Drawer<int>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(int).ToString()));
		}

		#endregion
	}
}