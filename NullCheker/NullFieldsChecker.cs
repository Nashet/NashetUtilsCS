using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Assets.NashUtilsCs.NullChecker
{
	public class NullFieldsChecker : UnityEditor.AssetModificationProcessor
	{
		private static bool IsEnabled = true;
		private static string prefix = $"[{nameof(NullFieldsChecker)}]";
		private static Type checkingAttribute = typeof(SerializeField);
		private static List<Type> excludedTypes = new List<Type> { };//typeof(TextMeshProUGUI)

		[DidReloadScripts]
		private static void OnScriptsReloaded()
		{
			if (!IsEnabled)
				return;
			// Get all game objects in the scene
			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>(); // add SO files

			foreach (var gameObject in allObjects)
			{
				// Get all the MonoBehaviour components attached to the current game object
				MonoBehaviour[] monoBehaviours = gameObject.GetComponents<MonoBehaviour>();

				foreach (var monoBehaviour in monoBehaviours)
				{
					if (monoBehaviour == null)
						continue;
					Type scriptType = monoBehaviour.GetType();
					if (excludedTypes.Contains(scriptType))
						continue;

					var fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
					foreach (var field in fields)
					{
						var customAttribute = Attribute.GetCustomAttribute(field, checkingAttribute);// typeof(SerializeField)) as SerializeField;
						if (customAttribute != null)
						{
							var fieldValue = field.GetValue(monoBehaviour);
							if (fieldValue == null || fieldValue is GameObject && fieldValue as GameObject == null)
							{
								Debug.LogError($"{prefix} Field with {checkingAttribute} attribute is null on {gameObject.name} => {field.Name}!", gameObject);
							}
						}
					}
				}
			}
		}
	}
}
