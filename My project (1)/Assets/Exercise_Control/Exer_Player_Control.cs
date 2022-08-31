using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exer_Player_Control : MonoBehaviour
{

    public string exercise;//название задания
    public GameObject[] need_objects;//нужные обьекты
    bool addExer = false;//если добавили задание
    bool chekExer = false;//проверка на выполнение 
    public bool isExer = false;//выполнено ли задание

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //если задание есть и нужно его проверить
        if (addExer && chekExer)
        {
            for(int i = 0; i < need_objects.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < GetComponent<Inventory>().items.Length; j++)
                {
                    if (GetComponent<Inventory>().items[j] == need_objects[i])
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag) { isExer = false; break; } else isExer = true;
            }
            chekExer = false;
        }
    }

    //сетеры
    public void Set_Exercise(string e) { exercise = e; }
    public void Set_Need_Objects(GameObject[] n) { need_objects = n; }
    public void Set_AddExer(bool isex) { addExer = isex; }
    public void Set_ChekExer(bool chex) { chekExer = chex; }
    //гетеры
    public bool Get_isExer() {return isExer; }
}
