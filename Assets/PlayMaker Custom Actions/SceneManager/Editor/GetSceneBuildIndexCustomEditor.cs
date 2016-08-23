// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetSceneBuildIndex))]
public class GetSceneBuildIndexCustomEditor : GetSceneActionBaseCustomEditor
{
	public override bool OnGUI()
	{
		bool changed = EditSceneReferenceField();

		EditField("buildIndex");

		EditSceneReferenceResultFields ();

		return GUI.changed || changed;
	}
}
