using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var client = GetComponent<uOSC.uOscClient>();
        client.Send("/motorAll", "hld", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            SceneManager.LoadScene("block");
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            var client = GetComponent<uOSC.uOscClient>();
            client.Send("/motorA", "stp", 0);
            SceneManager.LoadScene("block_con");
        }
        
    }
}
