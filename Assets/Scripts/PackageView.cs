using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageView : MonoBehaviour {	
	void Awake() {
		Debug.Log("PackageView awake");
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("PackageView Start. PackageView Rect =>" + this.gameObject.BP_Rect());
		Debug.Log("PackageView Start. AnchoredPosition ==> " + this.gameObject.BP_AnchoredPosition());
		Debug.Log("PackageView Start. sizeDetla ==> " + this.gameObject.BP_SizeDelta());
		Debug.Log("PackageView Start. anchorMax ==> " + this.gameObject.BP_AnchorMax() + " | " + this.gameObject.BP_AnchorMax().x + " | " + this.gameObject.BP_AnchorMax().y);
		Debug.Log("PackageView Start. offsetMin ==> " + this.gameObject.BP_OffsetMin());


		// Debug.Log("PackageView Start");
		// this.InitChildView();
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
	///
	private void InitChildView()
	{
		GameObject shopViewObj = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/ShopView");
		GameObject shopView = GameObject.Instantiate(shopViewObj);
		shopView.transform.SetParent(this.transform);
		

		
		// showViewRT.transform.localPosition = new Vector3(0f, 0f, 1f);
		// Debug.Log("showViewRT ==> " + showViewRT.rect);

		// float shopViewWidth = this.GetComponent<RectTransform>().rect.width * 0.47f;
		// float showViewHeight = this.GetComponent<RectTransform>().rect.height * 0.98f;

		// Debug.Log("shopViewWidth=> "+ shopViewWidth + " showViewHeight => " + showViewHeight);

		// BPCommon.SetRectTransformSize(shopView.GetComponent<RectTransform>(), new Vector2(shopViewWidth, showViewHeight));
		// shopView.GetComponent<RectTransform>().transform.localPosition = new Vector2(50f, 50f);
		// Debug.Log("XXX InitChildView ==> " + shopView.transform.localPosition + " | " + shopView.GetComponent<RectTransform>().rect);
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
