using UnityEngine;

[System.Serializable]
public abstract class ScriptableVariable<T> : ScriptableObject
{
	[SerializeField] protected bool externalControl = false;
	[SerializeField] protected T value;

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
	public bool AllowExternalControl
	{
		get { return externalControl; }
	}

	/// <summary>
	/// Returns the stored value of a ScriptableVariable if it exists, or a default value of the corresponding type otherwise.
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	protected static T GetValueOrDefault(ScriptableVariable<T> target)
	{
		if(target != null)
		{
			return target.GetValue();
		}
		else
		{
			Debug.LogWarningFormat("A ScriptableVariable of type {0} has not been assigned, default value \"{1}\" has been used instead", typeof(T).Name, default(T).ToString());
			return default(T);
		}
	}

	/// <summary>
	/// Returns the stored value.
	/// </summary>
	/// <returns>Returns the stored value.</returns>
	protected virtual T GetValue()
	{
		return value;
	}

	/// <summary>
	/// Tries to modify the ScriptableVariable's stored value. Will throw an error if external control isn't allowed.
	/// </summary>
	/// <param name="value">New value to assign to the ScriptableVariable.</param>
	protected void TrySetValue(T value)
	{
		if (externalControl)
		{
			SetValue(value);
		}
		else
		{
			throw new System.FieldAccessException(string.Format("The ScriptableVariable \"{0}\" ({1}) does not give control to external sources.", this.name, typeof(T).Name));
		}
	}

	/// <summary>
	/// Assign a new value to the ScriptableVariable.
	/// </summary>
	/// <param name="value">New value to assign.</param>
	protected virtual void SetValue(T value)
	{
		this.value = value;
	}

	/// <summary>
	/// Shortcut to return the stored value as a string instead of the ScriptableVariable.
	/// </summary>
	/// <returns>Returns the stored value as a string.</returns>
	public override string ToString()
	{
		return Value.ToString();
	}

	public static implicit operator T(ScriptableVariable<T> s)
	{
		return GetValueOrDefault(s);
	}
}