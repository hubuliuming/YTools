/****************************************************
    文件：IHotPoint.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using YFramework;

public interface IHotPoint 
{
    HotPointSystem.HotPointType HotPointType { get; }
    bool IsActivate { get;}
    IHotPoint HotPointParent { get; }
    List<IHotPoint> ChildNodes { get; }
    void SetParent(IHotPoint parent);
    void RemoveChild(IHotPoint child);
    void Activate();
    void Deactivate();

    void Dispose();
}