﻿// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetSceneName))]
public class GetSceneNameCustomEditor : GetSceneActionBaseCustomEditor
{
	public override bool OnGUI()
	{
		bool changed = EditSceneReferenceField();

		EditField("name");

		EditSceneReferenceResultFields ();

		return GUI.changed || changed;
	}
}
