using Object = UnityEngine.Object;
using System.Collections.Generic;
using ZeniAvatar.Runtime;
using ZeniAvatar.Editor;
using nadena.dev.ndmf;
using VRC.SDK3.Avatars.Components;
using static VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using System;
using UnityEngine;
using UnityEditor.Build.Reporting;


//Merged into MA and expanded on as Global Collider, just keeping this here for reference.

// [assembly: ExportsPlugin(typeof(ZARemapVRChatColliderPlugin))]
// namespace ZeniAvatar.Editor
// {
// 	internal class ZARemapVRChatColliderPlugin : Plugin<ZARemapVRChatColliderPlugin>
// 	{
// 		public override string QualifiedName => "ZA.RemapVRChatColliders";
// 		public override string DisplayName => "Zeni Avatar - Remap VRChat Collider";

// 		protected override void OnUnhandledException(Exception e)
// 		{
// 			ErrorReport.ReportException(e);
// 		}

// 		protected override void Configure()
// 		{
// 			var seq = InPhase(BuildPhase.Transforming);

// 			seq
// 				.BeforePlugin("com.anatawa12.avatar-optimizer")
// 				.Run("Remap VRChat Colliders", RemapVRChatColliders);
// 		}

// 		private void RemapVRChatColliders(BuildContext ctx)
// 		{
// 			var remapColliders = ctx.AvatarRootTransform.GetComponentsInChildren<ZARemapVRChatCollider>(true);
// 			if (remapColliders.Length == 0) return;

// 			//Reording so manual remaps run before automatic.
// 			Array.Sort(remapColliders, (a, b) =>
// 			{
// 				if (a.manualRemap && !b.manualRemap) return -1;
// 				if (!a.manualRemap && b.manualRemap) return 1;
// 				return 0;
// 			});

// 			//Keep track of used colliders
// 			var usedColliders = new List<VRChatColliders>();

// 			var ColliderIndex = 0;
// 			foreach (var my in remapColliders)
// 			{
// 				ColliderIndex++;
// 				VRChatColliders targetCollider;
// 				var newColliderConfig = new ColliderConfig
// 				{
// 					state = ColliderConfig.State.Custom,
// 					isMirrored = false,
// 					transform = my.remapTargetObject.transform,
// 					radius = my.radius,
// 					height = my.height,
// 					position = my.position,
// 					rotation = my.rotation,
// 				};

// 				if (my.manualRemap)
// 				{
// 					if (usedColliders.Contains(my.colliderToRemap))
// 					{
// 						//Should report an error here, but it doesn't need to be fatal. The last one will just take priority.
// 						//(I have setups with multiple manual colliders and I use execution order)
// 					}
// 					targetCollider = my.colliderToRemap;
// 				}
// 				else
// 				{
// 					//Find the first unused global collider. Left Right, Ring, Middle, Little, Index
// 					if (!usedColliders.Contains(VRChatColliders.FingerRingLeft)){
// 						targetCollider = VRChatColliders.FingerRingLeft;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerRingRight)){
// 						targetCollider = VRChatColliders.FingerRingRight;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerMiddleLeft)){
// 						targetCollider = VRChatColliders.FingerMiddleLeft;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerMiddleRight)){
// 						targetCollider = VRChatColliders.FingerMiddleRight;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerLittleLeft))
// 					{
// 						targetCollider = VRChatColliders.FingerLittleLeft;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerLittleRight)){
// 						targetCollider = VRChatColliders.FingerLittleRight;
// 					}
// 					//Ideally we should reccomend AGAINST yoinking the index finger since this could result in broken behavior when world dynamics drops.
// 					//Maybe log this as a warning or just don't include them in automatic?
// 					else if (!usedColliders.Contains(VRChatColliders.FingerIndexLeft)){
// 						targetCollider = VRChatColliders.FingerIndexLeft;
// 					}
// 					else if (!usedColliders.Contains(VRChatColliders.FingerIndexRight)){
// 						targetCollider = VRChatColliders.FingerIndexRight;
// 					}
// 					else{
// 						//Should report a build error here. Could be non fatal since we ran out of automatic colliders to use.
// 						//Something like...
// 						//$"[ZARemapVRChatColliderPlugin] Collider {my.colliderToRemap} already used"
// 						continue;
// 					}
// 				}
// 				usedColliders.Add(targetCollider);
				
// 				if (my.customShape)
// 				{
// 					//Finger bones are special
// 					bool isFinger =
// 						!(targetCollider == VRChatColliders.Head ||
// 						  targetCollider == VRChatColliders.Torso ||
// 						  targetCollider == VRChatColliders.HandLeft || targetCollider == VRChatColliders.HandRight ||
// 						  targetCollider == VRChatColliders.FootLeft || targetCollider == VRChatColliders.FootRight);
// 					if (isFinger)
// 					{
// 						//Create an empty inside the target bone with the custom position and rotation
// 						var ColliderRoot = new GameObject($"ZA_ColliderRoot_{targetCollider}_{ColliderIndex}");
// 						ColliderRoot.transform.SetParent(my.remapTargetObject.transform, false);
// 						//position before offsets should be height*-0.5 on y axis
// 						ColliderRoot.transform.localPosition = new Vector3(my.position.x, (my.height * -0.5f) + my.position.y, my.position.z);
// 						ColliderRoot.transform.localRotation = my.rotation;

