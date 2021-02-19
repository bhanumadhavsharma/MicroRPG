using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    
    public void PickupItem()
    {
        FindObjectOfType<Player>().AddItemToInventory(itemName);
        Destroy(this.gameObject);
    }
}
