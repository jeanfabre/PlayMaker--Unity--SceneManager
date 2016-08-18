﻿// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


public class GetSceneActionBaseCustomEditor : CustomActionEditor {

	public override bool OnGUI()
	{
		return false;
	}

	GetSceneActionBase _action;

	public bool EditSceneReferenceField()
	{
		_action = (GetSceneActionBase)target;
	
		EditField ("sceneReference");

		switch (_action.sceneReference) {
		case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		}
			
		return GUI.changed;
	}

	public void EditSceneReferenceResultFields()
	{
		EditField ("found");
		EditField ("foundEvent");
		EditField ("notFoundEvent");
	}


}