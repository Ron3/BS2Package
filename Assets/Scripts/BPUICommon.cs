using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



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
    public static void BP_SetParent(this GameObject obj, GameObject parentObj, BPUICommon.POSITION pos=BPUICommon.POSITION.CENTER)
    {
        if(obj == null){
            return;
        }

        if(parentObj == null)
        {
            obj.transform.SetParent(null);
        }
        else
        {
            obj.transform.SetParent(parentObj.transform);
            BPUICommon.SetVisionPositionByBPPos(obj, pos);
        }
        
    }

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

    public static Image BP_Image(this GameObject obj)
    {
        return obj.GetComponent<Image>();
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

    public static void BP_AttachText(this GameObject obj, string text)
    {
        if(obj == null){
            return;
        }

        // ui调试信息
        GameObject textObj = BPUICommon.CreateTextObject(text);
        textObj.BP_SetParent(obj);
    }

    ///
    /// Summary
    /// 递归遍历子孩子.找到第一个名字吻合的
    public static GameObject BP_Find(this GameObject parentObj, string name)
    {
        if(parentObj == null || name == null){
            return null;
        }

        // 1, 找第一层.
        Transform childTR = parentObj.transform.Find(name);
        if(childTR != null){
            return childTR.gameObject;
        }

        // 2, 到了这里.即表示要遍历找它子节点
        foreach(Transform tr in parentObj.transform){
           GameObject childObj = tr.gameObject.BP_Find(name);
           if(childObj != null){
               return childObj;
           }
        }

        return null;
    }    
}


public class BPUICommon
{
    public enum POSITION {CENTER,               // 居中
                            CENTER_LEFT,        // 靠左居中
                            CENTER_RIGHT,       // 靠右居中
                            BOTTOM_LEFT,        // 靠底居左
                            BOTTOM_CENTER,      // 靠底居中
                            BOTTOM_RIGHT,       // 靠底居右
                            TOP_LEFT,           // 靠上居左
                            TOP_CENTER,         // 靠上居中
                            TOP_RIGHT           // 靠上居右
                            };          

    public enum DIRECTION {HORIZONTAL_TOP, HORIZONTAL_CENTER, HORIZONTAL_BOTTOM, VERTICAL_CENTER, VERTICAL_LEFT, VERTICAL_RIGHT};

