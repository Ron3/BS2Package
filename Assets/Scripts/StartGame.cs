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
        // GameObject button = GameObject.Find("StartGameButton");
        // Debug.Log("StartGameButton localPosition=> " + button.transform.localPosition);
        // Debug.Log("StartGameButton position=> " + button.transform.position);
        // Debug.Log(button.BP_RT().rect);

        // GameObject canvas = GameObject.Find("Canvas");
        // Debug.Log("canvas localPosition=> " + canvas.transform.localPosition);
        // Debug.Log("canvas position=> " + canvas.transform.position);
        // Debug.Log("canvas rect ==> " + canvas.BP_RT().rect);
        
        UIManager.Instance.ShowPanel("Panel/Package");
    }
}
