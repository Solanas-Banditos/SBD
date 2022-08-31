using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;
    public Transform inventory;

    //добавление в массив
    public void AddItem(GameObject obj)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) { 
                items[i] = obj.GetComponent<Item>().main;
                Vis_AddItem(items[i]);
                Destroy(obj); break; 
            }
        }
        GetComponent<Exer_Player_Control>().Set_ChekExer(true);
    }
    //добавление в окно
    public void Vis_AddItem(GameObject obj)
    {
        for (int i = 0; i < inventory.childCount; i++)
        {
            if(inventory.GetChild(i).childCount == 0)
            {
                GameObject o = Instantiate(obj.GetComponent<Item>().main_inv);
                o.transform.SetParent(inventory.GetChild(i));
                obj.GetComponent<Item>().main_inv = o;
                break;
            }
        }
    }

    /*public void DeletItem()
    {
        GameObject[] obj = GetComponent<Exer_Player_Control>().need_objects;
        for (int i = 0; i < obj.Length; i++)
        {
            for (int j = 0; j < items.Length; j++)
            {
                if(obj[i] == items[j])
                {
                    Vis_DeletItem(items[j]);
                    items[j] = null;
                }
            }
        }
    }
    //добавление в окно
    public void Vis_DeletItem(GameObject obj)
    {
        for (int i = 0; i < inventory.childCount; i++)
        {
            if (inventory.GetChild(i).transform.GetChild(0) == obj.GetComponent<Item>().main_inv)
            {
                Destroy(inventory.GetChild(i).transform.GetChild(0));
                break;
            }
        }
    }*/


}
