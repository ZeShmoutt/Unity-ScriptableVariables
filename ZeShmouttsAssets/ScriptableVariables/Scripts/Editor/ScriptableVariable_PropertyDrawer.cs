using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	/// <summary>
	/// Custom property drawer for ScriptableVariables.
	/// </summary>
	public abstract class ScriptableVariable_Drawer<T> : PropertyDrawer
	{
		#region Base GUI method

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			float halfWidth = position.width * 0.5f;
			Rect valueRect = new Rect(position.x, position.y, halfWidth - 5f, position.height);
			Rect objectRect = new Rect(position.x + halfWidth + 5f, position.y, halfWidth - 5f, position.height);

			DrawObjectField(objectRect, property);
			DrawValueField(valueRect, property);
		}

		#endregion

		#region General parts

		/// <summary>
		/// Parent method for drawing both the ScriptableVariable field and its shortcut value field.
		/// </summary>
		protected virtual void DrawValueField(Rect position, SerializedProperty property)
		{
			ScriptableVariable<T> scriptableObject = (ScriptableVariable<T>)property.objectReferenceValue;

			bool wasEnabled = GUI.enabled;

			if (scriptableObject != null)
			{
				SerializedObject ser = new SerializedObject(scriptableObject);
				SerializedProperty prop = ser.FindProperty("value");

				GUI.enabled = scriptableObject.IsReadOnly;
				DrawValueWhenNotNull(position, prop);
				GUI.enabled = wasEnabled;
			}
			else
			{
				GUI.enabled = false;
				DrawValueWhenNull(position);
				GUI.enabled = wasEnabled;
			}
		}

		/// <summary>
		/// Draws the object selection field to pick a ScriptableVariable.
		/// </summary>
		protected virtual void DrawObjectField(Rect position, SerializedProperty property)
		{
			EditorGUI.PropertyField(position, property, GUIContent.none);
		}

		#endregion

		#region Type-specific parts

		/// <summary>
		/// Draws a copy of the ScriptableVariable value field, acting as a shortcut.
		/// </summary>
		protected virtual void DrawValueWhenNotNull(Rect position, SerializedProperty prop)
		{
			EditorGUI.PropertyField(position, prop, GUIContent.none);
		}

		/// <summary>
		/// Draws a fake field showing the default value according to the ScriptableVariable's type.
		/// </summary>
		protected virtual void DrawValueWhenNull(Rect position)
		{
			EditorGUI.LabelField(position, GUIContent.none, new GUIContent("None"));
		}

		#endregion
	}
}