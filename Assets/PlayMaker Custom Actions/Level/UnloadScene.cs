// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Unload Seene. Note that assets are currently not unloaded, in order to free up asset memory call Resources.UnloadUnusedAssets.")]
	public class UnloadScene : GetSceneActionBase
	{

		[ActionSection("Result")]

		[Tooltip("True if scene was unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmBool unloaded;

		[Tooltip("Event sent if scene was unloaded ")]
		public FsmEvent unloadedEvent;

		[Tooltip("Event sent scene was not unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			base.Reset ();
			unloaded = null;
			unloadedEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			if (_sceneFound) {
				
				bool _unloaded = SceneManager.UnloadScene (_scene);

				if (!unloaded.IsNone) unloaded.Value = _unloaded;

				if (_unloaded) {
					Fsm.Event (unloadedEvent);
				} else {
					Fsm.Event (failureEvent);
				}
			}

			Finish();
		}

	}
}

#endif