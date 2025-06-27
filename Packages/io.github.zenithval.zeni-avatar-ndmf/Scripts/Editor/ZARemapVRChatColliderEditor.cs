using UnityEditor;
using UnityEngine;
using ZeniAvatar.Runtime;
using VRC.SDK3.Avatars.Components;
using UnityEditor.EditorTools;
using UnityEngine.UIElements;

namespace ZeniAvatar.Editor
{
	[CustomEditor(typeof(ZARemapVRChatCollider))]
	public class ZARemapVRChatColliderEditor : UnityEditor.Editor
	{
		VRCAvatarDescriptor descriptor;

		public override void OnInspectorGUI()
		{
			var my = (ZARemapVRChatCollider)target;
			if (descriptor == null)
			{
				descriptor = my.transform.GetComponentInParent<VRCAvatarDescriptor>();
				EditorGUILayout.HelpBox("No VRCAvatarDescriptor found in parent hierarchy.", MessageType.Warning);
				return;
			}

			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.colliderToRemap)));
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.remapTarget)));

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.customShape)));
			if (my.customShape)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.radius)));
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.height)));

				if (my.colliderToRemap == VRChatColliders.Head ||
					my.colliderToRemap == VRChatColliders.Torso ||
					my.colliderToRemap == VRChatColliders.HandLeft || my.colliderToRemap == VRChatColliders.HandRight ||
					my.colliderToRemap == VRChatColliders.FootLeft || my.colliderToRemap == VRChatColliders.FootRight)
				{
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.position)));
					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.rotation)));
				}
				else
				{
					EditorGUILayout.HelpBox("Fingers use their parent bone to define position & rotation.", MessageType.Info);
				}


				EditorGUILayout.Space();
				EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.visualizeGizmo)));
			}

			serializedObject.ApplyModifiedProperties();
		}

		public void OnSceneGUI()
		{
			DrawCollider();
		}

		void DrawCollider()
		{
			var my = (ZARemapVRChatCollider)target;
			if (my.remapTarget == null) return;

			if (!(my.visualizeGizmo && my.customShape)) return;

			var transform = my.remapTargetObject.transform;
			var radius = my.radius;
			var height = my.height;

			var scale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			var clampedRadius = Mathf.Min(radius * scale, 2.5f * 0.5f) / scale;
			var clampedHeight = Mathf.Min(height * scale, 2.5f) / scale;

			bool isFinger =
				!(my.colliderToRemap == VRChatColliders.Head ||
				  my.colliderToRemap == VRChatColliders.Torso ||
				  my.colliderToRemap == VRChatColliders.HandLeft || my.colliderToRemap == VRChatColliders.HandRight ||
				  my.colliderToRemap == VRChatColliders.FootLeft || my.colliderToRemap == VRChatColliders.FootRight);

			if (isFinger && transform.parent != null)
			{
				var begin = transform.parent.position;
				var end = transform.position;

				var direction = (end - begin).normalized;
				var globalHeight = height * scale;
				end = begin + direction * globalHeight;
				var center = (begin + end) * 0.5f;
				var globalRadius = clampedRadius * scale;

				HandlesUtil.DrawWireCapsule(center, Quaternion.FromToRotation(Vector3.up, direction), globalHeight, globalRadius);
			}
			else
			{
				var globalPos = transform.TransformPoint(my.position);
				var globalRot = transform.rotation * my.rotation;
				HandlesUtil.DrawWireCapsule(globalPos, globalRot, clampedHeight * scale, clampedRadius * scale);
			}
		}
	}
}