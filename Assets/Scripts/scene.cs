using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OscJack;

public class scene : MonoBehaviour
{
    static string serverip = "127.0.0.1";
    OscClient client = new OscClient(serverip, 9067);
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            client.Send("/sleep", "");
            this.GetComponent<Controller_Brain>().pause();
            SceneManager.LoadScene("pause");
        }
        
    }
}
