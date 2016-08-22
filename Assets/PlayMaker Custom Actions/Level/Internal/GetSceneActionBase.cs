// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class GetSceneActionBase : FsmStateAction
	{
		public enum SceneReferenceOptions {SceneAtIndex,SceneByName,SceneByPath};
		public enum SceneSimpleReferenceOptions {SceneAtIndex,SceneByName};
		public enum SceneBuildReferenceOptions {SceneAtBuildIndex,SceneByName};
		public enum SceneAllReferenceOptions {ActiveScene,SceneAtIndex,SceneByName,SceneByPath,SceneByGameObject};

		[Tooltip("The reference option of the Scene")]
		public SceneAllReferenceOptions sceneReference;

		[Tooltip("The scene Index.")]
		public FsmInt sceneAtIndex;

		[Tooltip("The scene Name.")]
		public FsmString sceneByName;

		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		[Tooltip("The Scene of GameObject")]
		public FsmOwnerDefault sceneByGameObject;

		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool found;

		[Tooltip("Event sent if SceneReference resolves to a scene")]
		public FsmEvent foundEvent;

		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent notFoundEvent;

		[Tooltip("The Scene Cache")]
		protected Scene _scene;

		[Tooltip("True if a scene was found, use _scene to access it")]
		protected bool _sceneFound;

		public override void Reset()
		{
			base.Reset ();

			sceneReference = SceneAllReferenceOptions.ActiveScene;

			sceneAtIndex = null;
			sceneByName = null;
			sceneByPath = null;
			sceneByGameObject = null;

			found = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			try{
				switch (sceneReference) {
				case SceneAllReferenceOptions.ActiveScene:
					_scene = SceneManager.GetActiveScene ();
					break;
				case SceneAllReferenceOptions.SceneAtIndex:
					_scene = SceneManager.GetSceneAt (sceneAtIndex.Value);	
					break;
				case SceneAllReferenceOptions.SceneByName:
					_scene = SceneManager.GetSceneByName (sceneByName.Value);
					break;
				case SceneAllReferenceOptions.SceneByPath:
					_scene = SceneManager.GetSceneByPath (sceneByPath.Value);
					break;
				case SceneAllReferenceOptions.SceneByGameObject:
					GameObject _go = Fsm.GetOwnerDefaultTarget (sceneByGameObject);
					if (_go==null)
					{
						throw new  Exception ("Null GameObject");
					}else{
						_scene =_go.scene;
					}
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