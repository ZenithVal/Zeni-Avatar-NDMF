using Object = UnityEngine.Object;
using ZeniAvatar.Runtime;
using ZeniAvatar.Editor;
using nadena.dev.ndmf;

[assembly: ExportsPlugin(typeof(ZARemapVRChatcolliderPlugin))]
namespace ZeniAvatar.Editor
{
	public class ZARemapVRChatcolliderPlugin : Plugin<ZARemapVRChatcolliderPlugin>
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
							break;
						case VRChatColliders.HandRight:
							ctx.AvatarDescriptor.collider_handR.transform = newTransform;
							break;
						case VRChatColliders.FingerIndexLeft:
							ctx.AvatarDescriptor.collider_fingerIndexL.transform = newTransform;
							break;
						case VRChatColliders.FingerIndexRight:
							ctx.AvatarDescriptor.collider_fingerIndexR.transform = newTransform;
							break;
						case VRChatColliders.FingerMiddleLeft:
							ctx.AvatarDescriptor.collider_fingerMiddleL.transform = newTransform;
							break;
						case VRChatColliders.FingerMiddleRight:
							ctx.AvatarDescriptor.collider_fingerMiddleR.transform = newTransform;
							break;
						case VRChatColliders.FingerRingLeft:
							ctx.AvatarDescriptor.collider_fingerRingL.transform = newTransform;
							break;
						case VRChatColliders.FingerRingRight:
							ctx.AvatarDescriptor.collider_fingerRingR.transform = newTransform;
							break;
						case VRChatColliders.FingerLittleLeft:
							ctx.AvatarDescriptor.collider_fingerLittleL.transform = newTransform;
							break;
						case VRChatColliders.FingerLittleRight:
							ctx.AvatarDescriptor.collider_fingerLittleR.transform = newTransform;
							break;
						case VRChatColliders.FootLeft:
							ctx.AvatarDescriptor.collider_footL.transform = newTransform;
							break;
						case VRChatColliders.FootRight:
							ctx.AvatarDescriptor.collider_footR.transform = newTransform;
							break;
					}

					Object.DestroyImmediate(my);
				});
		}
	}
}

