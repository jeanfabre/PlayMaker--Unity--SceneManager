// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("SceneManager")]
	[Tooltip("This will merge the source scene into the destinationScene. This function merges the contents of the source scene into the destination scene, and deletes the source scene. All GameObjects at the root of the source scene are moved to the root of the destination scene. NOTE: This function is destructive: The source scene will be destroyed once the merge has been completed.")]
	public class MergeScenes : GetSceneActionBase
	{
		
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneAllReferenceOptions scene2Reference;

		[Tooltip("The scene Index.")]
		public FsmInt scene2AtIndex;

		[Tooltip("The scene Name.")]
		public FsmString scene2ByName;

		[Tooltip("The scene Path.")]
		public FsmString scene2ByPath;


		Scene _scene2;
		bool _scene2Found;

		public override void Reset()
		{
			scene2Reference = GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex;
			scene2ByPath = null;
			scene2ByName = null;
			scene2AtIndex = null;

		}

		public override void OnEnter()
		{
			GetScene2 ();

			if (_sceneFound) {

				SceneManager.MergeScenes (_scene, _scene2);

				Fsm.Event(foundEvent);
			}

			Finish();
		}

		void GetScene2()
		{
			try{
				switch (scene2Reference) {
				case SceneAllReferenceOptions.ActiveScene:
					_scene2 = SceneManager.GetActiveScene ();
					break;
				case SceneAllReferenceOptions.SceneAtIndex:
					_scene2 = SceneManager.GetSceneAt (scene2AtIndex.Value);	
					break;
				case SceneAllReferenceOptions.SceneByName:
					_scene2 = SceneManager.GetSceneByName (scene2ByName.Value);
					break;
				case SceneAllReferenceOptions.SceneByPath:
					_scene2 = SceneManager.GetSceneByPath (scene2ByPath.Value);
					break;
				}
			}catch(Exception e) {
				LogError (e.Message);
			}

			if (_scene2 == new Scene()) {
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