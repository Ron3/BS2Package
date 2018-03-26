using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///
///
public static class DictionaryExtension
{
	public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}


public class BPCommon
{
    public enum POSITION {CENTER, LEFT_BOTTOM};

    ///
    /// 设置坐标
    public static void SetViewPosition(GameObject view, POSITION targetPos)
    {
        if(view == null){
            return;
        }

        GameObject parentView = view.transform.parent.gameObject;
        if(parentView == null){
            return;
        }

        RectTransform childRect = view.GetComponent<RectTransform>();
        RectTransform parenetRect = parentView.GetComponent<RectTransform>();
        if(targetPos == BPCommon.POSITION.CENTER)
        {
            int width = (int)(parenetRect.rect.width - childRect.rect.width) / 2;
            int height = (int)(parenetRect.rect.height - childRect.rect.height) / 2;
            
            childRect.transform.localPosition = new Vector3(0f, 0f, 0f);
            Debug.Log("Ron setView===> " + childRect.position + " | " + childRect.localPosition);
            
            GameObject btn = GameObject.Find("StartGameButton");
            Debug.Log("btn setView ==> " + btn.transform.position + " | " + btn.transform.localPosition);

            // parenetRect.pivot
        }
    }
}


