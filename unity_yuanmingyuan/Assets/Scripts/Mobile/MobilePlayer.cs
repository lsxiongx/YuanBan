// 文件名称：MobilePlayer.cs
// 功能描述：移动端人物移动控制
// 编写作者：董冰茹
// 编写日期：2023.2.6

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TriggerDetection;
using UnityEngine.AI;

public class MobilePlayer : MonoBehaviour
{
    [Header("玩家平滑移动到指定角度的近似时间")]
    public float smoothTime = 0.2f;
 
    [Header("玩家速度")]
    public float playerSpeed = 5f;
    
    /// <summary>
    /// 玩家平滑移动到指定角度的瞬时速度，由Mathf.SmoothDampAngle()自动填充值
    /// </summary>
    private float currentVelocity;
    
    private Animator animator;
    
    /// <summary>
    /// 跟随玩家的摄像机
    /// </summary>
    private Transform followCamera;

    public VirtualJoystick virtualJoystick;
    
    /// <summary>
    /// 鼠标点击的目标点
    /// </summary>
    private Vector3 targetPos;

    /// <summary>
    /// 是否在闪现
    /// </summary>
    private bool isFlashTime;

    private ParticleSystem _particleSystem;
    
    private DetectDemo detectDemo;

    private NavMeshAgent agent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        followCamera = Camera.main.transform;
        detectDemo = GameObject.Find("Detection").GetComponent<DetectDemo>();

        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Start()
    {
        virtualJoystick = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<VirtualJoystick>();
        
        GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineFreeLook>().Follow = transform;
        GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineFreeLook>().LookAt = transform.GetChild(0);

        if (detectDemo.IsMobile())
        {
            transform.GetComponent<MobilePlayer>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<MobileCamera>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CameraController>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        //PlayerMove();
        TestMove();
        SwitchAnimation();
    }
    
    private Vector3 moveDir;
    private float moveSpeed;

    private void TestMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //使用摇杆移动
        Vector2 joyDir = virtualJoystick.GetDir();
        
        Vector3 dir = new Vector3(joyDir.x, 0f, joyDir.y).normalized;

        moveSpeed = dir.magnitude;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + followCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity,
                smoothTime);
                

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.position += moveDir.normalized * playerSpeed * Time.deltaTime;
        }
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


    /// <summary>
    /// 设置动画
    /// </summary>
    private void SwitchAnimation()
    {
        //animator.SetFloat("Speed",PlayerRigidbody.velocity.sqrMagnitude);
        animator.SetFloat("Speed",moveSpeed);
    }
}
