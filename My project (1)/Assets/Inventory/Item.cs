using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject main;//наш преваб
    public GameObject main_inv;//наша иконка

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            other.GetComponent<Inventory>().AddItem(gameObject);
        }

    }
}
