using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class feel : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text txt;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        var manager = Manager.m_instance;
        var emote = manager.emote;
        txt.SetText(emote);
        
    }
}
