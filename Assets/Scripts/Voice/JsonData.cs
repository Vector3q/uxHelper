using System;

[Serializable]
public struct JsonData
{
    /// <summary>
    /// 结果标识，started:握手，result:结果，error:异常
    /// </summary>
    public string action;

    /// <summary>
    /// 结果码(具体见错误码)
    /// </summary>
    public string code;

    /// <summary>
    /// 转写结果数据
    /// </summary>
    public Data data;

    /// <summary>
    /// 描述
    /// </summary>
    public string desc;

    /// <summary>
    /// 会话ID
    /// 主要用于DEBUG追查问题，如果出现问题，可以提供sid帮助确认问题。
    /// </summary>
    public string sid;
}