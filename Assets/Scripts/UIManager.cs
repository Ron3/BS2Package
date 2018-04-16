﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UIManager : Singleton<UIManager> {

	private bool m_IsInit = false;
	private GameObject m_CanvasRoot;
	private Dictionary<string, GameObject> m_PanelDic;

	public void Init()
	{
		if (m_IsInit == false)
		{
			m_PanelDic = new Dictionary<string, GameObject>();
			m_IsInit = true;
		}
	}

	/// <summary>
	/// Sets the canvas root.
	/// </summary>
	/// <param name="canvas">GameObject.</param>
	public void SetCanvasRoot(GameObject canvas)
	{
		if (m_CanvasRoot == null)
		{
			m_CanvasRoot = canvas;
		} 
		else 
		{
			Debug.Log("SetCanvasRoot Exception => " + m_CanvasRoot.GetInstanceID());
		}
	}

	/// <summary>
	/// Shows the panel.
	/// </summary>
	public void ShowPanel(string name)
	{
        if (this.isPanelLive(name) == true)
        {
            Debug.Log("[0] Panel is living. name=" + name);
            return;
        }

        GameObject obj = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>(name);
		if(obj == null)
		{
			return;
		}

        GameObject panel = GameObject.Instantiate(obj);
		m_PanelDic.Add(name, panel);
		panel.transform.SetParent(m_CanvasRoot.transform);

		GameObject canvas = GameObject.Find("Canvas");
		float width = 1334f;
		float height = 750f;

		panel.BP_RT().offsetMin = new Vector2(100f, 100f);
		panel.BP_RT().offsetMax = new Vector2(-100f, -100f);

		Debug.Log("Rect ==> " + panel.BP_Rect());
		

		// Debug.Log("localPosition =======>" + panel.BP_RT().localPosition);
		// panel.BP_RT().localPosition = new Vector3(0, -height/2.0f, 0);
		// panel.transform.DOMoveY(height/2.0f, 0.5f);

		// Debug.Log("panel anchoredPosition==>" + panel.BP_RT().anchoredPosition);
		// Debug.Log("canvas ==> " + width + " | " + height + "  panelAnchorMax=>  " + panel.BP_RT().anchorMax + "|" + panel.BP_RT().anchorMin);

		Debug.Log("Panel localPosition ==> " + panel.BP_LocalPosition());
		Debug.Log("Panel Rect ===> " + panel.BP_RT().rect + " | " + panel.BP_Pivot());
		GameObject priceView = panel.BP_Find("PriceView");
		Debug.Log("priceView rect ==> " + priceView.BP_RT().rect);
		
		// Debug.Log("ShopBgGridView Rect ===> " + panel.BP_Find("ShopBgGridView").BP_RT().rect);
		// BPUICommon.SetVisionPositionByBPPos(panel, BPUICommon.POSITION.CENTER);
		// Resolution[] resolutions = Screen.resolutions;
        // Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
		// float height = resolutions[0].height;
		// float width = resolutions[0].width;
		// Debug.Log("Screen height ==> " + height + "  width => " + width);
		// BPUICommon.SetRectTransformSize(panel.GetComponent<RectTransform>(), m_CanvasRoot.BP_Size());
		// RectTransform panelRectTransform = panel.GetComponent<RectTransform>();
		// Debug.Log(panel.transform.localPosition + " | " + panelRectTransform.pivot);
		// GameObject btn = GameObject.Find("StartGameButton");
		// Debug.Log("btn ==> " + btn.transform.localPosition);
		// // m_str
		// RectTransform canvasTransform = m_CanvasRoot.GetComponent<RectTransform>();
		// Debug.Log("canvasTransform.pivot ==> " + canvasTransform.pivot);
	}

    /// <summary>
    /// </summary>
    public bool isPanelLive(string name)
    {
        return m_PanelDic.ContainsKey(name);
    }

    public void ClosePanel(string name)
    {
        if (this.isPanelLive(name) == false)
        {
            return;
        }

        GameObject panel = m_PanelDic.TryGet(name);
		panel.transform.SetParent(null);
		m_PanelDic.Remove(name);
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
