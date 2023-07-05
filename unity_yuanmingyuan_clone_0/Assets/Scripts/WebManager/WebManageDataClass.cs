// 文件名称：***.cs
// 功能描述：数据总类
// 编写作者：雄
// 编写日期：

public class initDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public string type { get; set; }
    /// <summary>
    /// 是否多人
    /// </summary>
    public string multiplayer { get; set; }
    /// <summary>
    /// 模型Url
    /// </summary>
    public string modelURL { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public PlayerGenderClass message { get; set; }
}
public class PlayerGenderClass
{
    /// <summary>
    /// 下标
    /// </summary>
    public string model { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public string type { get; set; }
}
public class animationDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 动画
    /// </summary>
    public string animation { get; set; }
    /// <summary>
    /// 动画类型
    /// </summary>
    public string type { get; set; }
}
public class speakDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 对话信息
    /// </summary>
    public string message { get; set; }
}
public class AreaDatectDataClass
{
    /// <summary>
    /// NpcID
    /// </summary>
    public string NpcID { get; set; }
    /// <summary>
    /// 对话信息
    /// </summary>
    public string detectValue { get; set; }
}
public class speakNpcDataClass
{
    /// <summary>
    /// NpcID
    /// </summary>
    public string NpcID { get; set; }
    /// <summary>
    /// 对话信息
    /// </summary>
    public string message { get; set; }
}
public class modeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 视角切换模式
    /// </summary>
    public string mode { get; set; }
}
public class pathDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 路径目标
    /// </summary>
    public string pathType { get; set; }
}
public class positionChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 位置
    /// </summary>
    public string position { get; set; }
}
public class colorChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 颜色
    /// </summary>
    public string color { get; set; }
}
public class pantsChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 裤子
    /// </summary>
    public string pants { get; set; }
}
public class topsChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 上身
    /// </summary>
    public string tops { get; set; }
}
public class hatsChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 头部
    /// </summary>
    public string hats { get; set; }
}
public class suitChangeDataClass
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string playerID { get; set; }
    /// <summary>
    /// 套装
    /// </summary>
    public string suit { get; set; }
}
public class audioDataClass
{
    /// <summary>
    /// 音效ID
    /// </summary>
    public string audioID { get; set; }
}
public class bgDataClass
{
    /// <summary>
    /// bgID
    /// </summary>
    public string bgID { get; set; }
}
