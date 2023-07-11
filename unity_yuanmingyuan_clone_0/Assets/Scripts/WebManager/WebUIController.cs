// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class WebUIController : MonoBehaviour
{
    /// <summary>
    /// 关闭音效
    /// </summary>
    /// <param name="audioData">音效ID</param>
    public void audioOff(string audioData)
    {
        audioDataClass data = JsonMapper.ToObject<audioDataClass>(audioData);
        Debug.Log("联通方法：audioOff");

    }
    
    /// <summary>
    /// 开启音效
    /// </summary>
    /// <param name="audioData">音效ID</param>
    public void audioOn(string audioData)
    {
        audioDataClass data = JsonMapper.ToObject<audioDataClass>(audioData);
        Debug.Log("联通方法：audioOn");

    }

    /// <summary>
    /// BG改变
    /// </summary>
    /// <param name="bgData"></param>
    public void bgChange(string bgData)
    {
        bgDataClass data = JsonMapper.ToObject<bgDataClass>(bgData);
        Debug.Log("联通方法：bgChange");

    }
}
