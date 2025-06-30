using Object = UnityEngine.Object;
using ZeniAvatar.Runtime;
using ZeniAvatar.Editor;
using nadena.dev.ndmf;
using VRC.SDK3.Avatars.Components;
using static VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using System;
using UnityEditor.Build.Reporting;

[assembly: ExportsPlugin(typeof(ZARemapVRChatColliderPlugin))]
namespace ZeniAvatar.Editor
{
	internal class ZARemapVRChatColliderPlugin : Plugin<ZARemapVRChatColliderPlugin>
	{
		public override string QualifiedName => "ZA.RemapVRChatColliders";
		public override string DisplayName => "Zeni Avatar - Remap VRChat Collider";

		protected override void OnUnhandledException(Exception e)
		{
			ErrorReport.ReportException(e);
		}

		protected override void Configure()
		{
			var seq = InPhase(BuildPhase.Transforming);

			seq
				.BeforePlugin("com.anatawa12.avatar-optimizer")
				.Run("Remap VRChat Colliders", RemapVRChatColliders);
		}

		private void RemapVRChatColliders(BuildContext ctx)
		{
			var remapColliders = ctx.AvatarRootTransform.GetComponentsInChildren<ZARemapVRChatCollider>(true);
			if (remapColliders.Length == 0) return;
			//None found on avatar, skip

			foreach (var my in remapColliders) 
			{
				if (my.customShape)
				{
					ColliderConfig newColliderConfig = new ColliderConfig {
						state = ColliderConfig.State.Custom,
						isMirrored = false,
						transform = my.remapTargetObject.transform,
						radius = my.radius,
						height = my.height,
						position = my.position,
						rotation = my.rotation,
					};

					switch (my.colliderToRemap)
					{
						case VRChatColliders.Head:
							ctx.AvatarDescriptor.collider_head = newColliderConfig;
							break;
						case VRChatColliders.Torso:
							ctx.AvatarDescriptor.collider_torso = newColliderConfig;
							break;
						case VRChatColliders.HandLeft:
							ctx.AvatarDescriptor.collider_handL = newColliderConfig;
							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
							break;
						case VRChatColliders.HandRight:
							ctx.AvatarDescriptor.collider_handR = newColliderConfig;
							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
							break;
						case VRChatColliders.FingerIndexLeft:
							ctx.AvatarDescriptor.collider_fingerIndexL = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
							break;
						case VRChatColliders.FingerIndexRight:
							ctx.AvatarDescriptor.collider_fingerIndexR = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
							break;
						case VRChatColliders.FingerMiddleLeft:
							ctx.AvatarDescriptor.collider_fingerMiddleL = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
							break;
						case VRChatColliders.FingerMiddleRight:
							ctx.AvatarDescriptor.collider_fingerMiddleR = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
							break;
						case VRChatColliders.FingerRingLeft:
							ctx.AvatarDescriptor.collider_fingerRingL = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
							break;
						case VRChatColliders.FingerRingRight:
							ctx.AvatarDescriptor.collider_fingerRingR = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
							break;
						case VRChatColliders.FingerLittleLeft:
							ctx.AvatarDescriptor.collider_fingerLittleL = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
							break;
						case VRChatColliders.FingerLittleRight:
							ctx.AvatarDescriptor.collider_fingerLittleR = newColliderConfig;
							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
							break;
						case VRChatColliders.FootLeft:
							ctx.AvatarDescriptor.collider_footL = newColliderConfig;
							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
							break;
						case VRChatColliders.FootRight:
							ctx.AvatarDescriptor.collider_footR = newColliderConfig;
							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
							break;
					}
				}
				else
				{
					var newTransform = my.remapTargetObject.transform;
					var newState = ColliderConfig.State.Custom;

					switch (my.colliderToRemap)
					{
						case VRChatColliders.Head:
							ctx.AvatarDescriptor.collider_head.transform = newTransform;
							ctx.AvatarDescriptor.collider_head.state = newState;
							break;
						case VRChatColliders.Torso:
							ctx.AvatarDescriptor.collider_torso.transform = newTransform;
							ctx.AvatarDescriptor.collider_torso.state = newState;
							break;
						case VRChatColliders.HandLeft:
							ctx.AvatarDescriptor.collider_handL.transform = newTransform;
							ctx.AvatarDescriptor.collider_handL.state = newState;
							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
							break;
						case VRChatColliders.HandRight:
							ctx.AvatarDescriptor.collider_handR.transform = newTransform;
							ctx.AvatarDescriptor.collider_handR.state = newState;
							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
							break;
						case VRChatColliders.FingerIndexLeft:
							ctx.AvatarDescriptor.collider_fingerIndexL.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerIndexL.state = newState;
							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
							break;
						case VRChatColliders.FingerIndexRight:
							ctx.AvatarDescriptor.collider_fingerIndexR.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerIndexR.state = newState;
							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
							break;
						case VRChatColliders.FingerMiddleLeft:
							ctx.AvatarDescriptor.collider_fingerMiddleL.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerMiddleL.state = newState;
							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
							break;
						case VRChatColliders.FingerMiddleRight:
							ctx.AvatarDescriptor.collider_fingerMiddleR.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerMiddleR.state = newState;
							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
							break;
						case VRChatColliders.FingerRingLeft:
							ctx.AvatarDescriptor.collider_fingerRingL.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerRingL.state = newState;
							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
							break;
						case VRChatColliders.FingerRingRight:
							ctx.AvatarDescriptor.collider_fingerRingR.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerRingR.state = newState;
							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
							break;
						case VRChatColliders.FingerLittleLeft:
							ctx.AvatarDescriptor.collider_fingerLittleL.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerLittleL.state = newState;
							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
							break;
						case VRChatColliders.FingerLittleRight:
							ctx.AvatarDescriptor.collider_fingerLittleR.transform = newTransform;
							ctx.AvatarDescriptor.collider_fingerLittleR.state = newState;
							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
							break;
						case VRChatColliders.FootLeft:
							ctx.AvatarDescriptor.collider_footL.transform = newTransform;
							ctx.AvatarDescriptor.collider_footL.state = newState;
							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
							break;
						case VRChatColliders.FootRight:
							ctx.AvatarDescriptor.collider_footR.transform = newTransform;
							ctx.AvatarDescriptor.collider_footR.state = newState;
							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
							break;
					}
				}

				Object.DestroyImmediate(my);
			}
		}
	}
}

