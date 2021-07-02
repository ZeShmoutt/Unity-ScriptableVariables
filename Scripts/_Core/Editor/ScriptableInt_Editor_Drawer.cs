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

		protected override void DrawValueWhenNull(Rect _position)
		{
			EditorGUI.LabelField(_position, GUIContent.none, new GUIContent(default(int).ToString()));
		}

		#endregion
	}
}