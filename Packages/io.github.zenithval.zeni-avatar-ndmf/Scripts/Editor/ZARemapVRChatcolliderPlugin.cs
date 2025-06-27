using Object = UnityEngine.Object;
using ZeniAvatar.Runtime;
using ZeniAvatar.Editor;
using nadena.dev.ndmf;
using VRC.SDK3.Avatars.Components;
using static VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;

[assembly: ExportsPlugin(typeof(ZARemapVRChatColliderPlugin))]
namespace ZeniAvatar.Editor
{
	public class ZARemapVRChatColliderPlugin : Plugin<ZARemapVRChatColliderPlugin>
	{
		public override string QualifiedName => "ZA.RemapVRChatColliders";
		public override string DisplayName => "Zeni Avatar - Remap VRChat Collider";

		protected override void Configure()
		{
			InPhase(BuildPhase.Resolving)
				.Run("Remap VRChat Colliders", ctx =>
				{
					var my = ctx.AvatarRootTransform.GetComponentInChildren<ZARemapVRChatCollider>(true);
					if (my == null) return;

					var colliderToRemap = my.colliderToRemap;

					if (!my.customShape)
					{
						ColliderConfig colliderConfigTarget = new ColliderConfig {
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
								ctx.AvatarDescriptor.collider_head = colliderConfigTarget;
								break;
							case VRChatColliders.Torso:
								ctx.AvatarDescriptor.collider_torso = colliderConfigTarget;
								break;
							case VRChatColliders.HandLeft:
								ctx.AvatarDescriptor.collider_handL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_handR.isMirrored = false;
								break;
							case VRChatColliders.HandRight:
								ctx.AvatarDescriptor.collider_handR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_handL.isMirrored = false;
								break;
							case VRChatColliders.FingerIndexLeft:
								ctx.AvatarDescriptor.collider_fingerIndexL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
								break;
							case VRChatColliders.FingerIndexRight:
								ctx.AvatarDescriptor.collider_fingerIndexR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
								break;
							case VRChatColliders.FingerMiddleLeft:
								ctx.AvatarDescriptor.collider_fingerMiddleL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
								break;
							case VRChatColliders.FingerMiddleRight:
								ctx.AvatarDescriptor.collider_fingerMiddleR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
								break;
							case VRChatColliders.FingerRingLeft:
								ctx.AvatarDescriptor.collider_fingerRingL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
								break;
							case VRChatColliders.FingerRingRight:
								ctx.AvatarDescriptor.collider_fingerRingR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
								break;
							case VRChatColliders.FingerLittleLeft:
								ctx.AvatarDescriptor.collider_fingerLittleL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
								break;
							case VRChatColliders.FingerLittleRight:
								ctx.AvatarDescriptor.collider_fingerLittleR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
								break;
							case VRChatColliders.FootLeft:
								ctx.AvatarDescriptor.collider_footL = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_footR.isMirrored = false;
								break;
							case VRChatColliders.FootRight:
								ctx.AvatarDescriptor.collider_footR = colliderConfigTarget;
								ctx.AvatarDescriptor.collider_footL.isMirrored = false;
								break;
						}
					}
					else
					{
						var newTransform = my.remapTargetObject.transform;

						switch (my.colliderToRemap)
						{
							case VRChatColliders.Head:
								ctx.AvatarDescriptor.collider_head.transform = newTransform;
								break;
							case VRChatColliders.Torso:
								ctx.AvatarDescriptor.collider_torso.transform = newTransform;
								break;
							case VRChatColliders.HandLeft:
								ctx.AvatarDescriptor.collider_handL.transform = newTransform;
								ctx.AvatarDescriptor.collider_handR.isMirrored = false;
								break;
							case VRChatColliders.HandRight:
								ctx.AvatarDescriptor.collider_handR.transform = newTransform;
								ctx.AvatarDescriptor.collider_handL.isMirrored = false;
								break;
							case VRChatColliders.FingerIndexLeft:
								ctx.AvatarDescriptor.collider_fingerIndexL.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
								break;
							case VRChatColliders.FingerIndexRight:
								ctx.AvatarDescriptor.collider_fingerIndexR.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
								break;
							case VRChatColliders.FingerMiddleLeft:
								ctx.AvatarDescriptor.collider_fingerMiddleL.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
								break;
							case VRChatColliders.FingerMiddleRight:
								ctx.AvatarDescriptor.collider_fingerMiddleR.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
								break;
							case VRChatColliders.FingerRingLeft:
								ctx.AvatarDescriptor.collider_fingerRingL.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
								break;
							case VRChatColliders.FingerRingRight:
								ctx.AvatarDescriptor.collider_fingerRingR.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
								break;
							case VRChatColliders.FingerLittleLeft:
								ctx.AvatarDescriptor.collider_fingerLittleL.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
								break;
							case VRChatColliders.FingerLittleRight:
								ctx.AvatarDescriptor.collider_fingerLittleR.transform = newTransform;
								ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
								break;
							case VRChatColliders.FootLeft:
								ctx.AvatarDescriptor.collider_footL.transform = newTransform;
								ctx.AvatarDescriptor.collider_footR.isMirrored = false;
								break;
							case VRChatColliders.FootRight:
								ctx.AvatarDescriptor.collider_footR.transform = newTransform;
								ctx.AvatarDescriptor.collider_footL.isMirrored = false;
								break;
						}
					}

					Object.DestroyImmediate(my);
				});
		}
	}
}

