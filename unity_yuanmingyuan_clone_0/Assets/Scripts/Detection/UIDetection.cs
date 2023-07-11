// 文件名称：UIDetection.cs
// 功能描述：UI检测
// 编写作者：董冰茹
// 编写日期：2023.2.6

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TriggerDetection;

public class UIDetection : Singleton<UIDetection>
{
    private DetectDemo detectDemo;

    [Header("虚拟摇杆")] public Transform joyStick;

    protected override void Awake()
    {
        base.Awake();
        detectDemo = GetComponent<DetectDemo>();
    }

    private void Start()
    {
        if (detectDemo.IsMobile())
        {
            joyStick.gameObject.SetActive(true);
        }
    }

    public bool UIDetect()
    {
        if (detectDemo.IsMobile())
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (IsPointerOverGameObject(Input.GetTouch(0).position))
                {
                    /*Debug.Log("点击到UI");*/
                    return true;
                }
            }          
        }
        else
        {
            if(Input.GetMouseButton(0))
            {
                if (IsPointerOverGameObject(Input.mousePosition))
                {
                    /*Debug.Log("点击到UI");*/
                    return true;
                }
            }
        }
        
        return false;
    }

    /// <summary>
    /// 检测是否点击UI
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    private bool IsPointerOverGameObject(Vector2 mousePosition)
    {
        //创建一个点击事件
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        //向点击位置发射一条射线，检测是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        if (raycastResults.Count > 0)
        {
            return true;
        }

        return false;
    }
}