// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Get a scene isLoaded flag. true if the scene is modified. ")]
	public class GetSceneIsLoaded : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("true if the scene is loaded.")]
		public FsmBool isLoaded;

		[Tooltip("Event sent if the scene is loaded.")]
		public FsmEvent isLoadedEvent;

		[Tooltip("Event sent if the scene is not loaded.")]
		public FsmEvent isNotLoadedEvent;

	
		public override void Reset()
		{
			base.Reset ();

			isLoaded = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			DoGetSceneIsLoaded();

			Finish();
		}

		void DoGetSceneIsLoaded()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isLoaded.IsNone) {
				isLoaded.Value = _scene.isLoaded;
			}
		}
	}
}
#endif