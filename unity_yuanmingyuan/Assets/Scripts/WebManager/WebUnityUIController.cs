// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebUnityUIController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OnclickBanner(string bannerID,string message);
    [DllImport("__Internal")]
    private static extern void OnclickButton(string BottonID,string message);
    
    /// <summary>
    /// Unity内部调用：用来调用前端方法（点击背景或广告）
    /// </summary>
    /// <param name="bannerId"></param>
    /// <param name="messageJson"></param>
    public void clickBanner(string bannerId,string messageJson)
    {
        OnclickBanner(bannerId,messageJson);
    }
    
    /// <summary>
    /// Unity内部调用：用来调用前端方法（点击按钮）
    /// </summary>
    /// <param name="buttonId"></param>
    /// <param name="messageJson"></param>
    public void clickButton(string buttonId,string messageJson)
    {
        OnclickButton(buttonId,messageJson);
    }
}
