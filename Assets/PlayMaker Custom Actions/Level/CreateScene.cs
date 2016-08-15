// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Level)]
	[Tooltip("Create an empty new scene with the given name additively")]
	public class CreateScene : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmString sceneName;
		 
	
		public override void Reset()
		{
			sceneName = null;
		}

		public override void OnEnter()
		{
			#if UNITY_5_3_OR_NEWER

			SceneManager.CreateScene(sceneName.Value);

			#else

			Debug.LogWarning("<b>[CreateScene]</b><color=#FF9900ff> Need minimum unity 5.3</color>", this.Owner);
			#endif

			Finish();
		}


	}
}
