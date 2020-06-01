# Yet another ScriptableVariable framework

A way of storing common types as references in your assets.



## Uses and restrictions

As per the MIT license, this is pretty much unrestricted in use and modification.

I'd appreciate a lot if you could mention me somewhere if you use it, though.

## Links

 - [License](LICENSE)
 - [Changelog](CHANGELOG.md)

## Installation

 - Unity 2019 or newer : Open the Package manager, select  and import the package with the git URL.
 - Unity 2018 or older : Clone/download the repo and put it in your Unity project's assets.
 
-----

## Why would I need to store variables in ScriptableObjects ?

This git is essentially my implementation of the talk [*Game Architecture with Scriptable Objects*](https://www.youtube.com/watch?v=raQ3iHhE_Kk) by Ryan Hipple, so I highly recommend to watch it to fully understand.

In short, it's about modularity.

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
* The `ScriptableVariable<T>.ToString()` method is actually a shortcut for `ScriptableVariable<T>.value.ToString()`.
* The `ScriptableGradient` subtype has a shortcut for `Evaluate()`.
* **IMPORTANT :** While `if(ScriptableBool)` is usually interpreted as `if the ScriptableBool object exists`, due to the implicit conversion it will be interpreted as `if the ScriptableBool's value is true`. **This is fully intentional** (and it will also return false properly if the ScriptableBool object doesn't exist) - however it means that you can't distinguish between `the ScriptableBool object doesn't exist` and `the ScriptableBool's value is false`. I can't think of a case where the distinction might be important considering how ScriptableVariables are intended to be used, but keep that in mind anyway, okay ?

On top of that, ScriptableVariables use a runtime value, that can be safely modified without touching the original value set in the editor. The original value itself is pretty much inaccessible anyway.

## How to create additional ScriptableVariable subtypes

Copy any of the existing subtypes, and change the generic type and whatever else you want. As an example, a custom ScriptableTexture subtype created from a copy of the ScriptableString :

    using UnityEngine;

    namespace ZeShmouttsAssets.DataContainers
    {
        [CreateAssetMenu(menuName = EditorScripts.EditorConstants.MenuNamePath + "Texture", fileName = "New Texture")]
        public class ScriptableTexture : ScriptableVariable<Texture>
        {
            // Whatever additional features you need.
        } 
    }
    
That's it. All the features of the base version, by replacing exactly 4 words from "String" to "Texture".

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

Note the `DrawValueWhenNull()` method : this is the shortcut field drawn by the property drawer when there's no ScriptableVariable assigned to get a value from. The base version from `ScriptableVariable<T>` displays a label field with a dynamic type :

	EditorGUI.LabelField(position, GUIContent.none, new GUIContent(string.Format("None ({0})", typeof(T).Name)));

But you might want to override it to get a custom display, like what is done for the ScriptableColor (displaying a color field) and ScriptableSprite (displaying a custom label with a sprite icon) subtypes.
