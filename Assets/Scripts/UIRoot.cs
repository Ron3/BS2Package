using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class UIRoot : MonoBehaviour {

	void Awake()
	{
		GameData.Instance.Init();

		UIManager.Instance.SetCanvasRoot(this.gameObject);
		UIManager.Instance.Init();

		// GameObject obj = GameObject.Find("StartGameButton");
		// Debug.Log("Button => " + obj.name + " | " + obj.BP_RT().sizeDelta);
		
		// Animator aniObj = obj.GetComponent<Animator>();
		// foreach(System.Reflection.PropertyInfo p in aniObj.GetType().GetProperties())
		// {
		// 	Debug.Log("p.name ==> " + p.Name);
		// }
		
		// RuntimeAnimatorController controller = aniObj.runtimeAnimatorController;
		// Animation aniButton = Utility.AssetRelate.ResourcesLoadCheckNull<AnimatorOverrideController>("button");
		// Debug.Log("aniButton + " + aniButton.name);
		
		// GameObject testButton = GameObject.Find("Button");
		// var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/button.controller");
		// Debug.Log("name ===> " + controller.name);

		// Animator newAni = testButton.AddComponent<Animator>();
		// newAni.runtimeAnimatorController = aniButton;
		// newAni.Rebind();

		// for(int i=0; i<aniButton.animationClips.Length; ++i)
		// {
		// 	Debug.Log(aniButton.animationClips[i].name);
		// }
		
	}

	public void OnStartGameBtnClick()
	{
		// Debug.Log("OnStartGameBtnClick");
		UIManager.Instance.ShowPanel("Panel");

	}
}
