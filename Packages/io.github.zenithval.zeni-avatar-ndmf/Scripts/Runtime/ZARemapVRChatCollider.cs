/*Bone mapping based on ModularAvatarMergeArmature.cs
 Original Liscense: */
/*
 * MIT License
 *
 * Copyright (c) 2022 bd_
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using UnityEngine;
using nadena.dev.ndmf.runtime;
using nadena.dev.modular_avatar.core;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ZeniAvatar.Runtime
{
	public enum VRChatColliders
	{
		Head,
		Torso,
		HandLeft, HandRight,
		FingerIndexLeft, FingerIndexRight,
		FingerMiddleLeft, FingerMiddleRight,
		FingerRingLeft, FingerRingRight,
		FingerLittleLeft, FingerLittleRight,
		FootLeft, FootRight,
	}

	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("ZA/ZA Remap VRChat Collider")]
	public class ZARemapVRChatCollider : AvatarTagComponent
	{
		//VRChat descriptor Collider to replace
		public VRChatColliders colliderToRemap;

		// Injected by HeuristicBoneMapper
		internal static Func<string, string> NormalizeBoneName;
		internal static ImmutableHashSet<string> AllBoneNames;

		public AvatarObjectReference remapTarget = new AvatarObjectReference();
		public GameObject remapTargetObject => remapTarget.Get(this);

		// Customize Shape
		public bool customShape = false;
		public bool visualizeGizmo = true;
		public float radius = 0.05f;
		public float height = 0.2f;
		public Vector3 position = Vector3.zero;
		public Quaternion rotation = Quaternion.identity;

		//These can probably be removed.
		private string prefix = "";
		private string suffix = "";

		internal static string[][] boneNamePatterns;

		internal Transform MapBone(Transform bone)
		{
			var relPath = RuntimeUtil.RelativePath(gameObject, bone.gameObject);

			if (relPath == null) throw new ArgumentException("Bone is not a child of this component");
			if (relPath == "") return remapTarget.Get(this).transform;

			var segments = relPath.Split('/');

			var pointer = remapTarget.Get(this).transform;
			foreach (var segment in segments)
			{
				if (!segment.StartsWith(prefix) || !segment.EndsWith(suffix)
												|| segment.Length == prefix.Length + suffix.Length) return null;
				var targetObjectName = segment.Substring(prefix.Length,
					segment.Length - prefix.Length - suffix.Length);
				pointer = pointer.Find(targetObjectName);
			}

			return pointer;
		}

		internal Transform FindCorrespondingBone(Transform bone, Transform baseParent)
		{
			var childName = bone.gameObject.name;

			if (!childName.StartsWith(prefix) || !childName.EndsWith(suffix)
											  || childName.Length == prefix.Length + suffix.Length) return null;
			var targetObjectName = childName.Substring(prefix.Length,
				childName.Length - prefix.Length - suffix.Length);
			return baseParent.Find(targetObjectName);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (remapTargetObject == null)
			{
				remapTarget.Set(gameObject);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		public override void ResolveReferences()
		{
			remapTarget?.Get(this);
		}

		public IEnumerable<AvatarObjectReference> GetObjectReferences()
		{
			if (remapTarget != null) yield return remapTarget;
		}
	}
}