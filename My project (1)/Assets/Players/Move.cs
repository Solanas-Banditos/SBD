using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0;//скорость
    public Animator anim;//анимация

    Rigidbody2D rig;//наш Rigidbody2D

    bool boolInvers = false;

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

        //обработка анимки
        if (Input.GetKey(KeyCode.W))
            anim.SetBool("Back", true);
        else anim.SetBool("Back", false);
        if (x < 0 & !boolInvers)
        {
            transform.localScale = transform.localScale + new Vector3(-2 * transform.localScale.x, 0f, 0f);
            boolInvers = true;
        }
        else if (x > 0 & boolInvers)
        {
            transform.localScale = transform.localScale + new Vector3(-2 * transform.localScale.x, 0f, 0f);
            boolInvers = false;
        }

        //обработка движухи
        if (y != 0 || x != 0)
        {
            Vector3 vect_loc = Vector3.right * x * speed + Vector3.up * y * speed;
            rig.velocity = vect_loc;
        }
        if (y == 0 && x == 0) { rig.velocity = new Vector2(0f, 0f); }
    }
}
