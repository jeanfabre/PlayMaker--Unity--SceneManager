// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("Get a scene isValid flag. A scene may be invalid if, for example, you tried to open a scene that does not exist. In this case, the scene returned from EditorSceneManager.OpenScene would return False for IsValid. ")]
	public class GetSceneIsValid : GetSceneActionBase
	{
		[ActionSection("Result")]

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is loaded.")]
		public FsmBool isValid;

		[Tooltip("Event sent if the scene is valid.")]
		public FsmEvent isValidEvent;

		[Tooltip("Event sent if the scene is not valid.")]
		public FsmEvent isNozValidEvent;

	
		public override void Reset()
		{
			base.Reset ();

			isValid = null;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			DoGetSceneIsValid();

			Finish();
		}

		void DoGetSceneIsValid()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isValid.IsNone) {
				isValid.Value = _scene.IsValid();
			}

			Fsm.Event(foundEvent);
		}
	}
}
#endif