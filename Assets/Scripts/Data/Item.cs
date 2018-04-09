using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Item{
	public const int RANK_WHITE = 0;
	public const int RANK_GREEN = 1;
	public const int RANK_BLUE = 2;
	public const int RANK_PURPLE = 3;
	public const int RANK_ORANGE = 4;
	

	int m_iItemId = 0;
	int m_iStar = 0;
	int m_iMaxStar = 5;
	int m_iPrice = 0;
	int m_iRank = 1;
	string m_strIcon = "";
	string m_stritemName = "";
	string m_strItemDesc = "";
	
	///
	/// 构造函数
	Item()
	{

	}

	public int ItemId {
		get {return m_iItemId;}
		set{m_iItemId = value;}
	 }

	public int Star{
		get {return m_iStar;}
		set {m_iStar = value;}
	}

	public int Price{
		get {return m_iPrice;}
		set {m_iPrice = value;}
	}

	public string Icon{
		get {return m_strIcon;}
		set {m_strIcon = value;}
	}

	public string ItemName{
		get {return m_stritemName;}
		set {m_stritemName = value;}
	}

	public string ItemDesc{
		get {return m_strItemDesc;}
		set {m_strItemDesc = value;}
	}

	public int Rank{
		get {return m_iRank;}
		set {m_iRank = value;}
	}

	public int MaxStar{
		get {return m_iMaxStar;}
		set {m_iMaxStar = value;}
	}

	///
	/// 创建一个item
	public static Item CreateItem(int _itemId, string _itemName, int star=1)
	{
		System.Random rd = new System.Random();

		Item item = new Item();
		item.ItemId = _itemId;
		item.Star = 1;
		item.Price = 100;
		item.Icon = string.Format("item_icon_{0}", 1001 + _itemId);
		item.ItemName = _itemName;
		item.ItemDesc = item.Icon;
		item.Rank = rd.Next(RANK_GREEN, RANK_ORANGE);

		return item;
	}

	///
	/// 根据Rank加载对应的图片
	/// 
	public Sprite loadRankBgImageByRank(int rank)
	{
		string bgImageName = "";
		if(rank == RANK_WHITE){
			bgImageName = "";
		}
		else if(rank == RANK_GREEN){
			bgImageName = "icon_itemslot_green";
		}
		else if(rank == RANK_BLUE){
			bgImageName = "icon_itemslot_blue";
		}
		else if(rank == RANK_PURPLE){
			bgImageName = "icon_itemslot_purple";
		}
		else if(rank == RANK_ORANGE){
			bgImageName = "icon_itemslot_orange";
		}
		return Utility.AssetRelate.ResourcesLoadSprite("Images/" + bgImageName);
	}


	///
	/// 创建一个itemView
	///
	public GameObject CreateItemView(PackageView delegateObj = null, float width=100f, float height=100f)
	{
		// 1, 创建rank的底图
		GameObject rankObj = BPUICommon.CreateGameObjectByContainerName("Panel/Container_image");
		rankObj.BP_Image().sprite = loadRankBgImageByRank(this.Rank);
		BPUICommon.SetRectTransformSize_GameObj(rankObj, width, height);

		// 2, 创建物品icon
		GameObject itemObj = BPUICommon.CreateGameObjectByContainerName("Panel/Container_image");
		itemObj.BP_Image().sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/" + this.Icon);
		BPUICommon.SetRectTransformSize_GameObj(itemObj, width * 0.95f, height * 0.95f);
		itemObj.BP_SetParent(rankObj);

		// 3, 创建星星
		List<GameObject>starViewList =  new List<GameObject>();
		for(int i=0; i < this.MaxStar; i++)
		{
			GameObject starObj = BPUICommon.CreateGameObjectByContainerName("Panel/Container_image");
			BPUICommon.SetRectTransformSize_GameObj(starObj, height/5.0f, height/5.0f);
			// 这种是画实心的星星.
			if(this.Star > i){
				starObj.BP_Image().sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/icon_star");
			}
			else{
				starObj.BP_Image().sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/icon_star_2");
			}
			starViewList.Add(starObj);
			
		}
		
		GameObject startView = BPUICommon.MakeupView(starViewList, BPUICommon.DIRECTION.VERTICAL_CENTER, 0);
		startView.BP_SetParent(rankObj);
		BPUICommon.SetVisionPositionByBPPos(startView, BPUICommon.POSITION.CENTER_RIGHT);

		// // 4, 创建光环
		// GameObject haloObj = BPUICommon.CreateGameObjectByContainerName("Component/HaloButton");
		// // haloObj.BP_Image().sprite = Utility.AssetRelate.ResourcesLoadSprite("Images/outline_square");
		// // haloObj.BP_Image().type = Image.Type.Filled;
		// // haloObj.SetActive(false);
		// BPUICommon.SetRectTransformSize_GameObj(haloObj, width, height);
		// haloObj.BP_SetParent(rankObj);
		// haloObj.name = "halo";
		// haloObj.transform.name = "halo";

		// BoxCollider2D collider = rankObj.AddComponent<BoxCollider2D>();
				
		//  5, 给他一个事件处理的回调入口
		ItemViewEvent eventObj = rankObj.AddComponent<ItemViewEvent>();
		eventObj.DelegateObj = delegateObj;
		eventObj.itemView = rankObj;
		eventObj.itemObj = this;
		
		return rankObj;
	}

}



