using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManger.AllTiems _iemsType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManger.Instance.AddItem(_iemsType);
            Destroy(gameObject);
        }
    }
}
