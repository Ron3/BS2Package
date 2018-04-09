using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;




// IEndDragHandler, , , 
    // , IPointerEnterHandler, IPointerExitHandler




public class ItemViewEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler{

	public PackageView delegateObj;
	public GameObject itemView;
	public Item itemObj;
	
	///
	///
	public PackageView DelegateObj{
		get {return this.delegateObj;}
		set {this.delegateObj = value;}
	}

	///
	///
	public GameObject ItemView{
		get {return this.itemView;}
		set {this.itemView = value;}
	}

	///
	///
	public Item ItemObj{
		get {return this.itemObj;}
		set {this.itemObj = value;}
	}

	///
	/// 手指点下.开始拖动
	public void OnPointerDown(PointerEventData eventData)
	{
		// Debug.Log("OnPointerDown ... itemId => " + this.itemObj.ItemId);
		if(this.DelegateObj != null){
			this.DelegateObj.OnPointerDown(eventData, this);
		}
	}

	/// Summary
	/// 手指松开.结束拖动
	public void OnPointerUp(PointerEventData eventData)
	{
		if(this.DelegateObj != null){
			this.DelegateObj.OnPointerUp(eventData, this);
		}
	}

	/// Summary
	/// 手指松开.结束拖动
	public void OnDrag(PointerEventData eventData)
	{
		if(this.DelegateObj != null){
			this.DelegateObj.OnDrag(eventData, this);
		}
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
