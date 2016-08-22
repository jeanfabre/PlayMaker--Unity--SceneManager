// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Move a GameObject from its current scene to a new scene. It is required that the GameObject is at the root of its current scene.")]
	public class MoveGameObjectToScene : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Root GameObject to move to the referenced scene")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[ActionSection("Result")]

		[Tooltip("True if SceneReference resolves to a scene")]
		[UIHint(UIHint.Variable)]
		public FsmBool sceneFound;

		[Tooltip("Event sent if SceneReference resolves to a scene")]
		public FsmEvent sceneFoundEvent;

		[Tooltip("Event sent if SceneReference do not resolve to a scene")]
		public FsmEvent sceneNotFoundEvent;

		Scene _scene;
		bool _sceneFound;

		public override void Reset()
		{
			gameObject = null;

			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;

		}

		public override void OnEnter()
		{
			GetScene ();

			if (_sceneFound) {

				SceneManager.MoveGameObjectToScene(Fsm.GetOwnerDefaultTarget (gameObject), _scene);

				Fsm.Event(sceneFoundEvent);
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
				if (!sceneFound.IsNone) {
					sceneFound.Value = false;
				}
				Fsm.Event(sceneNotFoundEvent);
			} else {
				_sceneFound = true;
				if (!sceneFound.IsNone) {
					sceneFound.Value = true;
				}

			}
		}
	}
}

#endif