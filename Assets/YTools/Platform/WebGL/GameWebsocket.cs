using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

public class GameWebsocket
{
    private GameWebsocket()
    {
    }

    private static class GameWebsocketInstance
    {
        public static GameWebsocket INSTANCE = new GameWebsocket();
    }

    public static GameWebsocket Instance => GameWebsocketInstance.INSTANCE;

    public WebSocket socket;

    public event EventHandler<OpenEventArgs> OnOpen;
    public event EventHandler<CloseEventArgs> OnClose;
    public event EventHandler<ErrorEventArgs> OnError;
    public event EventHandler<MessageEventArgs> OnMessage;

    // 订阅
    Dictionary<int, Action<object>> subscriber = new Dictionary<int, Action<object>>();

    /// <summary>
    /// 授权码
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 连接地址
    /// </summary>
    public string WebSocketURL { get; set; }

    public void ConnectAsync(string wsUrl)
    {
        if (socket != null && (socket.ReadyState == WebSocketState.Open || socket.ReadyState == WebSocketState.Connecting))
        {
            return;
        }
        WebSocketURL = wsUrl;

        socket = new WebSocket(wsUrl);
        socket.OnOpen += Socket_OnOpen;
        socket.OnClose += Socket_OnClose;
        socket.OnMessage += Socket_OnMessage;
        socket.OnError += Socket_OnError;
        socket.ConnectAsync();

        
    }

    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
        Debug.Log("连接成功");
        OnOpen?.Invoke(this, e);
    }

    private void Socket_OnMessage(object sender, MessageEventArgs e)
    {
        OnMessage?.Invoke(this, e);
#if UNITY_EDITOR
        Debug.Log(e.Data);
#endif
    }

    private void Socket_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("关闭连接");
        OnClose?.Invoke(this, e);
    }

    private void Socket_OnError(object sender, ErrorEventArgs e)
    {
        Debug.Log($"出现错误：{e.Message}");
        OnError?.Invoke(this, e);
    }

    public void Send<T>(T message)
    {
        socket?.SendAsync(message.ToString());
    }

    public int Send<T>(string type, T data)
    {
        var message = new GameMessage<T>
        {
            type = type,
            data = data
        };
        socket?.SendAsync(message.ToString());
        return message.id;
    }

    public void Send<T>(string type, T data, Action<object> action)
    {
        int id = Send(type, data);
        if (!subscriber.ContainsKey(id))
        {
            subscriber.Add(id, action);
        }
    }

    public void SendWithResult<T, TResult>(string type, T data, Action<TResult> action)
    {
        Send(type, data, (obj) =>
        {
            var result = JsonConvert.DeserializeObject<TResult>(data.ToString());
            action(result);
        });
    }
}