#if UNITY_5_4_OR_NEWER
using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;


public class PlayMakerSceneManagerProxy : MonoBehaviour {

	public static string SceneLoadedEvent = "SCENE MANAGER / SCENE LOADED";
	public static string SceneUnLoadedEvent = "SCENE MANAGER / SCENE UNLOADED";
	public static string ActiveSceneChangedEvent = "SCENE MANAGER / ACTIVE SCENE CHANGED";

	public bool Debug = false;

	// Use this for initialization
	void OnEnable () {
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
		SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
	}

	void SceneManager_activeSceneChanged (Scene previousActiveScene , Scene activeScene)
	{
		if (Debug) UnityEngine.Debug.Log ("SceneManager_activeSceneChanged:\n Previous Scene:" + previousActiveScene.name + " New Scene:" + activeScene.name);

		SendActiveSceneChangedEvent.lastNewActiveScene = activeScene;
		SendActiveSceneChangedEvent.lastPreviousActiveScene = previousActiveScene;
		PlayMakerFSM.BroadcastEvent(SceneUnLoadedEvent);
	}


	void SceneManager_sceneUnloaded (Scene scene)
	{
		if (Debug) UnityEngine.Debug.Log ("SceneManager_sceneUnloaded:\n " + scene.name);

		SendSceneUnloadedEvent.lastUnLoadedScene = scene;
		PlayMakerFSM.BroadcastEvent(SceneUnLoadedEvent);
	}
	

	void SceneManager_sceneLoaded (Scene scene, LoadSceneMode mode)
	{
		if (Debug) UnityEngine.Debug.Log ("SceneManager_sceneLoaded:\n Scene:" + scene.name+" Mode"+mode);

		SendSceneLoadedEvent.lastLoadedScene = scene;
		SendSceneLoadedEvent.lastLoadedMode = mode;
		PlayMakerFSM.BroadcastEvent(SceneLoadedEvent);
	}

}
#endif