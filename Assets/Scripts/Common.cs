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

    /// Summary
    /// 目前这里不是返回的绝对值.但上层都以大于0的来做.稍后如果有必要.就改掉这个底层函数
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
    public enum DIRECTION {HORIZONTAL_TOP, HORIZONTAL_CENTER, HORIZONTAL_BOTTOM, VERTICAL_CENTER, VERTICAL_LEFT, VERTICAL_RIGHT};

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
        }
    }

    ///
    /// 这个坐标,只是视角上的坐标
    public static void SetVisionPositionByPoint(GameObject view, float x, float y, float z=0)
    {
        SetVisionPositionByPoint(view, new Vector3(x, y, z));
    }

    ///
    /// 这个坐标,只是视角上的坐标
    public static void SetVisionPositionByPoint(GameObject view, Vector2 v, float z=0)
    {
        SetVisionPositionByPoint(view, new Vector3(v.x, v.y, z));
    }

    ///
    /// Summary
    /// 目前无法支持锚点在4个角那种情况.只能支持锚点在1个点的(这样才能正确获取大小)
    /// v这个参数是根据父窗口的左下角为(0, 0)坐标系的值
    public static void SetVisionPositionByPoint(GameObject view, Vector3 v)
    {
        if(view == null || view.BP_IsAnchorsPoint() == false){
            return;
        }

        GameObject parentView = view.transform.parent.gameObject;
        Vector2 parentSize = parentView.BP_Size();
        float width = parentSize.x;
        float height = parentSize.y;

        // 视觉坐标先加上子view的锚点距离. 在转换成在父窗口的坐标值(即是这个点是子view的本地坐标)
        float x = v.x + view.BP_Size().x * view.BP_Pivot().x - parentView.BP_Size().x * parentView.BP_Pivot().x;
        float y = v.y + view.BP_Size().y * view.BP_Pivot().y - parentView.BP_Size().y * parentView.BP_Pivot().y;

        view.BP_RT().localPosition = new Vector3(x, y, v.z);
    }

    public static GameObject MakeupView(List<GameObject>viewArray, DIRECTION direction, float padding)
    {

        if(direction == DIRECTION.HORIZONTAL_TOP || direction == DIRECTION.HORIZONTAL_CENTER || direction == DIRECTION.HORIZONTAL_BOTTOM)
        {
            return MakeupView_horizontal(viewArray, direction, padding);
        }
        
        else if(direction == DIRECTION.VERTICAL_LEFT || direction == DIRECTION.VERTICAL_CENTER || direction == DIRECTION.VERTICAL_RIGHT)
        {
            
        }

        return null;
    }

    ///
    /// Sunmmary: 横向组合view
    /// 
    public static GameObject MakeupView_horizontal(List<GameObject>viewArray, DIRECTION direction, float padding)
    {
        float width = 0;
        float height = 0;
        foreach(GameObject subView in viewArray)
        {
            if(subView == null){
                continue;
            }

            width += subView.BP_Size().x;
            width += padding;
            height = Mathf.Max(height, subView.BP_Size().y);
        }

        // 减去最后的padding
        width -= padding;

        // 创建一个底view
        GameObject parentView = BPCommon.CreateGameObject("test");
        parentView.BP_RT().pivot = new Vector2(0, 0);
        SetRectTransformSize_GameObj(parentView, width, height);

        float offsetX = 0;
        foreach(GameObject subView in viewArray)
        {
            if(subView == null){
                continue;
            }

            subView.transform.SetParent(parentView.transform);
            float offsetY = 0;
            
            switch(direction)
            {
                case DIRECTION.HORIZONTAL_TOP:
                    // offsetY = height - subView.BP_Size().y - parentView.BP_Pivot().y * height + subView.BP_Pivot().y * subView.BP_Size().y;
                    offsetY = height - subView.BP_Size().y;
                    break;

                case DIRECTION.HORIZONTAL_CENTER:
                    offsetY = (height - subView.BP_Size().y) / 2.0f;
                    // offsetY = (height - subView.BP_Size().y) / 2.0f - parentView.BP_Pivot().y * height + subView.BP_Pivot().y * subView.BP_Size().y;
                    break;

                case DIRECTION.HORIZONTAL_BOTTOM:
                    offsetY = 0f;
                    break;

                default:
                    break;

            }

            SetVisionPositionByPoint(subView, offsetX, offsetY);

            offsetX += subView.BP_Size().x;
            offsetX += padding;
        }

        return parentView;
    }



    public static void SetRectTransformSize_GameObj(GameObject obj, float x, float y)
    {
        SetRectTransformSize(obj.BP_RT(), x, y);
    }

    public static void SetRectTransformSize(RectTransform trans, float x, float y)
    {
        SetRectTransformSize(trans, new Vector2(x, y));
    }

    public static void SetRectTransformSize(RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

    public static GameObject CreateGameObject(string name="")
    {
        GameObject objRes = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/Container");
        GameObject obj = GameObject.Instantiate(objRes);
        obj.name = name;
        return obj;
    }

}


