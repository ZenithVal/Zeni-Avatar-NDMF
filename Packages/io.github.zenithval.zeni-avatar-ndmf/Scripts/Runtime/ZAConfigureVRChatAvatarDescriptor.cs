using UnityEngine;
using nadena.dev.ndmf;
using VRC.SDK3.Avatars.Components;

namespace ZeniAvatar.Runtime
{
	[AddComponentMenu("ZA/ZA Configure Avatar Descriptor")]
	[DisallowMultipleComponent]
	public class ZAConfigureAvatarDescriptor : MonoBehaviour, INDMFEditorOnly
	{
		public VRCAvatarDescriptor avatarDescriptor;
	}
}