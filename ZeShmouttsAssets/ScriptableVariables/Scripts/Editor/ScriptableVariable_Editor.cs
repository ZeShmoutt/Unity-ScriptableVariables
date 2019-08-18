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

		SerializedProperty scriptProp;

		SerializedProperty valueProp;
		SerializedProperty readonlyProp;

		#endregion

		#region Base GUI method

		private void OnEnable()
		{
			scriptProp = serializedObject.FindProperty("m_Script");

			valueProp = serializedObject.FindProperty("value");
			readonlyProp = serializedObject.FindProperty("readOnly");
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
			DrawValueField();
			DrawRuntimeValueField();
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
		/// Draws the value field like the regular inspector.
		/// </summary>
		protected virtual void DrawValueField()
		{
			EditorGUILayout.PropertyField(valueProp);
		}

		/// <summary>
		/// Draws the runtime value field like the regular inspector when in play mode, or a warning label when not playing.
		/// </summary>
		protected virtual void DrawRuntimeValueField()
		{
			if (EditorApplication.isPlaying)
			{
				ScriptableVariable<T> obj = serializedObject.targetObject as ScriptableVariable<T>;

				BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
				FieldInfo field = typeof(ScriptableVariable<T>).GetField("runtimeValue", bindFlags);

				T runtime = (T)field.GetValue(obj);

				bool wasEnabled = GUI.enabled;

				GUI.enabled = false;
				EditorGUILayout.LabelField("Runtime value", runtime.ToString());
				GUI.enabled = wasEnabled;
			}
			else
			{
				EditorGUILayout.HelpBox("The runtime value will be visible in play mode.", MessageType.Info);
			}
		}

		#endregion
	}
}