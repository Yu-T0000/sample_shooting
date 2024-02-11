using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class reward_total : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text txt;
    public float reward = 0f;
    void Start()
    {

        
    }

    public void reward_manager(float re){
        reward += re;
    }
    public void reward_reset(){
        reward = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        txt.SetText("reward : {0:3}", reward);
        
    }
}
