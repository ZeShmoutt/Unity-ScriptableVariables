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

		public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			_position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);

			float halfWidth = _position.width * 0.5f;
			Rect valueRect = new Rect(_position.x, _position.y, halfWidth - 5f, _position.height);
			Rect objectRect = new Rect(_position.x + halfWidth + 5f, _position.y, halfWidth - 5f, _position.height);

			DrawObjectField(objectRect, _property);
			DrawValueField(valueRect, _property);
		}

		#endregion

		#region General parts

		/// <summary>
		/// Parent method for drawing both the ScriptableVariable field and its shortcut value field.
		/// </summary>
		protected virtual void DrawValueField(Rect _position, SerializedProperty _property)
		{
			ScriptableVariable<T> scriptableObject = (ScriptableVariable<T>)_property.objectReferenceValue;

			bool wasEnabled = GUI.enabled;

			if (scriptableObject != null)
			{
				SerializedObject ser = new SerializedObject(scriptableObject);
				SerializedProperty prop = ser.FindProperty(EditorConstants.FIELD_VALUE_BASE);

				GUI.enabled = scriptableObject.IsReadOnly;
				DrawValueWhenNotNull(_position, prop);
				GUI.enabled = wasEnabled;
			}
			else
			{
				GUI.enabled = false;
				DrawValueWhenNull(_position);
				GUI.enabled = wasEnabled;
			}
		}

		/// <summary>
		/// Draws the object selection field to pick a ScriptableVariable.
		/// </summary>
		protected virtual void DrawObjectField(Rect _position, SerializedProperty _property)
		{
			EditorGUI.PropertyField(_position, _property, GUIContent.none);
		}

		#endregion

		#region Type-specific parts

		/// <summary>
		/// Draws a copy of the ScriptableVariable value field, acting as a shortcut.
		/// </summary>
		protected virtual void DrawValueWhenNotNull(Rect _position, SerializedProperty _prop)
		{
			EditorGUI.PropertyField(_position, _prop, GUIContent.none);
		}

		/// <summary>
		/// Draws a fake field showing the default value according to the ScriptableVariable's type.
		/// </summary>
		protected virtual void DrawValueWhenNull(Rect _position)
		{
			EditorGUI.LabelField(_position, GUIContent.none, new GUIContent($"None ({typeof(T).Name})"));
		}

		#endregion
	}
}