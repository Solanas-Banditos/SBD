using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0;//скорость
    public Animator anim;//анимация

    Rigidbody2D rig;//наш Rigidbody2D

    // Start is called before the first frame update
    void Start()
    {
        rig = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (y != 0)
            anim.SetBool("Back", true);
        else anim.SetBool("Back", false);
        
        Vector3 vect_loc = Vector3.right * x * speed + Vector3.up * y * speed;
        rig.velocity = vect_loc;
        if (y == 0 && x == 0) { rig.velocity = new Vector2(0f, 0f); }
    }
}
