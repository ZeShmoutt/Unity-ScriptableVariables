using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableGradient))]
	public class ScriptableGradient_Editor : ScriptableVariable_Editor<Gradient>
	{
		#region Type-specific parts

		protected override void DrawRuntimeValueField(Gradient _value, string _label)
		{
			EditorGUILayout.GradientField(_label, _value);
		}

		#endregion
	}

	[CustomPropertyDrawer(typeof(ScriptableGradient))]
	public class ScriptableGradient_Drawer : ScriptableVariable_Drawer<Gradient>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect _position)
		{
			EditorGUI.GradientField(_position, GUIContent.none, new Gradient());
		}

		#endregion
	}
}