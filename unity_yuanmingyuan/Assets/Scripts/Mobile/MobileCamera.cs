// 文件名称：MobileCamera.cs
// 功能描述1：移动端 第三人称相机
// 功能描述2：跟随
// 功能描述3：单指视角旋转
// 功能描述4：视角遮挡处理
// 编写作者：董冰茹
// 编写日期：2023.2.6

using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCamera : MonoBehaviour
{
     /// <summary>
    /// 玩家
    /// </summary>
    private Transform player;

    /// <summary>
    /// 是否获取目标
    /// </summary>
    private bool getAim = true;

    /// <summary>
    /// Cinemachine
    /// </summary>
    private CinemachineFreeLook freeLookCam;

    /// <summary>
    /// 虚拟摇杆
    /// </summary>
    public VirtualJoystick virtualJoystick;
    
    private void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
        freeLookCam.enabled = true;
    }

    private void Update()
    {
        if (getAim)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                freeLookCam.Follow = player;
                freeLookCam.LookAt = player.GetChild(0); 
                getAim = false;
            }
        }
    }

    private void LateUpdate()
    {
        DragToRotateView();
        // 一只手指操作并且是摇杆移动，禁止旋转视角
       
        if (2 == Input.touchCount)
        {
            // 双指缩放
            HandleTwoFingerScale();
        }
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
        if (Input.GetMouseButton(0))
        {
            xAngle = Input.GetAxis("Mouse X");
            yAngle = Input.GetAxis("Mouse Y");
        }
        
        if (/*UIDetection.Instance.UIDetect() ||*/ (Input.touchCount <= 1 && virtualJoystick.inDrag))
        {
            xAngle = 0;
            yAngle = 0;
        }

        if (Input.GetMouseButtonUp(0))
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
    
    private float m_twoFingerLastDistance = -1;
    
    private void HandleTwoFingerScale()
    {
        float distance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        if (m_twoFingerLastDistance == -1)
        {
            m_twoFingerLastDistance = distance;
        }
        // 与上一帧比较变化
        float scale = 0.1f * (distance - m_twoFingerLastDistance);
        ScrollToScaleDistance(scale);
        m_twoFingerLastDistance = distance;
    }
    
    /// <summary>
    /// 视角缩放
    /// </summary>
    private void ScrollToScaleDistance(float scroll)
    {
        freeLookCam.m_Lens.FieldOfView -= scroll * scrollSensitive;

        freeLookCam.m_Lens.FieldOfView = Mathf.Clamp(freeLookCam.m_Lens.FieldOfView, minView, MaxView);
    }
}   