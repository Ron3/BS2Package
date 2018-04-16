using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Animations;
using DG.Tweening;

public class RonPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {

		this.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	/// <summary>
	/// 
	/// </summary>
	public void Init()
	{
		GameObject btn = GameObject.Find("Button");
		Button button = btn.GetComponent<Button>();
		button.onClick.AddListener( delegate(){
            this.OnBtnClick(btn);
		});
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="par"></param>
	public void AnimationTrigger(int args)
	{
		Debug.Log("AnimationTrigger => " + args);
	}


	/// <summary>
	/// 
	/// </summary>
	public void OnBtnClick(GameObject sender)
	{
		GameObject obj = this.gameObject.BP_Find("Child");
		Debug.Log("obj = " + obj.name);
		
		Animator animatorObj = obj.GetComponent<Animator>();
		// Debug.Log("GetCurrentAnimatorClipInfoCount ==> " + animatorObj.GetCurrentAnimatorClipInfoCount(0));

		AnimatorClipInfo[] clipInfoArray = animatorObj.GetCurrentAnimatorClipInfo(0);
		foreach(AnimatorClipInfo info in clipInfoArray)
		{
			AnimationClip clipInfo = info.clip;

			AnimationEvent evt = new AnimationEvent();
			// evt.intParameter = 123;
			evt.time = 0.2f;
			evt.functionName = "ShowMsg";

			clipInfo.AddEvent(evt);
			Debug.Log("AnimatorClipInfo weight => " + info.weight);	
		}

		animatorObj.Play("StartShake", 0);
		animatorObj.SetBool("Shake", true);


		// AnimationClip clip = clipInfoArray[0].clip;


		
		

		Tweener tweener = obj.transform.DOMoveX(100, 1);
		// obj.transform.DORestart();
		// DOTween.Play(obj.transform);
		// tweener.onComplete = delegate(){
		// 	DOTween.ToAlpha(
		// 		()  => obj.GetComponent<Image>().color,
    	// 		(c) => obj.GetComponent<Image>().color = c,
    	// 		0.3f,
    	// 		1.0f
		// 	);
		// };


		
		
		
		
		
		

		// AnimatorClipInfo[] clipInfoArray = animatorObj.GetCurrentAnimatorClipInfo(0);
		// AnimationClip clip = clipInfoArray[0].clip;
		
		
		// Debug.Log("Length => " + clip.length);
		
		

		// string name = animatorObj.GetCurrentAnimatorClipInfo(0)[0].clip.name;
		// Debug.Log("name ==> " + name);
		// animatorObj.Play(name, 0);


		// animatorObj.enabled = false;

		// animatorObj.StartPlayback();
		// animatorObj.playbackTime = 1;
		// Debug.Log("animatorObj.playbackTime ==> " + animatorObj.playbackTime);
		
		// animatorObj.StopPlayback();
		// animatorObj.Play("ShakeAnimation", 0);
		







		// var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath ("Assets/Resources/Panel/Ron.controller");
		// controller.AddParameter("TransitionNow", UnityEngine.AnimatorControllerParameterType.Trigger);
		// controller.AddParameter("Reset", UnityEngine.AnimatorControllerParameterType.Trigger);
		// controller.AddParameter("GotoB1", UnityEngine.AnimatorControllerParameterType.Trigger);
		// controller.AddParameter("GotoC", UnityEngine.AnimatorControllerParameterType.Trigger);

		// // Add StateMachines
		// var rootStateMachine = controller.layers[0].stateMachine;
		// var stateMachineA = rootStateMachine.AddStateMachine("smA");
		// var stateMachineB = rootStateMachine.AddStateMachine("smB");
		// var stateMachineC = stateMachineB.AddStateMachine("smC");
		
		// // Add States
		// var stateA1 = stateMachineA.AddState("stateA1");
		// var stateB1 = stateMachineB.AddState("stateB1");
		// var stateB2 = stateMachineB.AddState("stateB2");
		// stateMachineC.AddState("stateC1");
		// var stateC2 = stateMachineC.AddState("stateC2"); // don’t add an entry transition, should entry to state by default

		// // Add Transitions
		// var exitTransition = stateA1.AddExitTransition();
		// exitTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
		// exitTransition.duration = 0;

		// var resetTransition = stateMachineA.AddAnyStateTransition(stateA1);
		// resetTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "Reset");
		// resetTransition.duration = 0;

		// var transitionB1 = stateMachineB.AddEntryTransition(stateB1);
		// transitionB1.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoB1");
		// stateMachineB.AddEntryTransition(stateB2);
		// stateMachineC.defaultState = stateC2;
		// var exitTransitionC2 = stateC2.AddExitTransition();
		// exitTransitionC2.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "TransitionNow");
		// exitTransitionC2.duration = 0;

		// var stateMachineTransition = rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineC);
		// stateMachineTransition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, "GotoC");
		// rootStateMachine.AddStateMachineTransition(stateMachineA, stateMachineB);

		

		// // 1, 通过反射遍历他的所有属性
		// ForeachClass.ForeachClassProperties(obj.GetComponent<Animator>());
	}

}
