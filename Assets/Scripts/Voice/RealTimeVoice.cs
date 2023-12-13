using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

public class RealTimeVoice : MonoBehaviour
{
    private string appid = "xxxxx";
    private string appkey = "xxxxxxxxxxxxxxxxxxxxxxxxx";

    private string timeStamp;
    private string baseString;
    private string toMd5;

    private string signa;

    public AudioClip RecordedClip;
    ClientWebSocket ws;
    CancellationToken ct;

    private int MAX_RECORD_LENGTH = 3599;

    /// <summary>
    /// 语音识别回调事件
    /// </summary>
    public event Action<string> asrCallback;

    // Start is called before the first frame update
    void Start()
    {
        asrCallback += Output;
    }

    void Output(string str)
    {
        Debug.Log("语音识别结果：" + str);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
