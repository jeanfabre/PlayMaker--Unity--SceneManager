// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Loads the scene by its name or index in Build Settings. ")]
	public class LoadScene : FsmStateAction
	{
		
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[Tooltip("If true, Load Scene asynchronously.")]
		public FsmBool asynch;

		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		[ActionSection("Result")]

		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool found;

		[Tooltip("Event sent if SceneReference resolves to a scene")]
		public FsmEvent foundEvent;

		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent notFoundEvent;

		Scene _scene;
		bool _sceneFound;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;
			asynch = null;
			loadSceneMode = null;
		}

		public override void OnEnter()
		{
			GetScene ();

			if (_sceneFound) {
				
				if (asynch.Value) {
					if (sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex) {
						SceneManager.LoadSceneAsync(sceneAtIndex.Value, (LoadSceneMode)loadSceneMode.Value);
					} else {
						SceneManager.LoadSceneAsync(sceneByName.Value, (LoadSceneMode)loadSceneMode.Value);
					}
				} else {
					if (sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex) {
						SceneManager.LoadScene(sceneAtIndex.Value, (LoadSceneMode)loadSceneMode.Value);
					} else {
						SceneManager.LoadScene(sceneByName.Value, (LoadSceneMode)loadSceneMode.Value);
					}
				}

				Fsm.Event(foundEvent);
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
				if (!found.IsNone) {
					found.Value = false;
				}
				Fsm.Event(notFoundEvent);
			} else {
				_sceneFound = true;
				if (!found.IsNone) {
					found.Value = true;
				}

			}

		}

	}
}

#endif