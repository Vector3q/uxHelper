using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Reqs;

public class RealTimeVoice : MonoBehaviour
{
    private Requests requests = new Requests();
    private string appid = "f111cd01";
    private string appkey = "c5cd4195b06ee65921c844db76f615c2";

    private string timeStamp;
    private string baseString;
    private string toMd5;

    private string signa;
    public Text SpeechRecognitionText;
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
        Debug.Log("[Voice] Result: " + str);
    }
    
    public void StartASR()
    {
        Debug.Log("[ASR] Start 1");
        if (ws != null && ws.State == WebSocketState.Open)
        {
            Debug.LogWarning("[Voice] start ASR fails, please wait for the last connection to end!");
            return;
        }
        if(Microphone.devices.Length == 0)
        {
            Debug.LogError("[Voice] No accessible microphone!");
            return;
        }
        Debug.Log("[ASR] Start 2");
        ConnectASR_Aysnc();
        
        RecordedClip = Microphone.Start(null, false, MAX_RECORD_LENGTH, 16000);
    }

    public async void StopASR()
    {
        if(ws != null)
        {
            // close the coroutine of audio sending.
            StopCoroutine(SendAudioClip());
            await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"end\": true}")),
                WebSocketMessageType.Binary,
                true, new CancellationToken());
            
            
            
            //ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("{\"end\": true}")), WebSocketMessageType.Binary, true, new CancellationToken());
            Microphone.End(null);


            StartCoroutine(StopRecord());
            ws.Dispose();
        }
    }

    private IEnumerator StopRecord()
    {
        yield return new WaitUntil(() => ws.State != WebSocketState.Open);
        Debug.Log("[Voice] Stop Record");
    }

    async void ConnectASR_Aysnc()
    {
        Debug.Log("[ASR] Start 3");
        ws = new ClientWebSocket();
        ct = new CancellationToken();

        Uri url = GetUri();
        Debug.Log("[ASR] Start 4");
        await ws.ConnectAsync(url, ct);
        Debug.Log("[ASR] Start 5");
        StartCoroutine(SendAudioClip());
        Debug.Log("[ASR] Start 6");
        StringBuilder sb = new StringBuilder();
        while(ws.State == WebSocketState.Open)
        {
            Debug.Log(ws.CloseStatus.ToString());

            var result = new byte[4096];
            Debug.Log("[ASR] WS.RECEIVEASYNC");
            await ws.ReceiveAsync(new ArraySegment<byte>(result), ct);
            List<byte> list = new List<byte>(result);
            while (list[list.Count - 1] == 0x00) list.RemoveAt(list.Count - 1);
            string str = Encoding.UTF8.GetString(list.ToArray());
            Debug.Log("[Voice] Receive Message: " + str);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            JsonData jsonData = JsonUtility.FromJson<JsonData>(str);

            if (jsonData.action.Equals("started"))
            {
                Debug.Log("[Voice] Success!");
            }
            else if (jsonData.action.Equals("result"))
            {
                sb.Append(AnalysisResult(jsonData));
            }
            else if (jsonData.action.Equals("error"))
            {
                Debug.Log("[Voice] Error: " + jsonData.desc);
                ws.Abort();
            }
        }
        Debug.Log("[ASR] Start 6");
    }

    private Uri GetUri()
    {
        timeStamp = GetTimeStamp();
        baseString = appid + timeStamp;

        toMd5 = ToMD5(baseString);

        signa = ToHmacSHA1(toMd5, appkey);

        string requestUrl = string.Format("wss://rtasr.xfyun.cn/v1/ws?appid={0}&ts={1}&signa={2}&pd=tech", appid, timeStamp, UrlEncode(signa));
        Debug.Log("[Voice] requestUrl: " + requestUrl);
        return new Uri(requestUrl);
    }

    IEnumerator SendAudioClip()
    {
        yield return new WaitWhile(() => Microphone.GetPosition(null) <= 0);
        float accumulatedTime = 0;
        int position = Microphone.GetPosition(null);
        const float waitTime = 0.04f;
        const int maxlength = 1280;
        int status = 0;
        int lastPosition = 0;
        while(position < RecordedClip.samples && ws.State == WebSocketState.Open)
        {
            accumulatedTime += waitTime;
            if(accumulatedTime > MAX_RECORD_LENGTH)
            {
                Debug.Log("[Voice] Timed out");
                break;
            }

            yield return new WaitForSecondsRealtime(waitTime);

            if (Microphone.IsRecording(null))
            {
                position = Microphone.GetPosition(null);
            }

            if (position <= lastPosition)
            {
                continue;
            }

            int length = position - lastPosition > maxlength ? maxlength : position - lastPosition;
            byte[] data = GetAudioClip(lastPosition, length, RecordedClip);

            ws.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, new CancellationToken());
            lastPosition = lastPosition + length;
            status = 1;
        }
    }
    string AnalysisResult(JsonData jsonData)
    {
        Data result = JsonUtility.FromJson<Data>(jsonData.data);
        StringBuilder stringBuilder = new StringBuilder();

        for(int i=0; i<result.cn.st.rt[0].ws.Length; i++)
        {
            stringBuilder.Append(result.cn.st.rt[0].ws[i].cw[0].w);
        }

        string _thisType = result.cn.st.type;
        string testing = stringBuilder.ToString();  
        //Debug.Log("[Voice] Voice Test ws: " + ws.ToString());
        if (stringBuilder != null)
        {
            SpeechRecognitionText.text += testing;
        }

        return "";
    }
    public static byte[] GetAudioClip(int start, int length, AudioClip recordedClip)
    {
        float[] sounddata = new float[length];
        recordedClip.GetData(sounddata, start);
        int rescaleFactor = 32767;
        byte[] outData = new byte[sounddata.Length * 2];
        for(int i = 0; i < sounddata.Length; i++)
        {
            short temshort = (short)(sounddata[i] * rescaleFactor);
            byte[] temdata = BitConverter.GetBytes(temshort);
            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];
        }

        return outData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = Encoding.UTF8.GetBytes(str);
        for(int i=0; i<byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }
        return sb.ToString();
    }

    /// <summary>
    /// return to timestamp
    /// </summary>
    /// <returns>timestamp (seconds) </returns>
    private static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    /// <summary>
    /// MD5 String encode
    /// </summary>
    /// <param name="txt">String to encode</param>
    /// <returns>Encoded String</returns>
    public static string ToMD5(string txt)
    {
        using(MD5 mi = MD5.Create())
        {
            byte[] buffer = Encoding.Default.GetBytes(txt);
            byte[] newbuffer = mi.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<newbuffer.Length; i++)
            {
                sb.Append(newbuffer[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }


    /// <summary>
    /// HMACSHA Encode 
    /// </summary>
    /// <param name="text">String to encode</param>
    /// <param name="key">private key</param>
    /// <returns>Signature</returns>
    public static string ToHmacSHA1(string text, string key)
    {
        HMACSHA1 hmacsha1 = new HMACSHA1();
        hmacsha1.Key = Encoding.UTF8.GetBytes(key);

        byte[] dataBuffer = Encoding.UTF8.GetBytes(text);
        byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

        return Convert.ToBase64String(hashBytes);
    }
}
