using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Act : MonoBehaviour
{
    public float alpha_speed = 1f;//скорость изменения прозрачности
    public float alpha_side = 0.3f;//граница прозрачности
    bool exit_flag = false;//если вышел из комнаты

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //если вышел из комнаты постепенно возращаем непрозрачность
        if (exit_flag) {
            float alpha = GetComponent<SpriteRenderer>().color.a;
            if (alpha < 1)
            {
                alpha = alpha + alpha_speed * Time.deltaTime;
                Color color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, alpha);
                GetComponent<SpriteRenderer>().color = color;
            }
            else
                exit_flag = false;
        }
    }
    //пока в комнате постепенно уменьшаем А канал
    private void OnTriggerStay2D(Collider2D other)
    {
        float alpha = GetComponent<SpriteRenderer>().color.a;
        if (alpha > alpha_side)
        {
            alpha = alpha - alpha_speed*Time.deltaTime;
            Color color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                GetComponent<SpriteRenderer>().color.b, alpha);
            GetComponent<SpriteRenderer>().color = color;
        }
        exit_flag = false;
    }
    //если вышел из комнаты ставим флаг
    private void OnTriggerExit2D(Collider2D other) { exit_flag = true; }
}
