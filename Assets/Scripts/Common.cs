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

///
///
public static class MonoBehaviourExtension
{
    public static GameObject G(this MonoBehaviour mo)
    {
        return mo.gameObject;
    }
}

public static class GameObjectExtension
{
    public static RectTransform BP_RT(this GameObject obj)
    {
        if(obj == null)
        {
            return null;
        }

        return obj.GetComponent<RectTransform>();
    }

    public static float BP_Width(this GameObject obj)
    {
        if(obj == null){
            return 0;
        }

        return obj.GetComponent<RectTransform>().rect.width;
    }

    public static float BP_Height(this GameObject obj)
    {
        if(obj == null){
            return 0;
        }

        return obj.GetComponent<RectTransform>().rect.height;
    }

    public static Rect BP_Rect(this GameObject obj)
    {
        return obj.GetComponent<RectTransform>().rect;
    }

    public static Vector2 BP_Size(this GameObject obj)
    {
        if(obj == null){
            new Vector2(0f, 0f);
        }

        return obj.GetComponent<RectTransform>().rect.size;
    }

    public static Vector2 BP_LocalPosition(this GameObject obj)
    {
        return obj.transform.localPosition;
    }

    public static Vector2 BP_Position(this GameObject obj)
    {
        return obj.transform.position;
    }

    public static Vector2 BP_Pivot(this GameObject obj)
    {
        return obj.BP_RT().pivot;
    }

    public static Vector2 BP_SizeDelta(this GameObject obj)
    {
        return obj.BP_RT().sizeDelta;
    }

    public static Vector2 BP_AnchoredPosition(this GameObject obj)
    {
        return obj.BP_RT().anchoredPosition;
    }

    public static Vector2 BP_AnchorMax(this GameObject obj)
    {
        return obj.BP_RT().anchorMax;
    }

    public static Vector2 BP_AnchorMin(this GameObject obj)
    {
        return obj.BP_RT().anchorMin;
    }

    public static Vector2 BP_OffsetMax(this GameObject obj)
    {
        return obj.BP_RT().offsetMax;
    }
    
    public static Vector2 BP_OffsetMin(this GameObject obj)
    {
        return obj.BP_RT().offsetMin;
    }

    /// 判定Anchors是否是一个点
    public static bool BP_IsAnchorsPoint(this GameObject obj)
    {
        if(obj.BP_AnchorMax().x - obj.BP_AnchorMin().x >= 0.001 || obj.BP_AnchorMax().x - obj.BP_AnchorMin().x <= -0.001)
        {
            return false;
        }

        if(obj.BP_AnchorMax().y - obj.BP_AnchorMin().y >= 0.001 || obj.BP_AnchorMax().y - obj.BP_AnchorMin().y <= -0.001)
        {
            return false;
        }

        return true;
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

        // 假设目前只能处理这种情况
        if(parentView.BP_IsAnchorsPoint() == false){
            return;
        }

        float parentViewWidth = parentView.BP_Width();
        float parentViewHeight = parentView.BP_Height();

        if(targetPos == BPCommon.POSITION.CENTER)
        {
            float x = parentViewWidth / 2.0f - parentView.BP_RT().pivot.x * parentViewWidth;
            float y = parentViewHeight / 2.0f - parentView.BP_RT().pivot.y * parentViewHeight;

            view.BP_RT().localPosition = new Vector3(x, y, 0f);
            Debug.Log("Ron setView===> " + view.BP_RT().position + " | " + view.BP_RT().localPosition);
            // parenetRect.pivot
        }
    }


    public static void SetRectTransformSize(RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

}


