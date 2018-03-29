using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData: Singleton<GameData> {
	
	static bool m_bIsInit = true;
	List<Item> shopItemList;
	List<Item> playerItemList;

	public void Init()
	{
		if(m_bIsInit == false){
			return;
		}
		
		m_bIsInit = true;
		
		this.shopItemList = new List<Item>();
		this.playerItemList = new List<Item>();

		for(int i = 0; i < 10; ++i){
			Item item = Item.CreateItem(i, string.Format("{0}", i));
			this.shopItemList.Add(item);
		}
	}

	public List<Item> GetShopItemList()
	{
		return shopItemList;
	}
	
}
