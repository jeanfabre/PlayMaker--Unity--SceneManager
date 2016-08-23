// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetSceneRootGameObjects))]
public class GetSceneRootGameObjectsCustomEditor : GetSceneActionBaseCustomEditor
{
	public override bool OnGUI()
	{
		bool changed = EditSceneReferenceField();

		EditField("rootGameObjects");

		EditSceneReferenceResultFields ();

		return GUI.changed || changed;
	}
}
