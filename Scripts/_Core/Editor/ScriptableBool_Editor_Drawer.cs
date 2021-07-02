using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableBool))]
	public class ScriptableBool_Editor : ScriptableVariable_Editor<bool>
	{
		#region Type-specific parts

		protected override void DrawRuntimeValueField(bool _value, string _label)
		{
			EditorGUILayout.Toggle(_label, _value);
		}

		#endregion
	}

	[CustomPropertyDrawer(typeof(ScriptableBool))]
	public class ScriptableBool_Drawer : ScriptableVariable_Drawer<bool>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect _position)
		{
			EditorGUI.Toggle(_position, GUIContent.none, default);
		}

		#endregion
	}
}