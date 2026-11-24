using nadena.dev.ndmf;
using nadena.dev.ndmf.vrchat;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using ZeniAvatar.Editor;
using ZeniAvatar.Runtime;
using nadena.dev.modular_avatar.editor.ErrorReporting;

[assembly: ExportsPlugin(typeof(ZAConfigureAvatarDescriptorPlugin))]
namespace ZeniAvatar.Editor
{
	public class ZAConfigureAvatarDescriptorPlugin : Plugin<ZAConfigureAvatarDescriptorPlugin>
	{
		public override string QualifiedName => "ZA.ConfigureAvatarDescriptor";
		public override string DisplayName => "Zeni Avatar - Configure Avatar Descriptor";

		protected override void Configure()
		{
			InPhase(BuildPhase.PlatformInit)
				.Run("Configure Avatar Descriptor", ctx =>
				{
					var my = ctx.AvatarRootTransform.GetComponentInChildren<ZAConfigureAvatarDescriptor>(true);
					if (my == null) return;

					var sourceDesc = my.avatarDescriptor;
					var targetDesc = ctx.VRChatAvatarDescriptor();

					if (sourceDesc == targetDesc)
					{
						Debug.LogWarning("Source and target avatar descriptors are the same.");
						return;
					}

					if (ctx.AvatarRootTransform.GetComponentInChildren<ZASetViewPosition>(true) == null)
					{
						targetDesc.ViewPosition = sourceDesc.ViewPosition;
					}

					//Lips
					targetDesc.lipSync = sourceDesc.lipSync;
					targetDesc.VisemeBlendShapes = sourceDesc.VisemeBlendShapes;
					targetDesc.VisemeSkinnedMesh = sourceDesc.VisemeSkinnedMesh;

					//Eyes
					targetDesc.enableEyeLook = sourceDesc.enableEyeLook;
					targetDesc.customEyeLookSettings = sourceDesc.customEyeLookSettings;

					//Facetracking
					targetDesc.enableFaceTracking = sourceDesc.enableFaceTracking;
					targetDesc.useAllTrackingKeys = sourceDesc.useAllTrackingKeys;
					targetDesc.faceTrackingMeshes = sourceDesc.faceTrackingMeshes;
					targetDesc.faceTrackingBlendshapesSetList = sourceDesc.faceTrackingBlendshapesSetList;
					targetDesc.faceTrackingParameterBehaviors = sourceDesc.faceTrackingParameterBehaviors;
					targetDesc.visemeDampenMode = sourceDesc.visemeDampenMode;


					//Misc
					targetDesc.autoFootsteps = sourceDesc.autoFootsteps;
					targetDesc.autoLocomotion = sourceDesc.autoLocomotion;

					//expression (menus)
					targetDesc.expressionsMenu = sourceDesc.expressionsMenu;
					targetDesc.expressionParameters = sourceDesc.expressionParameters;

					Object.DestroyImmediate(sourceDesc.gameObject);
					Object.DestroyImmediate(my); 
				});
		}
	}
}