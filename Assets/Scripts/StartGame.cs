using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
    
	// Use this for initialization
	void Start () 
    {
        string btnName = "StartGameButton";
        GameObject obj = GameObject.Find(btnName);
        Button startGameBtn = obj.GetComponent<Button>();

        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener(delegate() {
            this.OnStartGameBtnClick(startGameBtn);
        });;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void OnStartGameBtnClick(Button btn)
    {
        UIManager.Instance.ShowPanel("Panel/Package");
    }
}
