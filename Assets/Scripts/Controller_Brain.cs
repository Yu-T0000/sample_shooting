using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Cysharp.Threading.Tasks;
using uOSC;
using OscJack;

public class Controller_Brain : MonoBehaviour
{
    uOscClient send_to_python;

    OscClient client;

    public int state = 0;
     public void Awake()
    {
        
        send_to_python = GetComponent<uOSC.uOscClient>();
        client = new OscClient("172.17.5.23", 8067);

    }
    public void OnMessage(){
        Debug.Log("received");
        Confuse();
    }
    async void Confuse(){
        state = 0;
        Debug.Log("Confuse()");
        send_to_python.Send("/motorA", "fwd",40);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        send_to_python.Send("/motorB", "rev",30);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        send_to_python.Send("/motorB", "fwd",50);
        await UniTask.Delay(TimeSpan.FromMilliseconds(5));
        send_to_python.Send("/motorA", "rev",30);
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        send_to_python.Send("/motorB", "stp",0);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        send_to_python.Send("/motorA", "stp",0);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        client.Send("/damage2");
        Debug.Log("Confuse():end");
        send_to_python.Send("/motorAll", "stp",0);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        send_to_python.Send("/motorAll", "stp",0);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }

    public void pause(){
        client.Send("/pause");
    }
    void Update(){
    }

}