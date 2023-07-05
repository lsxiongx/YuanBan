// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;
using UnityEngine;

public class WebNPCCtonroller : MonoBehaviour
{
    /// <summary>
    /// 调用前端方法，传入Npc的值
    /// </summary>
    /// <param name="npcData"></param>
    [DllImport("__Internal")]
    private static extern void GetNpcId(string npcData);

    /// <summary>
    /// Unity内部调用：用来调用前端方法（Npc范围触发）
    /// </summary>
    /// <param name="NpcId"></param>
    /// <param name="detectValue"></param>
    public void AreaDetect(string NpcId,string detectValue)
    {
        AreaDatectDataClass data = new AreaDatectDataClass();
        data.NpcID = NpcId;
        data.detectValue = detectValue;
        string strData = JsonMapper.ToJson(data);
        GetNpcId(strData);
    }
    
    /// <summary>
    /// Npc对话
    /// </summary>
    /// <param name="messageData"></param>
    public void speak(string messageData)
    {
        speakNpcDataClass data = JsonMapper.ToObject<speakNpcDataClass>(messageData);
        Debug.Log("联通方法：speak");

    }
}
