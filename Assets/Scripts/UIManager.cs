using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	/// <param name="name">Name.</param>
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
		BPCommon.SetViewPosition(panel, BPCommon.POSITION.CENTER);
		
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
