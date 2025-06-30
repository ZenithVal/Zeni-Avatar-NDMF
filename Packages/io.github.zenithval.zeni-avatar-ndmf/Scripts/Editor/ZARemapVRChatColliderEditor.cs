using UnityEditor;
using UnityEngine;
using ZeniAvatar.Runtime;
using VRC.SDK3.Avatars.Components;
using UnityEditor.EditorTools;
using UnityEngine.UIElements;
using static VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;

namespace ZeniAvatar.Editor
{
	[CustomEditor(typeof(ZARemapVRChatCollider))]
	public class ZARemapVRChatColliderEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var my = (ZARemapVRChatCollider)target;

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
			}

			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(ZARemapVRChatCollider.visualizeGizmo)));

			serializedObject.ApplyModifiedProperties();
		}

		public void OnSceneGUI()
		{
			DrawCollider();
		}

		void DrawCollider()
		{
			var my = (ZARemapVRChatCollider)target;
			if (my.remapTargetObject == null) return;
			var descriptor = my.remapTargetObject.GetComponentInParent<VRCAvatarDescriptor>();

			if (descriptor == null) return;

			if (!(my.visualizeGizmo)) return;

			ColliderConfig colliderConfig = new ColliderConfig {
				state = ColliderConfig.State.Custom,
				isMirrored = false,
				transform = my.remapTargetObject.transform,
				radius = my.radius,
				height = my.height,
				position = my.position,
				rotation = my.rotation
			};

			if (!my.customShape)
			{
				switch (my.colliderToRemap)
				{
					case VRChatColliders.Head:
						colliderConfig = descriptor.collider_head;
						break;
					case VRChatColliders.Torso:
						colliderConfig = descriptor.collider_torso;
						break;
					case VRChatColliders.HandLeft:
						colliderConfig = descriptor.collider_handL;
						break;
					case VRChatColliders.HandRight:
						colliderConfig = descriptor.collider_handR;
						break;
					case VRChatColliders.FingerIndexLeft:
						colliderConfig = descriptor.collider_fingerIndexL;
						break;
					case VRChatColliders.FingerIndexRight:
						colliderConfig = descriptor.collider_fingerIndexR;
						break;
					case VRChatColliders.FingerMiddleLeft:
						colliderConfig = descriptor.collider_fingerMiddleL;
						break;
					case VRChatColliders.FingerMiddleRight:
						colliderConfig = descriptor.collider_fingerMiddleR;
						break;
					case VRChatColliders.FingerRingLeft:
						colliderConfig = descriptor.collider_fingerRingL;
						break;
					case VRChatColliders.FingerRingRight:
						colliderConfig = descriptor.collider_fingerRingR;
						break;
					case VRChatColliders.FootLeft:
						colliderConfig = descriptor.collider_footL;
						break;
					case VRChatColliders.FootRight:
						colliderConfig = descriptor.collider_footR;
						break;
					default:
						break;
				}
				colliderConfig.transform = my.remapTargetObject.transform;
			}

			var transform = colliderConfig.transform;
			var radius = colliderConfig.radius;
			var height = colliderConfig.height;
			var position = colliderConfig.position;
			var rotation = colliderConfig.rotation;

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
				var globalPos = transform.TransformPoint(position);
				var globalRot = transform.rotation * rotation;
				HandlesUtil.DrawWireCapsule(globalPos, globalRot, clampedHeight * scale, clampedRadius * scale);
			}
		}

	}
}