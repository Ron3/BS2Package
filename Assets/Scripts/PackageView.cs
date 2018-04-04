using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PackageView : MonoBehaviour {	
	int m_shopGridRow = 4;
	int m_shopGridCol = 5;
	string m_strGridNameFormatProvider = "OneGridView_{0}";
	string m_strShopItemFormatProvider = "ron_shopItem_{0}";

	List<GameObject> m_itemViewList;
	

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
		this.InitShopItem();
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
		GameObject resGridObj = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/OneGridView");
		
		int index = 0;
		List<GameObject> colViewArray = new List<GameObject>();
		List<GameObject> rowViewArray = new List<GameObject>();
		for(int i = 0; i < m_shopGridRow; i++)
		{
			rowViewArray.Clear();
			for(int j=0; j < m_shopGridCol; j++)
			{
				GameObject oneGridView =  GameObject.Instantiate(resGridObj);
				rowViewArray.Add(oneGridView);
				oneGridView.name = string.Format(m_strGridNameFormatProvider, index);
				// Debug.Log("oneGridView.name ==> " + oneGridView.name);
				// 隐藏其中4个格子
				if(index == 10 || index == 11 || index == 15 || index == 16){
					oneGridView.SetActive(false);
				}
				
				// ui调试信息
				oneGridView.BP_AttachText(oneGridView.name);
				
				index += 1;
			}
			
			// 得到一个横向组合的
			GameObject horizontalView = BPUICommon.MakeupView(rowViewArray, BPUICommon.DIRECTION.HORIZONTAL_CENTER, 6f);
			colViewArray.Add(horizontalView);
		}

		// 最后竖向组合.得到一个完整的格子的view
		GameObject gridParentView = GameObject.Find("GridView");
		GameObject gridView = BPUICommon.MakeupView(colViewArray, BPUICommon.DIRECTION.VERTICAL_CENTER, 10f);
		gridView.transform.SetParent(gridParentView.transform);
		BPUICommon.SetVisionPositionByBPPos(gridView, BPUICommon.POSITION.CENTER);
	}

	///
	/// 把商品画上去
	private void InitShopItem()
	{
		this.m_itemViewList = new List<GameObject>();

		int index = 0;
		List<Item> shopItemList = GameData.Instance.GetShopItemList();
		foreach(Item item in shopItemList)
		{
			// Debug.Log("drawItem ==> " + index);
			GameObject itemView = item.CreateItemView(this);
			itemView.name = string.Format(m_strShopItemFormatProvider, index);
			GameObject gridView = GameObject.Find(string.Format(this.m_strGridNameFormatProvider, index));
			itemView.BP_SetParent(gridView);
			index += 1;

			// 最后记录在数组里面.
			this.m_itemViewList.Add(itemView);
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


	/// Summary
	/// 当手指按下某个物品的时候
	public void OnPointerDown(PointerEventData eventData, ItemViewEvent eventObj)
	{
		Debug.Log("PackageView ==> OnPointerDown");
		if(eventObj == null){
			return;
		}

		
		// // 1, 首先找到所有物品的. 然后隐藏他的光环
		// foreach(GameObject obj in this.m_itemViewList)
		// {
		// 	Transform haloObj = obj.transform.Find("halo");
		// 	haloObj.gameObject.SetActive(false);
		// }

		// 2, 在高亮当前的
		Transform currentHaloObj = eventObj.itemView.transform.Find("halo");
		currentHaloObj.gameObject.SetActive(true);

	}


	/// Summary
	/// 手指松开.结束拖动
	public void OnPointerUp(PointerEventData eventData, ItemViewEvent eventObj)
	{
		// Debug.Log("PackageView ==> OnPointerUp");
	}

	/// Summary:
	/// 拖动
	public void OnDrag(PointerEventData eventData, ItemViewEvent eventObj)
	{
		Debug.Log("PackageView ==> OnDrag");
	}
}
