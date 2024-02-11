using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public gamemanage Manager;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 vec = new Vector2(3.0f, 3.0f);

        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = vec;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ball_pos = this.transform.position;
        if(ball_pos.y < -5.5f){
            Manager.reset();

        }
        
    }
}
