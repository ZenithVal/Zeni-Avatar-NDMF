using UnityEditor;
using ZeniAvatar.Runtime;
using VRC.SDK3.Avatars.Components;

namespace ZeniAvatar.Editor
{
	[CustomEditor(typeof(ZASetViewPosition))]
	public class ZASetViewPositionEditor : UnityEditor.Editor
	{
		VRCAvatarDescriptor descriptor;

		public override void OnInspectorGUI()
		{
			var my = (ZASetViewPosition)target;

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZASetViewPosition.customViewPosition)));
			if (my.customViewPosition)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZASetViewPosition.viewPositionX)));
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZASetViewPosition.viewPositionY)));
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZASetViewPosition.viewPositionZ)));
			}
			else
			{
				EditorGUI.BeginDisabledGroup(true);
				
				if (descriptor == null)
				{
					descriptor = my.transform.GetComponentInParent<VRCAvatarDescriptor>();
					EditorGUILayout.HelpBox("No VRCAvatarDescriptor found in parent hierarchy.", MessageType.Warning);
					return;
				}
				var position = my.transform.position - descriptor.gameObject.transform.position;

				EditorGUILayout.FloatField("View Position X", position.x);
				EditorGUILayout.FloatField("View Position Y", position.y);
				EditorGUILayout.FloatField("View Position Z", position.z);

				EditorGUI.EndDisabledGroup();
			}
			serializedObject.ApplyModifiedProperties();
		}
	}
}