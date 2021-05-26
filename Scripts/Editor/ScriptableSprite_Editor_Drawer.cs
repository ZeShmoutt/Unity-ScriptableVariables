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

		protected override void DrawValueWhenNull(Rect position)
		{
			Rect iconRect = new Rect(position.x, position.y, position.height, position.height);
			Rect labelRect = new Rect(position.x + position.height, position.y, position.width - position.height, position.height);
			EditorGUI.LabelField(iconRect, EditorGUIUtility.IconContent("Sprite Icon"));
			EditorGUI.LabelField(labelRect, GUIContent.none, new GUIContent("None (Sprite)"));
		}

		#endregion
	}
}