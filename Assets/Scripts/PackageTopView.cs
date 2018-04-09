using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackageTopView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler{

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("PackageTopView  OnPointerDown" + eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("PackageTopView  OnPointerUP" + eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("PackageTopView  OnDrag" + eventData);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
