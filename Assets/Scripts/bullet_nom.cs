using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_nom : MonoBehaviour
{

    public Vector3 bullet_v;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void FixedUpdate() {
        transform.localPosition += bullet_v;
    }
    public void Init(float angle, float speed){
        var direction = util.GetDirection(angle);
        bullet_v = direction * speed;
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        Destroy(gameObject, 2);

    }
}
