using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumera : MonoBehaviour
{
    public Transform player;//игрок
    public float speed = 2f;//скорость камеры
    
    public float normw = 5f;//коэф. увеличения коллайдера
    Rigidbody2D rig;//наш Rigidbody2D

    
    void Start()
    {
        //задаем размеры коллайдера камеры и Rigidbody2D
        float asp = transform.GetChild(0).GetComponent<Camera>().aspect;
        GetComponent<BoxCollider2D>().size = new Vector2(asp * normw, (asp * normw) / asp);
        rig = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        
        float d = Vector2.Distance(transform.position, player.position);//дистанция
        //если отдалился следуем за игроком
        if (d > 0.1f)
        {
            Vector3 v = (player.position - transform.position) * speed * Time.deltaTime;
            rig.velocity = (new Vector2(v.x, v.y));
        }
        else rig.Sleep();
    }
}
