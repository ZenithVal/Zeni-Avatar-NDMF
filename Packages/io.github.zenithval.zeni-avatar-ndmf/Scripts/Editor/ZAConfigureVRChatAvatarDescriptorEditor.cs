using UnityEditor;
using ZeniAvatar.Runtime;
using VRC.SDK3.Avatars.Components;

namespace ZeniAvatar.Editor
{
	[CustomEditor(typeof(ZAConfigureAvatarDescriptor))]
	public class ZAConfigureAvatarDescriptorEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var my = (ZAConfigureAvatarDescriptor)target;

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZAConfigureAvatarDescriptor.avatarDescriptor)));

			serializedObject.ApplyModifiedProperties();
		}
	}
}