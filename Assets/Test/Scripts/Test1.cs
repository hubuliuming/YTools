

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using YFramework;
using Random = UnityEngine.Random;


public class Test1 : YMonoBehaviour
{
    
    private void Start()
    {
        Random.InitState(4);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Poll(1000, SelectMode.SelectRead);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Random.value);
        }

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var moment = new Vector3(x, 0, y);

        moment = Vector3.ClampMagnitude(moment, 5);
        
        moment = transform.TransformDirection(moment);
        
    }
}


