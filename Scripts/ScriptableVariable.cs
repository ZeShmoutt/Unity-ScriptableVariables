using UnityEngine;

namespace ZeShmouttsAssets.DataContainers
{
	/// <summary>
	/// Base class for ScriptableVariables.
	/// </summary>
	[System.Serializable]
	public abstract class ScriptableVariable<T> : ScriptableObject, ISerializationCallbackReceiver
	{
		#region Values

		/// <summary>
		/// Is this ScriptableVariable read only ?
		/// </summary>
		[SerializeField] protected bool readOnly = false;

		/// <summary>
		/// Base value. Will be kept in during runtime in case the ScriptableVariable needs to reset.
		/// </summary>
		[SerializeField] protected T value;

		/// <summary>
		/// Value used only at runtime. Can be safely modified as it isn't serialized.
		/// </summary>
		[System.NonSerialized] protected T runtimeValue;

		public void OnAfterDeserialize()
		{
			Reset();
		}

		public void OnBeforeSerialize()
		{

		}

		#endregion

		#region Public value interaction

		/// <summary>
		/// Returns the stored value of a ScriptableVariable if it exists, or a default value of the corresponding type otherwise.
		/// </summary>
		public T Value
		{
			get { return GetValueOrDefault(this); }
			set { TrySetValue(value); }
		}

		/// <summary>
		/// Can other scripts modify this ScriptableVariable's content ?
		/// </summary>
		public bool IsReadOnly
		{
			get { return readOnly; }
		}

		/// <summary>
		/// Resets the runtime value to the base value. Use with caution.
		/// </summary>
		public virtual void Reset()
		{
			runtimeValue = value;
		}

		#endregion

		#region Protected value interaction

		/// <summary>
		/// Returns the stored value of a ScriptableVariable if it exists, or a default value of the corresponding type otherwise.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		protected static T GetValueOrDefault(ScriptableVariable<T> target)
		{
			if (target != null)
			{
				return target.GetValue();
			}
			else
			{
				Debug.LogWarningFormat("A ScriptableVariable of type {0} has not been assigned, default value \"{1}\" has been used instead.", typeof(T).Name, default(T).ToString());
				return default(T);
			}
		}

		/// <summary>
		/// Returns the stored value. This will return the non-serialized runtime value only.
		/// </summary>
		/// <returns>Returns the stored value.</returns>
		protected virtual T GetValue()
		{
			return runtimeValue;
		}

		/// <summary>
		/// Tries to modify the ScriptableVariable's stored value. Will throw an error if the ScriptableVariable is read only.
		/// </summary>
		/// <param name="value">New value to assign to the ScriptableVariable.</param>
		protected void TrySetValue(T value)
		{
			if (readOnly)
			{
				SetValue(value);
			}
			else
			{
				throw new System.FieldAccessException(string.Format("The ScriptableVariable \"{0}\" ({1}) is read only.", this.name, typeof(T).Name));
			}
		}

		/// <summary>
		/// Assign a new value to the ScriptableVariable. This will modify the non-serialized runtime value only.
		/// </summary>
		/// <param name="value">New value to assign.</param>
		protected virtual void SetValue(T value)
		{
			this.runtimeValue = value;
		}

		#endregion

		#region Utility

		/// <summary>
		/// Shortcut to return the stored value as a string, instead of the ScriptableVariable.
		/// </summary>
		/// <returns>Returns the stored value as a string.</returns>
		public override string ToString()
		{
			return Value.ToString();
		}

		/// <summary>
		/// Implicit conversion to whatever type the variable is, for seamless integration.
		/// </summary>
		public static implicit operator T(ScriptableVariable<T> s)
		{
			return GetValueOrDefault(s);
		}

		#endregion
	}
}

#if UNITY_EDITOR

namespace ZeShmouttsAssets.DataContainers.EditorScripts
{
	/// <summary>
	/// Editor constants used for ScriptableVariable editor stuff.
	/// </summary>
	public static class EditorConstants
	{
		/// <summary>
		/// Base menu path for creating ScriptableVariables in the editor.
		/// </summary>
		public const string MenuNamePath = "ZeShmoutt's Assets/Data Containers/Scriptable Variables/";
	}
}

#endif