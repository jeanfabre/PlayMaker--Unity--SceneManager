// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Send an event when a scene was loaded.")]
	public class SendSceneLoadedEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The event to send when a scene was loaded")]
		public FsmEvent sceneLoaded;

		public override void Reset()
		{
			sceneLoaded = null;
		}

		public override void OnEnter()
		{
			SceneManager.sceneLoaded += SceneManager_sceneLoaded;

			Finish();
		}

		void SceneManager_sceneLoaded (Scene arg0, LoadSceneMode arg1)
		{
			Log ("Scene 0 " + arg0.name + " LoadSceneMode 1" + arg1);
			Fsm.Event (sceneLoaded);

			Finish ();
		}

		public override void OnExit()
		{
			SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		}

	}
}

#endif