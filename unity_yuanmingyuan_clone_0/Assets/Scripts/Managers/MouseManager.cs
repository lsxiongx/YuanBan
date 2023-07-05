// 文件名称：MouseManager.cs
// 功能描述：鼠标点击事件
// 编写作者：董冰茹
// 编写日期：2023.2.16

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    public event Action<Vector3> OnMouseClick;

    /// <summary>
    /// 射线
    /// </summary>
    private RaycastHit hitInfo;
    
    /// <summary>
    /// 上次点击的时间
    /// </summary>
    private float lastTime;
    
    /// <summary>
    /// 两次点击间隔时间
    /// </summary>
    private float gapTime = 0.2f;

    private void Update()
    {
        MouseController();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
    }

    /// <summary>
    /// 双击事件 
    /// </summary>
    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.realtimeSinceStartup - lastTime < gapTime)   // 鼠标双击
            {
                if (hitInfo.collider.gameObject.CompareTag("Ground"))
                {
                    OnMouseClick?.Invoke(hitInfo.point);
                }
            }
            else
            {
                lastTime = Time.realtimeSinceStartup;
            }
        }
    }
}
