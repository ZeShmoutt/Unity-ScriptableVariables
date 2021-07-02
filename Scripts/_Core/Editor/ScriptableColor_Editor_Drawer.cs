using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableColor))]
	public class ScriptableColor_Editor : ScriptableVariable_Editor<Color>
	{
		#region Type-specific parts

		protected override void DrawRuntimeValueField(Color _value, string _label)
		{
			EditorGUILayout.ColorField(_label, _value);
		}

		#endregion
	}

	[CustomPropertyDrawer(typeof(ScriptableColor))]
	public class ScriptableColor_Drawer : ScriptableVariable_Drawer<Color>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect _position)
		{
			EditorGUI.ColorField(_position, GUIContent.none, default);
		}

		#endregion
	}
}