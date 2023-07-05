// 文件名称：CameraController.cs
// 功能描述1：pc 第三人称相机
// 功能描述2：跟随
// 功能描述3：视角旋转
// 功能描述4：视角缩放
// 功能描述5：视角遮挡处理
// 编写作者：董冰茹
// 编写日期：2023.12

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    // /// <summary>
    // /// 玩家
    // /// </summary>
    // private Transform player;

    // /// <summary>
    // /// 是否获取目标
    // /// </summary>
    // private bool getAim = true;

    /// <summary>
    /// Cinemachine
    /// </summary>
    private CinemachineFreeLook freeLookCam;

    private void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
        // freeLookCam.enabled = true;
    
    }
    //
    // private void GetAim()
    // {
    //     player = GameObject.FindGameObjectWithTag("Player").transform;
    // }
    //
    // /// <summary>
    // /// 获取到玩家角色后停止
    // /// </summary>
    // private void Update()
    // {
    //     if (getAim)
    //     {
    //         if (GameObject.FindGameObjectWithTag("Player") != null)
    //         {
    //             GetAim();
    //             freeLookCam.Follow = player;
    //             freeLookCam.LookAt = player.GetChild(0);
    //             getAim = false;
    //         }
    //     }
    // }
    //
    private void LateUpdate()
    {
        DragToRotateView();
        ScrollToScaleDistance();
    }

    /// <summary>
    /// x旋转值
    /// </summary>
    private float xAngle;
    
    /// <summary>
    /// y旋转值
    /// </summary>
    private float yAngle;

    [Header("旋转灵敏度")]
    public float xRotaSensitive;
    
    /// <summary>
    /// 旋转视角
    /// </summary>
    private void DragToRotateView()
    {
        xAngle = 0;
        yAngle = 0;
        if (Input.GetMouseButtonDown(0) && VirtualJoystick.inDrag)  // UI检测
        {
            Debug.Log("点击到UI");
            return;
        }

        if ((Input.GetMouseButton(1) || Input.GetMouseButton(0)) && !VirtualJoystick.inDrag)
        {
            xAngle = Input.GetAxis("Mouse X");
            yAngle = Input.GetAxis("Mouse Y");
        }

        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            xAngle = 0;
            yAngle = 0;
        }

        freeLookCam.m_XAxis.m_InputAxisValue = -xAngle * xRotaSensitive;
        freeLookCam.m_YAxis.m_InputAxisValue = -yAngle;
    }

    [Header("滚轮灵敏度")]
    public float scrollSensitive;

    [Header("最小视野")]
    public float minView;
    
    [Header("最大视野")]
    public float MaxView;
    
    /// <summary>
    /// 视角缩放
    /// </summary>
    private void ScrollToScaleDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        freeLookCam.m_Lens.FieldOfView -= scroll * scrollSensitive;

        freeLookCam.m_Lens.FieldOfView = Mathf.Clamp(freeLookCam.m_Lens.FieldOfView, minView, MaxView);
    }
}