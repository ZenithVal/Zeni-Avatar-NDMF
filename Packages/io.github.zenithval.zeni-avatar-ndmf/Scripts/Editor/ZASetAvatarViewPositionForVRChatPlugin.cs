using nadena.dev.ndmf;
using ZeniAvatar.Editor;
using ZeniAvatar.Runtime;
using UnityEngine;

[assembly: ExportsPlugin(typeof(ZASetAvatarViewPositionForVRChatPlugin))]
namespace ZeniAvatar.Editor
{
	public class ZASetAvatarViewPositionForVRChatPlugin : Plugin<ZASetAvatarViewPositionForVRChatPlugin>
	{
		public override string QualifiedName => "ZA.SetAvatarViewPosition";
		public override string DisplayName => "Zeni Avatar - Set Avatar View Position";

		protected override void Configure()
		{
			InPhase(BuildPhase.Resolving)
				.Run("Set Avatar View Position", ctx =>
				{
					var my = ctx.AvatarRootTransform.GetComponentInChildren<ZASetViewPosition>(true);
					if (my == null) return;

					var position = new Vector3(0, 0, 0);
					if (my.customViewPosition)
					{
						position.x = my.viewPositionX;
						position.y = my.viewPositionY;
						position.z = my.viewPositionZ;
					}
					else
					{
						position = my.transform.position - ctx.AvatarRootTransform.position;
					}

					Debug.Log($"({GetType().Name}) Setting view position to ({position.x:0.000}, {position.y:0.000}, {position.z:0.000}) m");

					ctx.AvatarDescriptor.ViewPosition = position;

					Object.DestroyImmediate(my); 
				});
		}
	}
}