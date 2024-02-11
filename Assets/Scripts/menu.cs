using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            SceneManager.LoadScene("normal");
        }
        else if(Input.GetKeyDown(KeyCode.J)){
            SceneManager.LoadScene("controller");
        }
        
    }
}
