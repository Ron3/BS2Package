using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour {

	void Awake()
	{
		GameData.Instance.Init();

		UIManager.Instance.SetCanvasRoot(this.gameObject);
		UIManager.Instance.Init();
	}

	public void OnStartGameBtnClick()
	{
		UIManager.Instance.ShowPanel("Panel");
	}
}
