// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class WebPlayerController : MonoBehaviour
{
    private PlayerController playerController;
    public CharacterSelect characterSelect;

    public NetworkRunnerHandler networkRunnerHandler;

    public Button Test_btn;
    public string message;
    private void Start()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            //测试用，发布需注释
            string a =
                "{\"playerID\":\"Noob Bot 一号\",\"type\":\"cloud/local\",\"multiplayer\":\"yes/no\",\"modelURL\":\"string\",\"message\":{\"model\":\"0\",\"type\":\"female\"}}";
            Debug.Log(a);
            init(a);
        }
        Test_btn.onClick.AddListener(() =>
        {
            animation("{\"playerID\":\"id value\",\"animation\":\"wave\",\"type\":\"yes/no\"}");
            //NetworkPlayer.local._SendMessages.ReceiveMessage(message);
        });
    }
    /// <summary>
    /// 初始化角色参数
    /// </summary>
    /// <param name="messageData"></param>
    private void init(string messageData)
    {
        PlayerPrefs.SetString("playerData",messageData);
        networkRunnerHandler.StartGames();
        //initDataClass data = JsonMapper.ToObject<initDataClass>(messageData);
        // characterSelect.GetCharactersGender(data.message.type);
        // characterSelect.InstantitationCharacters(data.message.model,"GenderData.username");
        // playerController = characterSelect.getPlayer();
        // Debug.Log("联通方法：init" + "----------" + "PlayerName：" + playerController.gameObject.name);
    }
    
    /// <summary>
    /// 角色动画控制
    /// </summary>
    /// <param name="messageData"></param>
    private void animation(string messageData)
    {
        animationDataClass data = JsonMapper.ToObject<animationDataClass>(messageData);
        if (NetworkPlayer.local != null)
        {
            NetworkPlayer.local.GetComponent<CharacterMovementHandler>().setAnimStr(data.animation);
        }
        Debug.Log("联通方法：animation");
    }
    
    /// <summary>
    /// 角色对话
    /// </summary>
    /// <param name="messageData"></param>
    private void speak(string messageData)
    {
        speakDataClass data = JsonMapper.ToObject<speakDataClass>(messageData);
        if (data.message != null && data.message != String.Empty)
        {
            NetworkPlayer.local._SendMessages.ReceiveMessage(data.message);
        }
        Debug.Log("联通方法：speak");

    }
    
    /// <summary>
    /// 角色视角第一/第三人称切换
    /// </summary>
    /// <param name="messageData"></param>
    private void mode(string messageData)
    {
        modeDataClass data = JsonMapper.ToObject<modeDataClass>(messageData);
        Debug.Log("联通方法：mode");
    }
    
    /// <summary>
    /// 角色路径引导
    /// </summary>
    /// <param name="messageData"></param>
    private void path(string messageData)
    {
        pathDataClass data = JsonMapper.ToObject<pathDataClass>(messageData);
        Debug.Log("联通方法：path");

    }
    
    /// <summary>
    /// 角色位置改变
    /// </summary>
    /// <param name="messageData"></param>
    private void positionChange(string messageData)
    {
        positionChangeDataClass data = JsonMapper.ToObject<positionChangeDataClass>(messageData);
        Debug.Log("联通方法：positionChange");

    }
    
    /// <summary>
    /// 角色颜色改变
    /// </summary>
    /// <param name="messageData"></param>
    private void colorChange(string messageData)
    {
        colorChangeDataClass data = JsonMapper.ToObject<colorChangeDataClass>(messageData);
        Debug.Log("联通方法：colorChange");

    }
    
    /// <summary>
    /// 角色身体（裤子）改变
    /// </summary>
    /// <param name="messageData"></param>
    private void pantsChange(string messageData)
    {
        pantsChangeDataClass data = JsonMapper.ToObject<pantsChangeDataClass>(messageData);
        Debug.Log("联通方法：pantsChange");

    }
    
    /// <summary>
    /// 角色身体（上身）改变
    /// </summary>
    /// <param name="messageData"></param>
    private void topsChange(string messageData)
    {
        topsChangeDataClass data = JsonMapper.ToObject<topsChangeDataClass>(messageData);
        Debug.Log("联通方法：topsChange");

    }
    
    /// <summary>
    /// 角色身体（头部）改变
    /// </summary>
    /// <param name="messageData"></param>
    private void hatsChange(string messageData)
    {
        hatsChangeDataClass data = JsonMapper.ToObject<hatsChangeDataClass>(messageData);
        Debug.Log("联通方法：hatsChange");

    }
    
    /// <summary>
    /// 角色身体（套装）改变
    /// </summary>
    /// <param name="messageData"></param>
    private void suitChange(string messageData)
    {
        suitChangeDataClass data = JsonMapper.ToObject<suitChangeDataClass>(messageData);
        Debug.Log("联通方法：suitChange");

    }
}
