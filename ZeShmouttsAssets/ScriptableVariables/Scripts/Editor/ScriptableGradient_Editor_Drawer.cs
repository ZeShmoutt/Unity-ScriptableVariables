using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableGradient))]
	public class ScriptableGradient_Editor : ScriptableVariable_Editor<Gradient>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableGradient))]
	public class ScriptableGradient_Drawer : ScriptableVariable_Drawer<Gradient>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.GradientField(position, GUIContent.none, new Gradient());
		}

		#endregion
	}
}