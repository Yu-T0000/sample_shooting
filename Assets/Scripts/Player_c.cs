using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_c : MonoBehaviour
{
    public int m_hpMax; // HP の最大値
    public int m_hp; // HP

    public static Player_c m_instance;
    //private float x = 0;
    private float y = 0;

public void OnMessage(float value){

    y = value / 180;

}

    // Start is called before the first frame update
    private void Awake()
{
    m_instance = this;
    m_hp = m_hpMax; // HP
}

    

    void Start()
    {
        
    }

    void Restart()
    {
        Vector2 resetPosition = new Vector2(0,0);
        Transform player = GetComponent<Transform>();
        m_hp = m_hpMax;
        player.position = resetPosition;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //x = Random.Range(-2f,2f);
        Vector2 force = new Vector2(y*25, 0);
        Rigidbody2D player = GetComponent<Rigidbody2D>();
        player.AddForce(force);
        
    }
    public void Damage( int damage )
{
    // HP を減らす
    m_hp -= damage;
    //GameObject.Find("OSC").GetComponent<ControllerAgent>().c_AddReward(0.1f,0f,-0.1f);
    GameObject.Find("OSC").GetComponent<ControllerAgent1>().c_AddReward(0.1f,0f,-0.1f);

    if ( 0 >= m_hp ){
    Debug.Log("broken()");
    Destroy( GameObject.Find("Enemy"));
    //GameObject.Find("OSC").GetComponent<ControllerAgent>().broken();
    GameObject.Find("OSC").GetComponent<ControllerAgent1>().broken();
    Restart();
    //SceneManager.LoadScene (SceneManager.GetActiveScene().name);
    }
}
}
