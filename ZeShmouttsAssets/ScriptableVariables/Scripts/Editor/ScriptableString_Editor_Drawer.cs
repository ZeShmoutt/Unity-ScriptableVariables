using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableString))]
	public class ScriptableString_Editor : ScriptableVariable_Editor<string>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableString))]
	public class ScriptableString_Drawer : ScriptableVariable_Drawer<string>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent(default(string)));
		}

		#endregion
	}
}