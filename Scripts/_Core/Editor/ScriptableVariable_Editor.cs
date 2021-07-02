using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	/// <summary>
	/// Custom inspector for ScriptableVariables.
	/// </summary>
	[CustomEditor(typeof(ScriptableVariable<>))]
	public abstract class ScriptableVariable_Editor<T> : Editor
	{
		#region Variables

		private SerializedProperty scriptProp;
		private SerializedProperty valueProp;
		private SerializedProperty runtimeValueProp;
		private SerializedProperty readonlyProp;

		#endregion

		#region Base GUI method

		private void OnEnable()
		{
			scriptProp = serializedObject.FindProperty(EditorConstants.FIELD_SCRIPT);

			valueProp = serializedObject.FindProperty(EditorConstants.FIELD_VALUE_BASE);
			runtimeValueProp = serializedObject.FindProperty(EditorConstants.FIELD_VALUE_RUNTIME);
			readonlyProp = serializedObject.FindProperty(EditorConstants.FIELD_READ_ONLY);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawScriptField();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			DrawTypeLabel();
			DrawAccessCheckbox();
			EditorGUILayout.EndVertical();

			EditorGUILayout.LabelField("Value", EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			DrawBaseValueField();
			DrawRuntimeValueFieldInternal();
			EditorGUILayout.EndVertical();

			if (GUI.changed)
			{
				Undo.RecordObject(target, "Field change");
				serializedObject.ApplyModifiedProperties();
			}
		}

		private void OnDisable()
		{

		}

		#endregion

		#region General parts

		/// <summary>
		/// Draws a script field similar to the regular inspector, allowing quick access to the script itself.
		/// </summary>
		protected void DrawScriptField()
		{
			bool wasEnabled = GUI.enabled;

			GUI.enabled = false;
			EditorGUILayout.PropertyField(scriptProp, true);
			GUI.enabled = wasEnabled;
		}

		/// <summary>
		/// Draws a label to remind what type of variable it is.
		/// </summary>
		protected void DrawTypeLabel()
		{
			bool wasEnabled = GUI.enabled;

			GUI.enabled = false;
			EditorGUILayout.LabelField("Type", typeof(T).FullName);
			GUI.enabled = wasEnabled;
		}

		/// <summary>
		/// Draws the read only checkbox.
		/// </summary>
		protected void DrawAccessCheckbox()
		{
			EditorGUILayout.PropertyField(readonlyProp, new GUIContent("Read only", "Is this variable read only ?"));
		}

		#endregion

		#region Type-specific parts

		/// <summary>
		/// Draws the base value field.
		/// </summary>
		protected virtual void DrawBaseValueField()
		{
			EditorGUILayout.PropertyField(valueProp, new GUIContent("Base value"));
		}

		/// <summary>
		/// Internal method for drawing the runtime value field when in play mode, or a warning label when not playing.
		/// </summary>
		private void DrawRuntimeValueFieldInternal()
		{
			if (EditorApplication.isPlaying)
			{
				ScriptableVariable<T> obj = serializedObject.targetObject as ScriptableVariable<T>;

				BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
				FieldInfo field = typeof(ScriptableVariable<T>).GetField(EditorConstants.FIELD_VALUE_RUNTIME, bindFlags);

				T runtimeValue = (T)field.GetValue(obj);

				using (new EditorGUI.DisabledScope(true))
				{
					DrawRuntimeValueField(runtimeValue, "Runtime value");
				}
			}
			else
			{
				EditorGUILayout.HelpBox("The runtime value will be visible in play mode.", MessageType.Info);
			}
		}

		/// <summary>
		/// Draws the runtime value field while playing.
		/// </summary>
		protected virtual void DrawRuntimeValueField(T _value, string _label)
		{
			if (_value is Object)
			{
				EditorGUILayout.ObjectField(_label, _value as Object, typeof(Object), false);
			}
			else
			{
				EditorGUILayout.LabelField(_label, _value.ToString());
			}
		}

		#endregion
	}
}