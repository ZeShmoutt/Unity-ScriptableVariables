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
		[UnityEngine.Serialization.FormerlySerializedAs("readOnly")]
		[SerializeField] protected bool m_readOnly = false;

		/// <summary>
		/// Base value. Will be kept in during runtime in case the ScriptableVariable needs to reset.
		/// </summary>
		[UnityEngine.Serialization.FormerlySerializedAs("value")]
		[SerializeField] protected T m_valueBase;

		/// <summary>
		/// Value used only at runtime. Can be safely modified as it isn't serialized.
		/// </summary>
		[System.NonSerialized] protected T m_valueRuntime;

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
			get { return m_readOnly; }
		}

		/// <summary>
		/// Resets the runtime value to the base value. Use with caution.
		/// </summary>
		public virtual void Reset()
		{
			m_valueRuntime = m_valueBase;
		}

		#endregion

		#region Protected value interaction

		/// <summary>
		/// Returns the stored value of a ScriptableVariable if it exists, or a default value of the corresponding type otherwise.
		/// </summary>
		/// <param name="_target"></param>
		/// <returns></returns>
		protected static T GetValueOrDefault(ScriptableVariable<T> _target)
		{
			if (_target != null)
			{
				return _target.GetValue();
			}
			else
			{
				Debug.LogWarning($"A ScriptableVariable of type {typeof(T).Name} has not been assigned, default value \"{default(T).ToString()}\" has been used instead.");
				return default;
			}
		}

		/// <summary>
		/// Returns the stored value. This will return the non-serialized runtime value only.
		/// </summary>
		/// <returns>Returns the stored value.</returns>
		protected virtual T GetValue()
		{
			return m_valueRuntime;
		}

		/// <summary>
		/// Tries to modify the ScriptableVariable's stored value. Will throw an error if the ScriptableVariable is read only.
		/// </summary>
		/// <param name="_value">New value to assign to the ScriptableVariable.</param>
		protected void TrySetValue(T _value)
		{
			if (!m_readOnly)
			{
				SetValue(_value);
			}
			else
			{
				throw new System.FieldAccessException($"The ScriptableVariable \"{this.name}\" ({typeof(T).Name}) is read only.");
			}
		}

		/// <summary>
		/// Assign a new value to the ScriptableVariable. This will modify the non-serialized runtime value only.
		/// </summary>
		/// <param name="_value">New value to assign.</param>
		protected virtual void SetValue(T _value)
		{
			this.m_valueRuntime = _value;
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
		public static implicit operator T(ScriptableVariable<T> _s)
		{
			return GetValueOrDefault(_s);
		}

		#endregion
	}
}