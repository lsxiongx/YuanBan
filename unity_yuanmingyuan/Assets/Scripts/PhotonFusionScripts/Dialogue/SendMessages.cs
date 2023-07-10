// 文件名称：SendMessages.cs
// 功能描述一：游戏角色头顶消息发送
// 编写作者：雄
// 编写日期：2023.2.6

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
using Unity.VisualScripting;

public class SendMessages : NetworkBehaviour
{
    /// <summary>
    /// 消息文本组件
    /// </summary>
    [Header("文本组件")] public TextMeshPro[] MessageText;

    [Networked(OnChanged = nameof(OnNickMessageChanged))] public NetworkString<_16> MessageData { set; get; }
    [Networked] public NetworkBool MessageBool { set; get; }
    
    private int IntMessageLength = 50;


    static void OnNickMessageChanged(Changed<SendMessages> changed)
    {
        changed.Behaviour.OnNickMessageChanged();
    }

    private void OnNickMessageChanged()
    {
        MessageText[1].text = MessageData.ToString();
        MessageText[0].text = MessageData.ToString();
    }
    
    [Rpc(RpcSources.InputAuthority,RpcTargets.StateAuthority)]
    private void RPC_SetNickMessage(string nickName,RpcInfo info = default)
    {
        this.MessageData = nickName;
    }

    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    private void RPC_SetNickMessageBool(bool nickName,RpcInfo info = default)
    {
        this.MessageBool = nickName;
    }

    private void Start()
    {
        ExitMessage();
    }

    public override void FixedUpdateNetwork()
    {
        if (MessageBool)
        {
            MessageText[0].gameObject.SetActive(MessageBool);
            MessageText[1].gameObject.SetActive(MessageBool);
        }
        else
        {
            MessageText[0].gameObject.SetActive(MessageBool);
            MessageText[1].gameObject.SetActive(MessageBool);
        }
    }

    /// <summary>
    /// 发送消息启用Canvas及脚本
    /// </summary>  
    /// <param name="strmessage"></param>
    public void ReceiveMessage(string strmessage)
    {
        Debug.Log($"场景接收到聊天信息：{strmessage}");
        //ExitMessage();
        RPC_SetNickMessageBool(false);
        strmessage =  Setnamelength(strmessage,IntMessageLength);
        
        RPC_SetNickMessage(strmessage);
        
        RPC_SetNickMessageBool(true);

        //ControllerCanvas();

        //ControllerUiCarmera(true);
        CancelInvoke();
        Invoke(nameof(ExitMessage), 10);
    }

    /// <summary>
    /// 消息字数限制
    /// </summary>
    /// <param name="i"></param>
    public string Setnamelength(string data, int i)//给名字限定i个字符的长度
    {
        string nameLenthString = MessageText[0].text;

        if (data.Length > i)
        {
            Debug.Log("长度"+ i);
            return data.Substring(0, i - 1).ToString() + "..";//截取到指定长度
        }
        else
        {
            return data;
        }
    }

    private void ControllerMessage(bool isOn)
    {
        MessageText[0].gameObject.SetActive(isOn);
        MessageText[1].gameObject.SetActive(isOn);
    }

    private void ExitMessage()
    {
        RPC_SetNickMessageBool(false);
    }
}