using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageView : MonoBehaviour {

	void Awake() {
		Debug.Log("PackageView awake");
	}
	
	// Use this for initialization
	void Start () {

		Debug.Log("PackageView OnCloseBtnClick");
		GameObject closeBtn = GameObject.Find("PackageViewCloseButton");
		Button btn = closeBtn.GetComponent<Button>();
		btn.onClick.AddListener(delegate() {
            this.OnCloseBtnClick(closeBtn);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCloseBtnClick(GameObject btn)
	{
		Debug.Log("OnCloseBtnClick");
		UIManager.Instance.ClosePanel("Panel/Package");
		GameObject.Destroy(this.gameObject);
	}

}