// 						//Offset the child bone to center it
// 						var ColliderTarget = new GameObject($"ZA_ColliderTarget_{targetCollider}_{ColliderIndex}");
// 						ColliderTarget.transform.SetParent(ColliderRoot.transform, false);
// 						//Always setting y to 0.1 so the collider is in the correct orientation.
// 						ColliderTarget.transform.localPosition = new Vector3(0, 0.1f, 0);
// 						newColliderConfig.transform = ColliderTarget.transform;
// 						newColliderConfig.position = Vector3.zero;
// 						newColliderConfig.rotation = Quaternion.identity;
// 					}

// 					switch (targetCollider)
// 					{
// 						case VRChatColliders.Head:
// 							ctx.AvatarDescriptor.collider_head = newColliderConfig;
// 							break;
// 						case VRChatColliders.Torso:
// 							ctx.AvatarDescriptor.collider_torso = newColliderConfig;
// 							break;
// 						case VRChatColliders.HandLeft:
// 							ctx.AvatarDescriptor.collider_handL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
// 							break;
// 						case VRChatColliders.HandRight:
// 							ctx.AvatarDescriptor.collider_handR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerIndexLeft:
// 							ctx.AvatarDescriptor.collider_fingerIndexL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerIndexRight:
// 							ctx.AvatarDescriptor.collider_fingerIndexR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerMiddleLeft:
// 							ctx.AvatarDescriptor.collider_fingerMiddleL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerMiddleRight:
// 							ctx.AvatarDescriptor.collider_fingerMiddleR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerRingLeft:
// 							ctx.AvatarDescriptor.collider_fingerRingL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerRingRight:
// 							ctx.AvatarDescriptor.collider_fingerRingR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerLittleLeft:
// 							ctx.AvatarDescriptor.collider_fingerLittleL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerLittleRight:
// 							ctx.AvatarDescriptor.collider_fingerLittleR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FootLeft:
// 							ctx.AvatarDescriptor.collider_footL = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FootRight:
// 							ctx.AvatarDescriptor.collider_footR = newColliderConfig;
// 							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
// 							break;
// 					}
// 				}
// 				else
// 				{
// 					var newState = ColliderConfig.State.Custom;
// 					var newTransform = my.remapTargetObject.transform;

// 					switch (targetCollider)
// 					{
// 						case VRChatColliders.Head:
// 							ctx.AvatarDescriptor.collider_head.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_head.state = newState;
// 							break;
// 						case VRChatColliders.Torso:
// 							ctx.AvatarDescriptor.collider_torso.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_torso.state = newState;
// 							break;
// 						case VRChatColliders.HandLeft:
// 							ctx.AvatarDescriptor.collider_handL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_handL.state = newState;
// 							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
// 							break;
// 						case VRChatColliders.HandRight:
// 							ctx.AvatarDescriptor.collider_handR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_handR.state = newState;
// 							ctx.AvatarDescriptor.collider_handR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_handL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerIndexLeft:
// 							ctx.AvatarDescriptor.collider_fingerIndexL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerIndexL.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerIndexRight:
// 							ctx.AvatarDescriptor.collider_fingerIndexR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerIndexR.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerIndexR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerIndexL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerMiddleLeft:
// 							ctx.AvatarDescriptor.collider_fingerMiddleL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerMiddleL.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerMiddleRight:
// 							ctx.AvatarDescriptor.collider_fingerMiddleR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerMiddleR.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerMiddleR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerMiddleL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerRingLeft:
// 							ctx.AvatarDescriptor.collider_fingerRingL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerRingL.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerRingRight:
// 							ctx.AvatarDescriptor.collider_fingerRingR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerRingR.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerRingR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerRingL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerLittleLeft:
// 							ctx.AvatarDescriptor.collider_fingerLittleL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerLittleL.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FingerLittleRight:
// 							ctx.AvatarDescriptor.collider_fingerLittleR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_fingerLittleR.state = newState;
// 							ctx.AvatarDescriptor.collider_fingerLittleR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_fingerLittleL.isMirrored = false;
// 							break;
// 						case VRChatColliders.FootLeft:
// 							ctx.AvatarDescriptor.collider_footL.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_footL.state = newState;
// 							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
// 							break;
// 						case VRChatColliders.FootRight:
// 							ctx.AvatarDescriptor.collider_footR.transform = newTransform;
// 							ctx.AvatarDescriptor.collider_footR.state = newState;
// 							ctx.AvatarDescriptor.collider_footR.isMirrored = false;
// 							ctx.AvatarDescriptor.collider_footL.isMirrored = false;
// 							break;
// 					}
// 				}

// 				Object.DestroyImmediate(my);
// 			}
// 		}
// 	}
// }

