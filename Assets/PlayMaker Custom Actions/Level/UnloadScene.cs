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

		public enum SceneReferenceOptions {ActiveScene,SceneAtBuildIndex,SceneAtIndex,SceneByName,SceneByPath,SceneByGameObject};

		[Tooltip("The reference options of the Scene")]
		public SceneReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The build index of the scene to unload.")]
		public FsmInt sceneAtBuildIndex;

		[Tooltip("The index of the scene to unload.")]
		public FsmInt sceneAtIndex;

		[Tooltip("The scene Path.")]
		public FsmString sceneByPath;

		[Tooltip("The GameObject unload scene of")]
		public FsmOwnerDefault sceneByGameObject;


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
			sceneReference = SceneReferenceOptions.SceneAtBuildIndex;
			sceneByName = null;
			sceneAtBuildIndex = null;
			sceneAtIndex = null;
			sceneByPath = null;
			sceneByGameObject = null;

			unloaded = null;
			unloadedEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			bool _unloaded = false;

			try{
				switch (sceneReference) {
				case SceneReferenceOptions.ActiveScene:
					_unloaded = SceneManager.UnloadScene(SceneManager.GetActiveScene());
					break;
				case SceneReferenceOptions.SceneAtBuildIndex:
					_unloaded = SceneManager.UnloadScene(sceneAtBuildIndex.Value);
					break;
				case SceneReferenceOptions.SceneAtIndex:
					_unloaded = SceneManager.UnloadScene(SceneManager.GetSceneAt(sceneAtIndex.Value));
					break;
				case SceneReferenceOptions.SceneByName:
					_unloaded = SceneManager.UnloadScene (sceneByName.Value);
					break;
				case SceneReferenceOptions.SceneByPath:
					_unloaded = SceneManager.UnloadScene(SceneManager.GetSceneByPath(sceneByPath.Value));
					break;
				case SceneReferenceOptions.SceneByGameObject:
					GameObject _go = Fsm.GetOwnerDefaultTarget (sceneByGameObject);
					if (_go==null)
					{
						throw new  Exception ("Null GameObject");
					}else{
						_unloaded = SceneManager.UnloadScene(_go.scene);
					}

					break;
				}
			}catch(Exception e)
			{
				LogError(e.Message);			
			}
							
			if (!unloaded.IsNone)
				unloaded.Value = _unloaded;

			if (_unloaded) {
				Fsm.Event (unloadedEvent);
			} else {
				Fsm.Event (failureEvent);
			}

			Finish();
		}
	}
}

#endif