using System;
using UnityEngine;


[Serializable]
public struct Data
{
    /// <summary>
    /// 转写结果序号	从0开始
    /// </summary>
    public string seg_id;

    [Serializable]
    public struct CN
    {
        [Serializable]
        public struct ST
        {
            [Serializable]
            public struct RT
            {
                [Serializable]
                public class WS
                {
                    [Serializable]
                    public class CW
                    {
                        /// <summary>
                        /// 词识别结果
                        /// </summary>
                        public string w;

                        /// <summary>
                        /// 词标识  n-普通词；s-顺滑词（语气词）；p-标点
                        /// </summary>
                        public string wp;
                    }

                    [SerializeField] public CW[] cw;

                    /// <summary>
                    /// 词在本句中的开始时间，单位是帧，1帧=10ms  即词在整段语音中的开始时间为(bg+wb*10)ms
                    /// 中间结果的 wb 为 0
                    /// </summary>
                    public string wb;

                    /// <summary>
                    /// 词在本句中的结束时间，单位是帧，1帧=10ms  即词在整段语音中的结束时间为(bg+we*10)ms
                    /// 中间结果的 we 为 0
                    /// </summary>
                    public string we;
                }

                [SerializeField] public WS[] ws;
            }

            [SerializeField] public RT[] rt;

            /// <summary>
            /// 句子在整段语音中的开始时间，单位毫秒(ms)
            /// 中间结果的bg为准确值
            /// </summary>
            public string bg;

            /// <summary>
            /// 结果类型标识	0-最终结果；1-中间结果
            /// </summary>
            public string type;

            /// <summary>
            /// 句子在整段语音中的结束时间，单位毫秒(ms)
            /// 中间结果的ed为0
            /// </summary>
            public string ed;
        }

        [SerializeField] public ST st;
    }

    [SerializeField] public CN cn;

    /// <summary>
    /// 
    /// </summary>
    public string ls;

    public override string ToString()
    {
        return string.Format("seg_id:{0}, cn:{1}, ls:{2}", seg_id.ToString(), cn.ToString(), ls.ToString());
    }
}