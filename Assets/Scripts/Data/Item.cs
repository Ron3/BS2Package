using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Item{
	public const int RANK_WHITE = 0;
	public const int RANK_GREEN = 1;
	public const int RANK_BLUE = 3;
	public const int RANK_PURPLE = 4;
	public const int RANK_ORANGE = 5;
	

	int m_iItemId = 0;
	int m_iStar = 0;
	int m_iPrice = 0;
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

	///
	/// 创建一个item
	public static Item CreateItem(int _itemId, string _itemName, int star=1)
	{
		Item item = new Item();
		item.ItemId = _itemId;
		item.Star = 1;
		item.Price = 100;
		item.Icon = string.Format("item_icon_{0}", 1001 + _itemId);
		item.ItemName = _itemName;
		item.ItemDesc = item.Icon;

		return item;
	}

	///
	/// 创建一个itemView
	///
	public GameObject CreateItemView()
	{
		GameObject itemObj = BPUICommon.CreateGameObject("shopItem");
		Image imgObj =  itemObj.AddComponent<Image>();
		
		Sprite s = Utility.AssetRelate.ResourcesLoadSprite("Images/" + this.Icon);
		Debug.Log("Sprite ==> " + s.name);
		itemObj.GetComponent<Image>().sprite = s;
		// itemObj.GetComponent<Image>().color = new Color(0,0,0,255);



		return itemObj;
	}

}



