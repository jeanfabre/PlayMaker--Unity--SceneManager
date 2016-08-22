// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Set the scene to be active.")]
	public class SetActiveScene : FsmStateAction
	{
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneReferenceOptions sceneReference;

		[Tooltip("The path of the scene to load.")]
		public FsmString sceneByPath;

		[Tooltip("The name of the scene to load.")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;


		[ActionSection("Result")]

		[Tooltip("True if set active succedded")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("Event sent if setActive succedded ")]
		public FsmEvent successEvent;

		[Tooltip("Event sent if scene not loaded yet")]
		[UIHint(UIHint.Variable)]
		public FsmEvent sceneNotLoadedEvent;

		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent sceneNotFoundEvent;

		Scene _scene;
		bool _sceneFound;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneReferenceOptions.SceneAtIndex;
			sceneByPath = null;
			sceneByName = null;
			sceneAtIndex = null;

			success = null;
			successEvent = null;
			sceneNotLoadedEvent = null;
			sceneNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			GetScene ();

			if (_sceneFound) {

				bool _result = SceneManager.SetActiveScene (_scene);
				if (!success.IsNone)  success.Value = _result;
				if (!_result) {
					Fsm.Event (sceneNotLoadedEvent);
				} else {
					Fsm.Event(successEvent);
				}
					
			}

			Finish();
		}

		void GetScene()
		{
			try{
				switch (sceneReference) {
				case GetSceneActionBase.SceneReferenceOptions.SceneAtIndex:
					_scene = SceneManager.GetSceneAt (sceneAtIndex.Value);	
					break;
				case GetSceneActionBase.SceneReferenceOptions.SceneByName:
					_scene = SceneManager.GetSceneByName (sceneByName.Value);
					break;
				case GetSceneActionBase.SceneReferenceOptions.SceneByPath:
					_scene = SceneManager.GetSceneByPath (sceneByPath.Value);
					break;
				}
			}catch(Exception e) {
				LogError (e.Message);
			}

			if (_scene == new Scene()) {
				_sceneFound = false;
				if (!success.IsNone) success.Value = false;
				Fsm.Event(sceneNotFoundEvent);
			} else {
				_sceneFound = true;
			}

		}
	}
}

#endif