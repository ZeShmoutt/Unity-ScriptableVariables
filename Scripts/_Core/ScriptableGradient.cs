using UnityEngine;

namespace ZeShmouttsAssets.DataContainers
{
	[CreateAssetMenu(menuName = EditorScripts.MenuPathConstants.MENU_NAME_PATH + "Gradient", fileName = "New Gradient")]
	public class ScriptableGradient : ScriptableVariable<Gradient>
	{
		#region Utility

		/// <summary>
		/// Calculate color at a given time.
		/// </summary>
		/// <param name="_time">Time of the key (0 - 1).</param>
		/// <returns>Returns a Color.</returns>
		public Color Evaluate(float _time)
		{
			return GetValueOrDefault(this).Evaluate(_time);
		}

		#endregion
	}
}