    ///
    /// 设置坐标
    public static void SetVisionPositionByBPPos(GameObject view, POSITION targetPos=POSITION.CENTER)
    {
        if(view == null || view.transform.parent.gameObject == null){
            return;
        }

        // 这里的x,y是视角坐标(与左下角为0,0坐标系的)
        float x = 0, y = 0;     
        GameObject parentView = view.transform.parent.gameObject;
        if(targetPos == BPUICommon.POSITION.CENTER)
        {
            x = (parentView.BP_Size().x - view.BP_Size().x) / 2f;
            y = (parentView.BP_Size().y - view.BP_Size().y) / 2f;
        }
        else if(targetPos == BPUICommon.POSITION.CENTER_LEFT)
        {
            x = 0;
            y = (parentView.BP_Size().y - view.BP_Size().y) / 2f;
        }
        else if(targetPos == BPUICommon.POSITION.CENTER_RIGHT)
        {
            x = parentView.BP_Size().x - view.BP_Size().x;
            y = (parentView.BP_Size().y - view.BP_Size().y) / 2f;
        }
        else if(targetPos == BPUICommon.POSITION.BOTTOM_LEFT)
        {
            x = 0;
            y = 0;
        }
        else if(targetPos == BPUICommon.POSITION.BOTTOM_CENTER)
        {
            x = (parentView.BP_Size().x - view.BP_Size().x) / 2f;
            y = 0;
        }
        else if(targetPos == BPUICommon.POSITION.BOTTOM_RIGHT)
        {
            x = parentView.BP_Size().x - view.BP_Size().x;
            y = 0;
        }
        else if(targetPos == BPUICommon.POSITION.TOP_LEFT)
        {
            x = 0;
            y = parentView.BP_Size().y - view.BP_Size().y;
        }
        else if(targetPos == BPUICommon.POSITION.TOP_CENTER)
        {
            x = (parentView.BP_Size().x - view.BP_Size().x) / 2f;
            y = parentView.BP_Size().y - view.BP_Size().y;
        }
        else if(targetPos == BPUICommon.POSITION.TOP_RIGHT)
        {
            x = parentView.BP_Size().x - view.BP_Size().x;
            y = parentView.BP_Size().y - view.BP_Size().y;
        }

        SetVisionPositionByPoint(view, x, y);
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
            return MakeupView_vertical(viewArray, direction, padding);
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
        GameObject parentView = BPUICommon.CreateGameObject("test");
        parentView.BP_RT().pivot = new Vector2(0.5f, 0.5f);
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
                    offsetY = height - subView.BP_Size().y;
                    break;

                case DIRECTION.HORIZONTAL_CENTER:
                    offsetY = (height - subView.BP_Size().y) / 2.0f;
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

    ///
    /// 竖向组合view
    /// 
    public static GameObject MakeupView_vertical(List<GameObject>viewArray, DIRECTION direction, float padding)
    {
        float width = 0;
        float height = 0;
        foreach(GameObject subView in viewArray)
        {
            if(subView == null){
                continue;
            }

            height += subView.BP_Size().y;
            height += padding;
            width = Mathf.Max(width, subView.BP_Size().x);
        }

        // 减去最后的padding
        height -= padding;

        // 从上往下布局(跟上一个项目一样)
        viewArray.Reverse();

        // 创建一个底view
        GameObject parentView = BPUICommon.CreateGameObject("test");
        parentView.BP_RT().pivot = new Vector2(0.5f, 0.5f);
        SetRectTransformSize_GameObj(parentView, width, height);

        float offsetY = 0;
        foreach(GameObject subView in viewArray)
        {
            if(subView == null){
                continue;
            }

            subView.transform.SetParent(parentView.transform);
            float offsetX = 0;
            
            switch(direction)
            {
                case DIRECTION.VERTICAL_LEFT:
                    offsetX = 0;
                    break;

                case DIRECTION.VERTICAL_CENTER:
                    offsetX = (width - subView.BP_Size().x) / 2.0f;
                    break;

                case DIRECTION.VERTICAL_RIGHT:
                    offsetY = width - subView.BP_Size().x;
                    break;

                default:
                    break;
            }

            SetVisionPositionByPoint(subView, offsetX, offsetY);

            offsetY += subView.BP_Size().y;
            offsetY += padding;
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

    ///
    /// 根据容器名字.加载不同的GameObject
    public static GameObject CreateGameObjectByContainerName(string containerPath, string name="")
    {
        GameObject objRes = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>(containerPath);
        if(objRes == null){
            return null;
        }

        GameObject obj = GameObject.Instantiate(objRes);
        obj.name = name;
        return obj;
    }


    public static GameObject CreateGameObject(string name="")
    {
        GameObject objRes = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/Container");
        GameObject obj = GameObject.Instantiate(objRes);
        obj.name = name;
        return obj;
    }

    public static GameObject CreateTextObject(string text)
    {
        GameObject objRes = Utility.AssetRelate.ResourcesLoadCheckNull<GameObject>("Panel/Container_text");
        GameObject obj = GameObject.Instantiate(objRes);
        Text textObj = obj.GetComponent<Text>();
        textObj.text = text;
        textObj.color = Color.gray;
        return obj;
    }

    // Summary
    // 向命中的控件渗透消息
    public static void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function) where T: IEventSystemHandler
	{
		GameObject current = data.pointerCurrentRaycast.gameObject;
		List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, resultList); 
		foreach(RaycastResult obj in resultList)
        {
            if(current.gameObject != obj.gameObject){
				ExecuteEvents.Execute(obj.gameObject, data, function);
			}
		}
	}   


    /// Summary
    /// 得到射线命中的所有GameObject
    public static List<GameObject> GetHitGameObject(PointerEventData data)
	{
        List<GameObject> hitList = new List<GameObject>();

		// GameObject current = data.pointerCurrentRaycast.gameObject;
        // hitList.Add(current);

		List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, resultList); 
		foreach(RaycastResult obj in resultList)
        {
            hitList.Add(obj.gameObject);
		}

        return hitList;
	}   

}




public class ForeachClass
{
    /// <summary>
    /// C#反射遍历对象属性
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="model">对象</param>
    public static void ForeachClassProperties<T>(T model)
    {
        Type t = model.GetType();
        PropertyInfo[] PropertyList = t.GetProperties();
        foreach (PropertyInfo item in PropertyList)
        {
            string name = item.Name;
            // object value = item.GetValue(model, null);

            Debug.Log("name => " + name);
            
            // if(name == "animation")
            // {
            //     object value = item.GetValue(model, null);
            //     // Debug.Log("name => " + name + " value => " + value);
            // }
        }
    }
}
