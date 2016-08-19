// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Loads the scene by its name or index in Build Settings.")]
	public class LoadSceneAsynch : FsmStateAction
	{
		
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		[Tooltip("Allow the scene to be activated as soon as it's ready")]
		public FsmBool allowSceneActivation;

		[Tooltip("lets you tweak in which order async operation calls will be performed. Leave to none for default")]
		public FsmInt operationPriority;

		[ActionSection("Result")]

		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		[Tooltip("Event sent if a problem occured, check log for information")]
		public FsmEvent failureEvent;


		AsyncOperation _asyncOperation;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;
			loadSceneMode = null;
			allowSceneActivation = null;
			operationPriority = new FsmInt() {UseVariable=true};

			isDone = null;
			progress = null;
			doneEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{

			isDone.Value = false;
			progress.Value = 0f;

			bool _result = DoLoadAsynch ();

			if (!_result) {
				Fsm.Event (failureEvent);
				Finish ();
			}

		}


		bool DoLoadAsynch()
		{
			if (sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex)
			{
				if (SceneManager.GetActiveScene ().buildIndex == sceneAtIndex.Value) {
					return false;
				} else {
					_asyncOperation = SceneManager.LoadSceneAsync (sceneAtIndex.Value, (LoadSceneMode)loadSceneMode.Value);
				}

			} else {
				if (SceneManager.GetActiveScene ().name == sceneByName.Value) {
					return false;
				} else {
					_asyncOperation = SceneManager.LoadSceneAsync (sceneByName.Value, (LoadSceneMode)loadSceneMode.Value);
				}
			}

			if (!operationPriority.IsNone) {
				_asyncOperation.priority = operationPriority.Value;
			}

			_asyncOperation.allowSceneActivation = allowSceneActivation.Value;

			return true;
		}

		public override void OnUpdate()
		{
			if (_asyncOperation == null) {
				return;
			}

			if (_asyncOperation.isDone) {
				isDone.Value = true;
				progress.Value = _asyncOperation.progress;

				_asyncOperation = null;

				Fsm.Event (doneEvent);
				Finish ();

			} else {
				progress.Value = _asyncOperation.progress;
			}


		}

		public override void OnExit()
		{
			_asyncOperation = null;
		}

	}
}

#endif