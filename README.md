# Yet another ScriptableVariable framework

A way of storing common types as references in your assets.

## How to create a ScriptableVariable

As usual with ScriptableObjects :

* Using Unity's menu bar : `Assets/Create/ZeShmoutt's Assets/Data Containers/Scriptable Variables/<Variable type>`
* In the Project window : `Create/ZeShmoutt's Assets/Data Containers/Scriptable Variables/<Variable type>`

## How to use in your scripts

**Step 1 :** Add `using ZeShmouttsAssets.DataContainers;` at the top with the other `using`s.

**Step 2 :** Use a ScriptableVariable like any other variable type. Please note that the generic `ScriptableVariable<T>` type is marked as `abstract`, so use the subtypes instead (`ScriptableBool`, `ScriptableFloat`, and so on), as you can't instantiate the generic version.

**Note :**

This version of ScriptableVariables has been made with seamless integration in mind, so :

* ScriptableVariables are affected by operators just like the type they represent : `1 + ScriptableFloatOfValue4` would result in 5 as if you directly did `1 + 4`.
* The `ScriptableVariable.ToString()` method is actually a shortcut for `ScriptableVariable.value.ToString()`.
* The `ScriptableGradient` subtype has a shortcut for `Evaluate()`.

On top of that, ScriptableVariables use a runtime value, that can be safely modified without touching the original value set in the editor. The original value itself is pretty much inaccessible anyway.

## How to create additional ScriptableVariable subtypes

Copy any of the existing subtypes, and change the generic type and whatever else you want. As an example, a custom ScriptableTexture substype :

    using UnityEngine;

    namespace ZeShmouttsAssets.DataContainers
    {
        [CreateAssetMenu(menuName = EditorScripts.EditorConstants.MenuNamePath + "Texture", fileName = "New Texture")]
        public class ScriptableTexture : ScriptableVariable<Texture>
        {
            // Whatever additional features you need.
        } 
    }
    
That's it. All the features of the base version, by changing exactly 4 words from "String" to "Texture".

Optionally, you can also duplicate the corresponding Editor script to get that fancy inspector and property drawer.

	using UnityEditor;
	using UnityEngine;

	namespace ZeShmouttsAssets.DataContainers.EditorScripts
	{
		[CustomEditor(typeof(ScriptableTexture))]
		public class ScriptableTexture_Editor : ScriptableVariable_Editor<Texture>
		{
			// Whatever additional features you need.
		}

		[CustomPropertyDrawer(typeof(ScriptableBool))]
		public class ScriptableTexture_Drawer : ScriptableVariable_Drawer<Texture>
		{
			#region Type-specific parts

			protected override void DrawValueWhenNull(Rect position)
			{
				EditorGUI.LabelField(position, GUIContent.none, new GUIContent("None (Texture)"));
			}

			#endregion
		}
	}

Note the `DrawValueWhenNull()` method : this is the shortcut field drawn by the property drawer when there's no ScriptableVariable assigned to get a value from. The base version displays a label field with a dynamic type :

	EditorGUI.LabelField(position, GUIContent.none, new GUIContent(string.Format("None ({0})", typeof(T).Name)));

But you might want to override it to get a custom display, like what is done for the ScriptableColor (displaying a color field) and ScriptableSprite (displaying a custom label with a sprite icon) subtypes.
