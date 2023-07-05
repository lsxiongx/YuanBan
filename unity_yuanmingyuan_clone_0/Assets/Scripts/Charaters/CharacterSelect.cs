 // 文件名称：CharacterSelect.cs
// 功能描述：角色选择
// 编写作者：董冰茹
// 编写日期：2023.2.22

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [Header("角色女")] public GameObject[] femalecharacters;
    
    [Header("角色男")] public GameObject[] malecharacters;

    /// <summary>
    /// 实例化女性角色，隐藏/展示
    /// </summary>
    private GameObject[] femaleShow;
    
    /// <summary>
    /// 实例化男性角色，隐藏/展示
    /// </summary>
    private GameObject[] maleShow;
    
    /// <summary>
    /// 判断性别是否为女
    /// </summary>
    private bool _isGirl;

    /// <summary>
    /// 角色id
    /// </summary>
    private string characterId;

    public Vector3 originPos;
    private PlayerController player;
    
    private void Awake()
    {
        femaleShow = new GameObject[femalecharacters.Length];
        maleShow = new GameObject[malecharacters.Length];
    }

    public PlayerController getPlayer()
    {
        return player;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void InstantitationCharacters(string id,string userName)
    {
        float playId = float.Parse(id);
        int toId = (int)playId;
        if (_isGirl)
        {
            femaleShow[toId] = Instantiate(femalecharacters[toId], originPos, transform.rotation);
            GameObject obj = femaleShow[toId];
            player = obj.GetComponent<PlayerController>();
            // obj.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshPro>().text = userName;
            // obj.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshPro>().text = userName;
        }
        else
        {
            maleShow[toId] = Instantiate(malecharacters[toId], originPos, transform.rotation);
            GameObject obj = maleShow[toId];
            player = obj.GetComponent<PlayerController>();
            // obj.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshPro>().text = userName;
            // obj.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshPro>().text = userName;
        }
    }

    /// <summary>
    /// 获取前端传入玩家性别
    /// </summary>
    /// <param name="gender">性别是否为女</param>
    public void GetCharactersGender(string gender)
    {
        if (string.Equals(gender, "female"))
        {
            _isGirl = true;
        }
        else
        {
            _isGirl = false;
        }
    }
}