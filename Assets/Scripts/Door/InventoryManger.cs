using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
    public static InventoryManger Instance;
    public List<AllTiems> _inventryItem = new List<AllTiems>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(AllTiems item)
    {
        if (!_inventryItem.Contains(item))
        {
            _inventryItem.Add(item);
        }
    }
    public void RemoveItem(AllTiems item)
    {
        if (_inventryItem.Contains(item))
        {
            _inventryItem.Remove(item);
        }
    }

    public enum AllTiems
    {
        keyRed,keyBlue,keyGreen
    }
}
