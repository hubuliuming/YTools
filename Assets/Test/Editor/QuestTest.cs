/****************************************************
    文件：QuestTest.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/


using System;
using UnityEditor;
using UnityEngine;
using YFramework.Extension;

public class QuestTest 
{
    [MenuItem("YFramework/QuestClick")]
    private static void Click()
    {
        string email = "hubuliuminggmail.com";
        Debug.Log(email.IsEmail());
    }
}