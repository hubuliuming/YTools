
using System;
using UnityEngine;
using UnityEngine.UI;
using YFramework.Extension;

public class RockerControl : MonoBehaviour
{
    [Tooltip("小点可以移动的距离")]
    public float pointDis = 140;   

    public Image ImgTouch;  
    public Image imgDirBG;  
    public Image imgDirPoint;  
    public Transform ArrowRoot;  

    Vector2 startPos = Vector2.zero;   //初始位置
    Vector2 defaultPos = Vector2.zero;

    public Action OnClickDown;
    
    /// <summary>
    /// 滑动的方向
    /// </summary>
    public Vector2 Dir { get;private set; }  

    public bool RePlay
    {
        get;
        set;
    }

    public bool GameOver
    {
        get;
        set;
    }

    void Start()
    {
        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.ScreenOPDis;     //计算出 不同分辨率下的 小点移动移动的距离    当前场景的高度  除以  我们设定的 高度  的  比例  在乘以  我们的 小点的 移动范围
        //关闭箭头
        ArrowRoot.gameObject.SetActive(false);
        defaultPos = imgDirBG.transform.position;   //获得轮盘的初始位置

        if (!RePlay)
        { 
            if (!GameOver)  
            {
                RegisterMoveEvts();   //调用 事件方法
            }
        }

    }
    

    void RegisterMoveEvts()
    {
        //注释移动的点击时间 
        ArrowRoot.gameObject.SetActive(false);    //把箭头传递进去

        var imgTouchListener = ImgTouch.gameObject.GetOrAddComponent<PEListener>();

        imgTouchListener.onClickDown = (evt, args) =>
        {
            startPos = evt.position;

            imgDirPoint.color = new Color(1, 1, 1, 1f);
            imgDirBG.transform.position = evt.position;
        };

        imgTouchListener.onClick = (evt, args) =>
        {
            ResetState();
            OnClickDown?.Invoke();
        };
        imgTouchListener.onDrag = (evt, args) =>
        {
            Vector2 dir = evt.position - startPos; //获得 小点的 方向信息
            float len = dir.magnitude; //获得  小点可以移动的距离
            if (len > pointDis)
            {
                //把移动范围牵制在140
                Vector2 clampDir = Vector2.ClampMagnitude(dir, 140); 
                //起始点的位置  +加上计算出来的位置
                imgDirPoint.transform.position = startPos + clampDir; 
            }
            else
            {
                //我们点在哪里  就移动到哪里
                imgDirPoint.transform.position = evt.position; 
            }

            if (dir != Vector2.zero) 
            {
                if (!GameOver)
                {
                    ArrowRoot.gameObject.SetActive(true); 
                    float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);
                    //计算出来朝向的角度
                    ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
                }
            }

            InputMoveKey(dir.normalized); //把方向 归1
        };
        imgTouchListener.onClickUp = (evt, args) =>
        {
            ResetState();
        };
    }

    private void ResetState()
    {
        imgDirBG.transform.position = defaultPos; //把轮盘位置  还原
        imgDirPoint.color = new Color(1, 1, 1, 0.5f); //透明度设置为0.5  那个小点
        imgDirPoint.transform.localPosition = Vector2.zero; //把轮盘上的小点清零
        ArrowRoot.gameObject.SetActive(false);
        InputMoveKey(Vector2.zero); //手指抬起的适合 归0 
    }


    void InputMoveKey(Vector2 dir)    //获得 归1 后 的 方向向量
    {
        this.Dir = dir;
    }
    
}
public static class ClientConfig 
{
    public const int ScreenStandardWidth = 1920;   //高
    public const int ScreenStandardHeight = 1080;   //宽

    public const int ScreenOPDis = 140;  //点最大移动的范围
}
