using UnityEngine;

namespace ZeShmouttsAssets.DataContainers
{
	[CreateAssetMenu(menuName = EditorScripts.EditorConstants.MenuNamePath + "Gradient", fileName = "New Gradient")]
	public class ScriptableGradient : ScriptableVariable<Gradient>
	{
		#region Utility

		/// <summary>
		/// Calculate color at a given time.
		/// </summary>
		/// <param name="time">Time of the key (0 - 1).</param>
		/// <returns>Returns a Color.</returns>
		public Color Evaluate(float time)
		{
			return GetValueOrDefault(this).Evaluate(time);
		}

		#endregion
	}
}