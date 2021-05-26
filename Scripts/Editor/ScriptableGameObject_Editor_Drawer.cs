using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	[CustomEditor(typeof(ScriptableGameObject))]
	public class ScriptableGameObject_Editor : ScriptableVariable_Editor<GameObject>
	{
		// No changes needed.
	}

	[CustomPropertyDrawer(typeof(ScriptableGameObject))]
	public class ScriptableGameObject_Drawer : ScriptableVariable_Drawer<GameObject>
	{
		#region Type-specific parts

		protected override void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent("None (GameObject)"));
		}

		#endregion
	}
}