using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableSprite))]
	public class ScriptableSprite_Editor : ScriptableVariable_Editor<Sprite>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableSprite))]
	public class ScriptableSprite_Drawer : ScriptableVariable_Drawer<Sprite>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect _position)
		{
			Rect iconRect = new Rect(_position.x, _position.y, _position.height, _position.height);
			Rect labelRect = new Rect(_position.x + _position.height, _position.y, _position.width - _position.height, _position.height);
			EditorGUI.LabelField(iconRect, EditorGUIUtility.IconContent("Sprite Icon"));
			EditorGUI.LabelField(labelRect, GUIContent.none, new GUIContent("None (Sprite)"));
		}

		#endregion
	}
}