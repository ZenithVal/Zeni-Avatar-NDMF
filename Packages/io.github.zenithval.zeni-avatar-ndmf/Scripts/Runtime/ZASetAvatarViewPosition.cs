using UnityEngine;
using nadena.dev.ndmf;

namespace ZeniAvatar.Runtime
{
	[AddComponentMenu("ZA/ZA Set View Position")]
	[DisallowMultipleComponent]
	public class ZASetViewPosition : MonoBehaviour, INDMFEditorOnly
	{
		public bool customViewPosition;
		public float viewPositionX = 0f;
		public float viewPositionY = 0f;
		public float viewPositionZ = 0f;
	}
}