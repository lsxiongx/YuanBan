// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-5, 5), 1.5f, Random.Range(-5, 5));
    }
}
