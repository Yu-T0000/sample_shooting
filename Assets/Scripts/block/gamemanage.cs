using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gamemanage : MonoBehaviour
{
    public GameObject[] prefabArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemy.Length);
        if(enemy.Length == 0){
            reset();
        }
        
    }

    public void reset()
    { 
        SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
