using UnityEditor;
using UnityEngine;

namespace Assets.NashUtilsCs.NullCheker
{
	[CustomPropertyDrawer(typeof(IsNotNull))]
	public class CannotBeNullDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect inRect, SerializedProperty inProp, GUIContent label)
		{
			EditorGUI.BeginProperty(inRect, label, inProp);

			bool isError = inProp.type == "string" && string.IsNullOrEmpty(inProp.stringValue) ||
				inProp.propertyType == SerializedPropertyType.ObjectReference && inProp.objectReferenceValue == null;
			if (isError)
			{
				if (inProp.type == "string" || inProp.propertyType == SerializedPropertyType.ObjectReference)
				{
					label.text = "[!] " + label.text;
					GUI.color = Color.red;
				}
			}

			EditorGUI.PropertyField(inRect, inProp, label);
			GUI.color = Color.white;

			EditorGUI.EndProperty();
		}
	}
}
