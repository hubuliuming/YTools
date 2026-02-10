/****************************************************
    文件：HotPointSystem.cs
    作者：Y
    邮箱: 916111418@qq.com
    日期：#CreateTime#
    功能：Nothing
*****************************************************/

using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class HotPointSystem :AbstractSystem 
{
    #region 成员变量

    private static Dictionary<HotPointType, List<IHotPoint>> _hotPointDic;

    #endregion
    
    protected override void OnInit()
    {
        _hotPointDic = new Dictionary<HotPointType, List<IHotPoint>>();
    }
    
  

    public static void RegisterHotPoint(IHotPoint hotPoint,IHotPoint parent)
    {
        if (hotPoint == null) return;
        if (_hotPointDic.ContainsKey(hotPoint.HotPointType))
        {
            if (_hotPointDic[hotPoint.HotPointType].Count > 0)
            {
                _hotPointDic[hotPoint.HotPointType].Add(hotPoint);
            }
            else
            {
                _hotPointDic[hotPoint.HotPointType] = new List<IHotPoint>();
                _hotPointDic[hotPoint.HotPointType].Add(hotPoint);
            }
            hotPoint.SetParent(parent);
        }
        else
        {
            var list = new List<IHotPoint>();
            list.Add(hotPoint);
            _hotPointDic.Add(hotPoint.HotPointType, list);
        }

    }

    public static void UnRegister(IHotPoint hotPoint)
    {
        if (hotPoint == null) return;
        if (_hotPointDic.ContainsKey(hotPoint.HotPointType))
        {
            if (_hotPointDic[hotPoint.HotPointType].Remove(hotPoint))
            {
                hotPoint.HotPointParent?.RemoveChild(hotPoint);
                hotPoint.ChildNodes.Clear();
            }
        }
    }

    public static void UnRegister(HotPointType hotPointType)
    {
        if (_hotPointDic.ContainsKey(hotPointType))
        {
            foreach (var point in _hotPointDic[hotPointType])
            {
                point.Dispose();
            }
            _hotPointDic.Remove(hotPointType);
        }
    }

    public enum HotPointType
    {
        DailyTask,
        MainTask,
        Achievement,
    }
}