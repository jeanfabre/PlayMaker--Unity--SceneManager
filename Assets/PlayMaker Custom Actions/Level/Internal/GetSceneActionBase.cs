// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class GetSceneActionBase : FsmStateAction
	{
		public enum SceneReferenceOptions {ActiveScene,SceneAtIndex,SceneByName,SceneByPath};

		[Tooltip("The Scene Cache")]
		protected Scene _scene;

		[Tooltip("True if a scene was found, use _scene to access it")]
		protected bool _sceneFound;

		[Tooltip("The reference option of the Scene")]
		public SceneReferenceOptions sceneReference;

		[Tooltip("The scene Index.")]
		public FsmInt sceneAtIndex;

		[Tooltip("The scene Name.")]
		public FsmString sceneByName;

		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		[Tooltip("True if index resolves to a scene")]
		public FsmBool found;

		[Tooltip("Event sent if index resolves to a scene")]
		public FsmEvent foundEvent;

		[Tooltip("Event sent if index do not resolve to a scene")]
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			base.Reset ();

			sceneReference = SceneReferenceOptions.ActiveScene;

			sceneAtIndex = null;
			sceneByName = null;
			sceneByPath = null;

			found = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			try{
				switch (sceneReference) {
				case SceneReferenceOptions.ActiveScene:
					_scene = SceneManager.GetActiveScene ();
					break;
				case SceneReferenceOptions.SceneAtIndex:
					_scene = SceneManager.GetSceneAt (sceneAtIndex.Value);	
					break;
				case SceneReferenceOptions.SceneByName:
					_scene = SceneManager.GetSceneByName (sceneByName.Value);
					break;
				case SceneReferenceOptions.SceneByPath:
					_scene = SceneManager.GetSceneByPath (sceneByPath.Value);
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
				Fsm.Event(foundEvent);
			}
		}
	}
}
#endif