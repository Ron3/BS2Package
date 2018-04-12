using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour {

	NotificationCenter notifyCenter;

	// Use this for initialization
	void Start () {
		this.Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	///
	/// 
	///
	public void Init()
	{
		GameObject button = this.gameObject.BP_Find("Button");
		Button buttonComponent = button.GetComponent<Button>();
		buttonComponent.onClick.AddListener(delegate() {
            this.OnBtnClick(button);
        });

		// 注册任务
		this.notifyCenter = new NotificationCenter();
		this.notifyCenter.AddObserver(this, "Finish");
	}

	///
	///
	///
	public void OnBtnClick(GameObject sender)
	{
		Debug.Log("OnBtnClick ===> ");
		Hashtable data = new Hashtable();
		data.Add(1, 1);
		data.Add("2", 2);

		this.notifyCenter.PostNotification(this, "Finish", data);
	}

	
	///
	/// Summary:
	/// 这里一点要注意.因为是在遍历那边的数据结构.如果直接又调用删除.直接会导致崩溃
	public IEnumerator Finish(NotificationCenter.Notification notifyData)
	{
		Debug.Log("Finish func call");
		IDictionaryEnumerator myEnumerator = notifyData.data.GetEnumerator();  
		while (myEnumerator.MoveNext())
		{
			Debug.Log("type ===>" + myEnumerator.Key.GetType());
			Debug.Log(myEnumerator.Key + " | " + notifyData.data[myEnumerator.Key]);
		}

		// yield return new WaitForSeconds(0.01f);
		// NotificationCenter.DefaultCenter.RemoveObserver(this, "Finish");
		return null;
	}
}
