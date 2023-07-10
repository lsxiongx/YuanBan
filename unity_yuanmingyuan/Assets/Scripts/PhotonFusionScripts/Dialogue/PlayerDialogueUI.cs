// 文件名称：PlayerDialogueUI.cs
// 功能描述：对话框字体自带背景
// 编写作者：董冰茹
// 编写日期：2023.2.23

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;

[RequireComponent(typeof(TextMeshPro))]
public class PlayerDialogueUI : MonoBehaviour
{
    public float PaddingTop;

    public float PaddingBottom;

    public float PaddingLeft;

    public float PaddingRight;

    public Material material;

    private GameObject Background;

    private TextMeshPro textMeshPro;

    public float xAngle;
    public float yAngle;
    public float zAngle;

    public Transform player;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();

        Background = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Background.GetComponent<MeshCollider>().enabled = false;
        Background.name = "background";
        yAngle = player.eulerAngles.y;
        Background.transform.Rotate(xAngle, yAngle, zAngle);
        Background.transform.SetParent(this.transform);
       
        if (material != null)
        {
            Background.GetComponent<MeshRenderer>().material = material;
        }
    }

    void Update()
    {
        var bounds = textMeshPro.bounds;
        // Debug.Log($"{bounds}");
        
        Vector3 pos = bounds.center;
        float hoseiX = -(PaddingLeft / 2) + (PaddingRight / 2);
        float hoseiY = -(PaddingBottom / 2) + (PaddingTop / 2);
        float hoseiZ = 0.01f;
        
        Background.transform.localPosition = new Vector3(pos.x + hoseiX, pos.y + hoseiY, pos.z + hoseiZ);

        Vector3 scale = bounds.extents;
        float hoseiW = (PaddingLeft + PaddingRight) / 10;
        float hoseiH = (PaddingTop + PaddingBottom) / 10;
        
        Background.transform.localScale = new Vector3((scale.x / 10 * 2) + hoseiW, 1, (scale.y / 10 * 2) + hoseiH);
    }
}
