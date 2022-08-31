using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exer_Collection : Talk_Mes
{
    
    public GameObject Canvas;//канвас
    public GameObject pointer;//указатель на взаимодействие
    public GameObject dialog;//диалоговое окно (префаб)

    GameObject dialog1;//диалоговое окно (обьект на сцене)
    bool flag_stay= false;//рядом ли обьект
    public int mes_ind = 0;//индекс предложения
    bool isExe = false;//

    Collider2D o;//кто подошел

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //заставляем указатель и диалог быть над обьектом
        if (pointer.activeInHierarchy)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            pointer.GetComponent<RectTransform>().position = screenPos;
        }
        if (dialog1 != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            dialog1.GetComponent<RectTransform>().position = screenPos;
        }
        //если стоим рядом
        if(flag_stay && !isExe)
        {
            //если выполнили задание
            if (Input.GetKeyDown(KeyCode.Space) && o.GetComponent<Exer_Player_Control>().isExer) 
            {
                Down_Last_But();
                Down_One_But();
                //Delet_Obj();
                isExe = true;
                return;
            }
            //если говорим
            if (Input.GetKeyDown(KeyCode.Space)  && dialog1 != null) Down_Next();
            //если начали говорить
            if (Input.GetKeyUp(KeyCode.Space) && dialog1 == null && ((mes_ind + 2) < messages.Length || isExe)) Down_One_But();
            //if (isExe) { Delet_Obj(); }
        }
    }

    //если встали рядом
    private void OnTriggerEnter2D(Collider2D other)
    {
        o = other;
        flag_stay = true;
    }

    //если отошли
    private void OnTriggerExit2D (Collider2D other)
    {
        Down_Last_But();
        flag_stay = false;
    }



    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////
    /// </summary>



    //действия при первом нажатии
    public void Down_One_But()
    {
            pointer.SetActive(false);
            dialog1 = Instantiate(dialog);
            dialog1.transform.SetParent(Canvas.transform);
            dialog1.GetComponentInChildren<Text>().text = messages[mes_ind];
    }

    //переход на новый диалог
    public void Down_Next()
    {
        if ((mes_ind + 2) >= messages.Length && !pointer.activeInHierarchy)
        {
            Down_Last_But();
            if (exercise != null && exercise != "") { 
                o.GetComponent<Exer_Player_Control>().Set_Exercise(exercise); 
                o.GetComponent<Exer_Player_Control>().Set_Need_Objects(need_objects);
                o.GetComponent<Exer_Player_Control>().Set_AddExer(true);
                o.GetComponent<Exer_Player_Control>().Set_ChekExer(true);
            }
        }
        if ((mes_ind + 2) < messages.Length)
        {
            dialog1.GetComponentInChildren<Text>().text = messages[mes_ind + 1];
            mes_ind++;
        }
        if ((mes_ind + 2) == messages.Length) mes_ind++;
        
        
    }


    //если отошли от обьекта
    public void Down_Last_But()
    { 
        //если вышли то включаем указатель и удаляем диалог
        pointer.SetActive(true);
        Destroy(dialog1);
        //если мы не закончили диалог начинаем сначала
        if ((mes_ind + 2) < messages.Length) mes_ind = 0;
    }
    /*
    public void Delet_Obj()
    {
        o.GetComponent<Inventory>().DeletItem();
    }*/
}
