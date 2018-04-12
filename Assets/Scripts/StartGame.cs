using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        
        // GameObject button = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/Container");
        // Debug.Log("Size ==> " + button.BP_SizeDelta());
        // Debug.Log("BP_AnchorMax ==> " + button.BP_AnchorMax());
        // Debug.Log("BP_AnchorMin ==> " + button.BP_AnchorMin());
        // Debug.Log("anchoredPosition ==> " + button.BP_RT().anchoredPosition);
        // Vector2 max = button.BP_AnchorMax();
        // Vector2 min = button.BP_AnchorMin();
        // button.BP_RT().anchorMax = new Vector2(0, 0);
        // button.BP_RT().anchorMin = new Vector2(0, 0);
        // Debug.Log("Size ==>  2222 " + button.BP_SizeDelta());
        // Debug.Log("BP_AnchorMax ==> 2222 " + button.BP_AnchorMax());
        // Debug.Log("BP_AnchorMin ==> 2222 " + button.BP_AnchorMin());
        // button.BP_RT().anchorMax = max;
        // button.BP_RT().anchorMin = min;
        

        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.CENTER);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.CENTER_LEFT);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.CENTER_RIGHT);

        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.BOTTOM_LEFT);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.BOTTOM_CENTER);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.BOTTOM_RIGHT);

        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.TOP_LEFT);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.TOP_CENTER);
        // BPUICommon.SetVisionPositionByBPPos(button, BPUICommon.POSITION.TOP_RIGHT);

        // BPUICommon.SetVisionPositionByPoint(button, 0, 0);
        // Debug.Log("StartGameButton localPosition=> " + button.transform.localPosition);
        // Debug.Log("StartGameButton position=> " + button.transform.position);
        // Debug.Log(button.BP_RT().rect);
        // GameObject canvas = GameObject.Find("Canvas");
        // Debug.Log("canvas localPosition=> " + canvas.transform.localPosition);
        // Debug.Log("canvas position=> " + canvas.transform.position);
        // Debug.Log("canvas rect ==> " + canvas.BP_RT().rect);
        
        // 弹出背包界面
        // UIManager.Instance.ShowPanel("Panel/Package");
        UIManager.Instance.ShowPanel("Panel/Panel");

        // this.TestAutoLayout();
    }

    ///
    /// Summary:
    /// 测试自动布局的
    public void TestAutoLayout()
    {
        float width = 100f, height = 100f;
        GameObject panel = GameObject.Find("Panel");
        // HorizontalLayoutGroup group = panel.GetComponent<HorizontalLayoutGroup>();
        // DestroyImmediate(group);

        // HorizontalLayoutGroup group = panel.AddComponent<HorizontalLayoutGroup>();
        // group.padding = new RectOffset(10, 10, 10, 10);
        // group.childForceExpandWidth = false;
        // group.childForceExpandHeight = false;


        GameObject obj = new GameObject();
        obj.AddComponent<RectTransform>();
        obj.name = "RonObj";
        Image img = obj.AddComponent<Image>();
        img.name = "RonObjImg";
        img.sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/icon_itemslot_blue");

        LayoutElement layout = obj.AddComponent<LayoutElement>();
        layout.name = "layout";
        layout.minWidth = 500f;
        layout.preferredWidth = 600f;

        obj.BP_SetParent(panel);



        // // 创建格子
        // for(int i=0; i < 1; ++i)
        // {
        //     // 1, 创建rank的底图
        //     GameObject rankObj = BPUICommon.CreateGameObjectByContainerName("Panel/Container_image");
        //     rankObj.BP_Image().sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/icon_itemslot_blue");
        //     // rankObj.BP_SetParent(panel);
        //     Debug.Log("rankObj.Rect ==> " + rankObj.BP_Rect());
        //     // BPUICommon.SetRectTransformSize_GameObj(rankObj, width, height);
        // }
    }
}
