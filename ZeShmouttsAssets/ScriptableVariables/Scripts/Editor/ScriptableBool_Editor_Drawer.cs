using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableBool))]
	public class ScriptableBool_Editor : ScriptableVariable_Editor<bool>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableBool))]
	public class ScriptableBool_Drawer : ScriptableVariable_Drawer<bool>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.Toggle(position, GUIContent.none, default(bool));
		}

		#endregion
	}
}