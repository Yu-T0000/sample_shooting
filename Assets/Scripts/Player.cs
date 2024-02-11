using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int m_hpMax; // HP の最大値
    public int m_hp; // HP

    public static Player m_instance;


    // Start is called before the first frame update
    private void Awake()
{
    m_instance = this;
    m_hp = m_hpMax; // HP
}

    

    void Start()
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        Rigidbody2D player = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(x*10, 0);
        player.AddForce(force);
        
    }
    void Restart()
    {
        Vector2 resetPosition = new Vector2(0,0);
        Transform player = GetComponent<Transform>();
        m_hp = m_hpMax;
        player.position = resetPosition;
    }
    public void Damage( int damage )
{
    // HP を減らす
    m_hp -= damage;

    // HP がまだある場合、ここで処理を終える
    if ( 0 < m_hp ) return;


    Destroy( GameObject.Find("Enemy"));
    Restart();
}
}
