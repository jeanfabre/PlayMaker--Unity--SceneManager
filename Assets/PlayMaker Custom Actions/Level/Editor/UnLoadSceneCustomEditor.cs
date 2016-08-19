// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(UnloadScene))]
public class UnloadSceneCustomEditor : CustomActionEditor
{
	UnloadScene _target ;

	public override bool OnGUI()
	{
		_target = (UnloadScene)target;

		EditField ("sceneReference");

		if (_target.sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex) {
			EditField ("sceneAtIndex");
		} else {
			EditField ("sceneByName");
		}

		EditField("unloaded");
		EditField("unloadedEvent");
		EditField("failureEventEvent");

		return GUI.changed;
	}
}

#endif