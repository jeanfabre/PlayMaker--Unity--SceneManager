// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Unload Seene. Note that assets are currently not unloaded, in order to free up asset memory call Resources.UnloadUnusedAssets.")]
	public class UnloadScene : FsmStateAction
	{

		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[ActionSection("Result")]

		[Tooltip("True if scene was unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmBool unloaded;

		[Tooltip("Event sent if scene was unloaded ")]
		public FsmEvent unloadedEvent;

		[Tooltip("Event sent scene was not unloaded")]
		[UIHint(UIHint.Variable)]
		public FsmEvent failureEvent;

		Scene _scene;
		bool _sceneFound;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;

			unloaded = null;
			unloadedEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			GetScene ();

			if (_sceneFound) {


				bool _unloaded = SceneManager.UnloadScene (_scene);

				if (!unloaded.IsNone)
					unloaded.Value = _unloaded;

				if (_unloaded) {
					Fsm.Event (unloadedEvent);
				} else {
					Fsm.Event (failureEvent);
				}
			} else {
				if (!unloaded.IsNone) unloaded.Value = false;
				Fsm.Event (failureEvent);
			}
				
			Finish();
		}

		void GetScene()
		{
			try{
				switch (sceneReference) {
				case GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex:
					_scene = SceneManager.GetSceneAt (sceneAtIndex.Value);	
					break;
				case GetSceneActionBase.SceneSimpleReferenceOptions.SceneByName:
					_scene = SceneManager.GetSceneByName (sceneByName.Value);
					break;
				}
			}catch(Exception e) {
				LogError (e.Message);
			}

			if (_scene == new Scene()) {
				_sceneFound = false;
			} else {

			}
		}
	}
}

#endif