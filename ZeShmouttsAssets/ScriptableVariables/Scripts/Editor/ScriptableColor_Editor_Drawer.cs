using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableColor))]
	public class ScriptableColor_Editor : ScriptableVariable_Editor<Color>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableColor))]
	public class ScriptableColor_Drawer : ScriptableVariable_Drawer<Color>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.ColorField(position, GUIContent.none, default(Color));
		}

		#endregion
	}
}