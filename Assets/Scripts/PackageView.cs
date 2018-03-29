using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageView : MonoBehaviour {	
	int m_shopGridRow = 4;
	int m_shopGridCol = 5;

	void Awake() {
		Debug.Log("PackageView awake");
	}
	
	// Use this for initialization
	void Start () {
		// Debug.Log("PackageView Start. PackageView Rect =>" + this.gameObject.BP_Rect());
		// Debug.Log("PackageView Start. AnchoredPosition ==> " + this.gameObject.BP_AnchoredPosition());
		// Debug.Log("PackageView Start. sizeDetla ==> " + this.gameObject.BP_SizeDelta());
		// Debug.Log("PackageView Start. anchorMax ==> " + this.gameObject.BP_AnchorMax() + " | " + this.gameObject.BP_AnchorMax().x + " | " + this.gameObject.BP_AnchorMax().y);
		// Debug.Log("PackageView Start. offsetMin ==> " + this.gameObject.BP_OffsetMin());
		// Debug.Log("PackageView Start");
		
		this.InitChildView();
		this.InitListenner();
	}
	
	// Update is called once per frame
	void Update () {
		// RectTransform rt = this.GetComponent<RectTransform>();
		// Debug.Log(rt.rect);

	}

	void OnCloseBtnClick(GameObject btn)
	{
		Debug.Log("OnCloseBtnClick");
		UIManager.Instance.ClosePanel("Panel/Package");
		GameObject.Destroy(this.gameObject);
	}

	///
	/// Summary
	/// 动态初始化
	private void InitChildView()
	{
		GameObject shopView = GameObject.Find("GridView");
		GameObject resGridObj = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/OneGridView");
		
		List<GameObject> suvViewArray = new List<GameObject>();
		for(int i = 0; i < m_shopGridRow; i++)
		{
			suvViewArray.Clear();
			Debug.Log("gridViewArray size ==> " + suvViewArray.Count);

			for(int j=0; j < m_shopGridCol; j++)
			{
				GameObject gridView =  GameObject.Instantiate(resGridObj);
				gridView.transform.SetParent(shopView.transform);
				suvViewArray.Add(gridView);
			}

			GameObject parentView = BPCommon.MakeupView(suvViewArray, BPCommon.DIRECTION.HORIZONTAL_CENTER, 6f);
			parentView.BP_RT().SetParent(shopView.transform);
			
			float x = (shopView.BP_Size().x  - parentView.BP_Size().x)/2f;
			BPCommon.SetVisionPositionByPoint(parentView, x, shopView.BP_Size().y  - parentView.BP_Size().y - 10f);
			
		}
	}

	///
	///
	private void InitListenner()
	{
		Debug.Log("PackageView OnCloseBtnClick");
		GameObject closeBtn = GameObject.Find("PackageViewCloseButton");
		Button btn = closeBtn.GetComponent<Button>();
		btn.onClick.AddListener(delegate() {
            this.OnCloseBtnClick(closeBtn);
        });
	}

}
