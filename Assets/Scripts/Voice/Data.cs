using System;
using UnityEngine;


[Serializable]
public struct Data
{
    /// <summary>
    /// תд������	��0��ʼ
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
                        /// ��ʶ����
                        /// </summary>
                        public string w;

                        /// <summary>
                        /// �ʱ�ʶ  n-��ͨ�ʣ�s-˳���ʣ������ʣ���p-���
                        /// </summary>
                        public string wp;
                    }

                    [SerializeField] public CW[] cw;

                    /// <summary>
                    /// ���ڱ����еĿ�ʼʱ�䣬��λ��֡��1֡=10ms  ���������������еĿ�ʼʱ��Ϊ(bg+wb*10)ms
                    /// �м����� wb Ϊ 0
                    /// </summary>
                    public string wb;

                    /// <summary>
                    /// ���ڱ����еĽ���ʱ�䣬��λ��֡��1֡=10ms  ���������������еĽ���ʱ��Ϊ(bg+we*10)ms
                    /// �м����� we Ϊ 0
                    /// </summary>
                    public string we;
                }

                [SerializeField] public WS[] ws;
            }

            [SerializeField] public RT[] rt;

            /// <summary>
            /// ���������������еĿ�ʼʱ�䣬��λ����(ms)
            /// �м�����bgΪ׼ȷֵ
            /// </summary>
            public string bg;

            /// <summary>
            /// ������ͱ�ʶ	0-���ս����1-�м���
            /// </summary>
            public string type;

            /// <summary>
            /// ���������������еĽ���ʱ�䣬��λ����(ms)
            /// �м�����edΪ0
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