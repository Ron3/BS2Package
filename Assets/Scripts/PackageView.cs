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
	string m_strBagItemFormatProvider = "ron_BagItem_{0}";

	List<GameObject> m_itemViewList;
	List<GameObject> m_bagGridBgItemViewList;				// 背包背景格子
	bool isPassExcute = false;
	
	Vector3 m_oldPos;								// 移动前的位置
	Transform m_oldParentObj;						// 移动前的父窗口
	
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
		this.InitBagGridView();
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
	/// 动态初始化商店的格子
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
	/// Summary
	/// 动态初始化玩家背包格子
	private void InitBagGridView()
	{	
		// 顺便在这里重新初始化链表
		this.m_bagGridBgItemViewList = new List<GameObject>();

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
				oneGridView.name = string.Format(m_strBagItemFormatProvider, index);
				// ui调试信息
				oneGridView.BP_AttachText(oneGridView.name);
				index += 1;

				// 放到列表里面去
				ItemViewEvent eventObj = oneGridView.AddComponent<ItemViewEvent>();
				eventObj.DelegateObj = this;
				eventObj.itemView = oneGridView;
				eventObj.itemObj = null;

				this.m_bagGridBgItemViewList.Add(oneGridView);
			}
			
			// 得到一个横向组合的
			GameObject horizontalView = BPUICommon.MakeupView(rowViewArray, BPUICommon.DIRECTION.HORIZONTAL_CENTER, 6f);
			colViewArray.Add(horizontalView);
		}

		// 最后竖向组合.得到一个完整的格子的view
		Transform bagViewTR = this.transform.Find("BagView");
		Transform gridParentViewTR = bagViewTR.Find("BagGridView");
		// Transform gridParentViewTR = this.transform.FindChild("BagGridView");
		GameObject gridParentView = gridParentViewTR.gameObject;
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

		this.isPassExcute = false;

		// // 1, 首先找到所有物品的. 然后隐藏他的光环
		// foreach(GameObject obj in this.m_itemViewList)
		// {
		// 	Transform haloObj = obj.transform.Find("halo");
		// 	haloObj.gameObject.SetActive(false);
		// 	Debug.Log("haloObj 22222 => " + haloObj.name);
		// }

		// // 2, 在高亮当前的
		// Transform currentHaloObj = eventObj.itemView.transform.Find("halo");
		// currentHaloObj.gameObject.SetActive(true);

		// 3, 隐藏当前的
		// eventObj.itemView.SetActive(false);

		// 直接把这个itemView移到topView上面
		this.m_oldParentObj = eventObj.itemView.transform.parent;
		this.m_oldPos = eventObj.itemView.transform.localPosition;

		Transform topView = this.gameObject.transform.Find("TopView");
		eventObj.itemView.BP_RT().SetParent(topView);

		// 测试遍历自己的子孩子
		// GameObject bagGridView = this.gameObject.BP_Find("BagGridView");
		// bagGridView.SetActive(false);

		
		// 隐藏.可以证明一点.如果是隐藏.则Drag事件无法接手.
		// eventObj.itemView.SetActive(false);
		// Transform topViewRT = this.transform.Find("TopView");
		// Image imgObj = topViewRT.gameObject.GetComponent<Image>();
		// imgObj.raycastTarget = true;

	}


	/// Summary
	/// 手指松开.结束拖动
	public void OnPointerUp(PointerEventData eventData, ItemViewEvent eventObj)
	{
		Debug.Log("PackageView ==> OnPointerUp" + eventData.position + " eventObj=>" + eventObj.name);

		// if(this.isPassExcute == false){
		// 	this.isPassExcute = true;
		// 	BPUICommon.PassEvent(eventData, ExecuteEvents.pointerUpHandler);
		// }

		GameObject bagBgGrid = this.GetHitBgGrid(eventData);
		if(bagBgGrid == null){
			eventObj.itemView.BP_SetParent(this.m_oldParentObj.gameObject);
			eventObj.itemView.transform.localPosition = this.m_oldPos;
			return;
		}
		else
		{
			// 命中购买逻辑.判定是否有足够的格子.如果没有.依然是将物品移动回去

			// 判定是否足够金钱.

			// 到了这里可以购买. 则把物品放入到背包中

			GameObject obj = eventObj.itemView;
			Item itemObj = eventObj.itemObj;
			BuyItem(itemObj);
			GameObject newItemView = itemObj.CreateItemView();
			newItemView.BP_SetParent(bagBgGrid);
			BPUICommon.SetVisionPositionByBPPos(newItemView, BPUICommon.POSITION.CENTER);
			
			// 最后删除
			eventObj.itemView.BP_SetParent(null);
			Destroy(eventObj.itemView);
		}


	}

	/// Summary:
	/// 拖动
	public void OnDrag(PointerEventData eventData, ItemViewEvent eventObj)
	{
		// Debug.Log("PackageView ==> OnDrag" + eventData);
		BPUICommon.SetVisionPositionByPoint(eventObj.itemView, eventData.position);
		// BPUICommon.PassEvent(eventData, ExecuteEvents.dragHandler);
	}


	/// Summary:
	/// 得到命中那个背景格子
	private GameObject GetHitBgGrid(PointerEventData eventData)
	{
		// 检测他命中那个
		List<GameObject> hitObjectList = BPUICommon.GetHitGameObject(eventData);
		foreach(GameObject obj in hitObjectList)
		{
			if(this.m_bagGridBgItemViewList.Contains(obj)){
				return obj;
			}
		}
		return null;
	}

	///
	/// 购买某个物品
	private void BuyItem(Item itemObj)
	{
		

	}
}
