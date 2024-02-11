using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public Rigidbody2D gunbody;
    float dir;
    float angle;
    float nomalized;

    public float radius = 0.7f;

    public float rotateSpeed = 2;

    public GameObject playerObject;

    public float bulletSpeed;

    public float bulletAngleRange;

    public float bulletTimer;

    public int bulletcount;
    public float bulletInterval;

    public bullet_nom bulletPrefub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var player = playerObject.GetComponent<Player>();
        if(player.m_hp <= 0){
            Destroy( gameObject );
        }
        
        if (Input.GetKey(KeyCode.F)){
            angle = rotateSpeed;
            dir += angle;
        }
        else if (Input.GetKey(KeyCode.J)){
            angle = -1 * rotateSpeed;
            dir += angle;
        }

        nomalized = Mathf.Repeat(dir, 360);
        var rad = Mathf.Deg2Rad * nomalized;

        Transform pl = playerObject.transform;
        Vector2 plpos = pl.position;

        gunbody.MovePosition(
            new Vector2(
                radius * Mathf.Cos(rad)  + plpos.x,
                radius * Mathf.Sin(rad) + plpos.y

            )
        );
        transform.eulerAngles = new Vector3( 0f, 0f, nomalized - 90);

        bulletTimer += Time.deltaTime;
        if(bulletTimer < bulletInterval) return;

        bulletTimer = 0;

        ShootNWay( nomalized, bulletAngleRange, bulletSpeed, bulletcount);
        
    }

    private void ShootNWay(float angleBace, float angleRange, float speed, int count){

        var pos = transform.localPosition;
        var rot = transform.localRotation;

        if(1 < count){
            for(int i = 0; i < count; ++i){
                var angle = angleBace + angleRange * ((float)i/(count - 1) - 0.5f);
                var shot = Instantiate(bulletPrefub, pos, rot);
                shot.Init(angle,speed);
            }
        }
        else if(count == 1){
            var shot = Instantiate(bulletPrefub, pos, rot);
                shot.Init(angleBace,speed);

        }
    }
}
