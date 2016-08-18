// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Get a scene isDirty flag. true if the scene is modified. ")]
	public class GetSceneIsDirty : GetSceneActionBase
	{
		[ActionSection("Result")]

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		[Tooltip("Event sent if the scene is modified.")]
		public FsmEvent isDirtyEvent;

		[Tooltip("Event sent if the scene is unmodified.")]
		public FsmEvent isNotDirtyEvent;

	
		public override void Reset()
		{
			base.Reset ();

			isDirty = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			DoGetSceneIsDirty();

			Finish();
		}

		void DoGetSceneIsDirty()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isDirty.IsNone) {
				isDirty.Value = _scene.isDirty;
			}

			Fsm.Event(foundEvent);
		}
	}
}

#endif