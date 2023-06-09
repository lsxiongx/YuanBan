 // 文件名称：PlayerController.cs
// 功能描述1：pc 玩家移动
// 功能描述2：W、A、S、D移动
// 功能描述3：移动动画
// 功能描述4：双击闪现
// 编写作者：董冰茹
// 编写日期：2022.12

using System;
using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

 public class PlayerController : Singleton<PlayerController>
{
    [Header("玩家平滑移动到指定角度的近似时间")]
    public float smoothTime = 0.2f;

    [Header("玩家速度")]
    public float playerSpeed = 5f;

    /*[Header("跳跃高度")]
    public float jumpHeigh;*/

    /// <summary>
    /// 玩家平滑移动到指定角度的瞬时速度，由Mathf.SmoothDampAngle()自动填充值
    /// </summary>
    private float currentVelocity;

    private Animator animator;

    /// <summary>
    /// 跟随玩家的摄像机
    /// </summary>
    private Transform followCamera;

    /// <summary>
    /// 鼠标点击的目标点
    /// </summary>
    private Vector3 targetPos;

    /// <summary>
    /// 是否在闪现
    /// </summary>
    private bool isFlashTime;

    //private DetectDemo detectDemo;

    private NavMeshAgent agent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        followCamera = Camera.main.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void FixedUpdate()
    {
        TestMove();
        SwitchAnimation();
    }

    private Vector3 moveDir;
    private float moveSpeed;
    private void TestMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h,0f,v).normalized;

        moveSpeed = dir.magnitude;
        
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + followCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
                
                
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * playerSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// 设置动画
    /// </summary>
    private void SwitchAnimation()
    {
        //animator.SetFloat("Speed", PlayerRigidbody.velocity.sqrMagnitude);
        animator.SetFloat("Speed",moveSpeed);
    }

    public void SwitchAnimator(string animatorId)
    {
        switch (animatorId)
        {
            case "dance":
                animator.SetTrigger("dance");
                break;
            case "seat":
                animator.SetTrigger("seat");
                break;
            case "jump":
                if (!agent.isOnOffMeshLink)
                {
                    animator.SetTrigger("jump");
                }
                break;
            case "wave":
                animator.SetTrigger("wave");
                break;
        }
    }
}
