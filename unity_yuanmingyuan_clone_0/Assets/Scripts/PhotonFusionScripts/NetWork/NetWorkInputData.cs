// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetWorkInputData : INetworkInput
{
    public Vector2 Movements;
    public float rotationInput;
    
}