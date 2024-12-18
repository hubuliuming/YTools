

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using YFramework;
using Random = UnityEngine.Random;


public class Test1 : YMonoBehaviour
{
    

    
    private void Start()
    {
        string filePath = @"C:\Users\91611\Documents\WeChat Files\wxid_rz1e49017c0a22\FileStorage\File\2024-10\CurveData.dat";
        int floatsPerFrame = 5000;

        List<float[]> frames = ParseBinaryFile(filePath, floatsPerFrame);

        // 示例输出：打印每帧的第一个浮点数
        for (int i = 0; i < frames.Count; i++)
        {
            Debug.Log($"Frame {i + 1}, first float: {frames[i][0]}");
        }
        
    }
    
    static List<float[]> ParseBinaryFile(string filePath, int floatsPerFrame)
    {
        List<float[]> frames = new List<float[]>();

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (BinaryReader br = new BinaryReader(fs))
        {
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                float[] frame = new float[floatsPerFrame];
                for (int i = 0; i < floatsPerFrame; i++)
                {
                    if (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        frame[i] = br.ReadSingle();
                    }
                    else
                    {
                        break; // 防止最后一个帧不完整的情况
                    }
                }
                frames.Add(frame);
            }
        }

        return frames;
    }

    void Update()
    {
       
    }
}


