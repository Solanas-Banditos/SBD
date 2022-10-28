using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Act : MonoBehaviour
{
    public float alpha_speed = 2f;//скорость изменения прозрачности
    public float alpha_side = 0.3f;//граница прозрачности
    bool exit_flag = false;//если вышел из комнаты
    
    Collider2D objOther;//объект из которого вышли
    public float deltFixBackUp = 0.00001f;//изменение положения по Z

    float zTrans = 0f;

    void Start()
    {
        zTrans = transform.position.z;
    }

    void FixedUpdate()
    {
        if (exit_flag) Clear_ComeBack(objOther);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Object_BackFix_Up") 
        {
            transform.position = new Vector3
                (transform.position.x, 
                transform.position.y,
                other.transform.position.z + deltFixBackUp);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Object_Clear")
        {
            exit_flag = false;
            Clear_ComeOn(other);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Object_Clear" )
        {
            objOther = other;
            exit_flag = true;
        }
        //
        if (other.tag == "Object_BackFix_Up")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zTrans);
        }
    }
    

    //прозрачность вкл.
    private void Clear_ComeOn(Collider2D other)
    {
        SpriteRenderer spriteOther = other.transform.parent.GetComponent<SpriteRenderer>();
        float alpha = spriteOther.color.a;
        if (alpha > alpha_side)
        {
            alpha = alpha - alpha_speed * Time.deltaTime;
            Color color = new Color(spriteOther.color.r, spriteOther.color.g,
                spriteOther.color.b, alpha);
            spriteOther.color = color;
        }
    }

    //прозрачность выкл.
    private void Clear_ComeBack(Collider2D other)
    {
        SpriteRenderer spriteOther = other.transform.parent.GetComponent<SpriteRenderer>();
        float alpha = spriteOther.color.a;
        if (alpha < 1)
        {
            alpha = alpha + alpha_speed * Time.deltaTime;
            Color color = new Color(spriteOther.color.r, spriteOther.color.g,
            spriteOther.color.b, alpha);
            spriteOther.color = color;
        }
        else
            exit_flag = false;
    }


}
