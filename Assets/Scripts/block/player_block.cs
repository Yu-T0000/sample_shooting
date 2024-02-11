using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_block : MonoBehaviour
{
    private float input_x;
    // Start is called before the first frame update
    void Start()
    {
        input_x = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_pos = this.transform.position;
        Vector3 scale = this.transform.localScale;
        var scale_h = scale.x /2;

        player_pos.x = input_x;
        var pos_right = player_pos.x + scale_h;
        var pos_left = player_pos.x - scale_h;
        Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.MovePosition(player_pos);
        if(pos_right > 8.5f){
            player_pos.x = 8.5f - scale_h;
        }
        if(pos_left < -8.5f){
            player_pos.x = -8.5f + scale_h;
        }

        if (Input.GetKey(KeyCode.RightArrow)){
            if(pos_right<8.5f){
                input_x += 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(pos_left>-8.5f){
                input_x -= 0.1f;
            }
        }


    }
}
