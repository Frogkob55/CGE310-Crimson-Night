using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKryy : MonoBehaviour
{
    [SerializeField] Keybutton _switchKeybutton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _switchKeybutton.DoorLockedStatus();
            Destroy(gameObject);
        }
    }
}
