// 文件名称：DetectDemo.cs
// 功能描述1：相交球检测
// 功能描述2：box检测
// 功能描述3：设备检测
// 编写作者：董冰茹
// 编写日期：2023.1

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TriggerDetection
{
    public class DetectDemo : MonoBehaviour
    {
        /// <summary>
        /// 相交球检测
        /// </summary>
        /// <param name="center">检测中心</param>
        /// <param name="radius">检测半径</param>
        /// <param name="detectLayer">检测的层级</param>
        /// <returns></returns>
        public bool DetectSphere(Vector3 center,float radius,LayerMask detectLayer)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, detectLayer);

            if (colliders.Length <= 0)
            {
                return false;
            }

            for (int i = 0; i < colliders.Length;)
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// box检测
        /// </summary>
        /// <param name="center">检测中心</param>
        /// <param name="a">长</param>
        /// <param name="b">宽</param>
        /// <param name="h">高</param>
        /// <param name="detectLayer">层级</param>
        /// <returns></returns>
        public bool DetectBox(Vector3 center, float a, float b, float h, LayerMask detectLayer)
        {
            Collider[] colliders = Physics.OverlapBox(center, new Vector3(a, b, h), Quaternion.identity, detectLayer);

            if (colliders.Length <= 0)
            {
                return false;
            }
            
            for (int i = 0; i < colliders.Length;)
            {
                return true;
            }

            return false;
        }
        
        [DllImport("__Internal")]
        private static extern bool HelloPlatform();
        
        // <summary>
        /// 平台检测
        /// </summary>
        public bool IsMobile()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // On Mobile
                if (!HelloPlatform())
                {
                    return true;
                }
                else
                {
                    // On PC
                    return false;
                }
            }
            //其它平台
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 玩家
        /// </summary>
        private Transform player;

        /// <summary>
        /// 是否获取目标
        /// </summary>
        private bool getAim = true;

        private void Start()
        {
            //InvokeRepeating("GetScripts",2,2);
        }

        /// <summary>
        /// 获取到玩家角色后停止
        /// </summary>
        private void Update()
        {
            if (getAim)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    GetAim();
                    getAim = false;
                }
            }
            
        }
        
        private void GetAim()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        
        private void GetScripts()
        {
            if (player != null)
            {
                if (IsMobile())
                {
                    player.GetComponent<MobilePlayer>().enabled = true;
                    GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<MobileCamera>().enabled = true;
                }
                else
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CameraController>().enabled = true;
                }
            }
        }
    }
}

