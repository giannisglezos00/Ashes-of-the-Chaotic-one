using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickup : MonoBehaviour
{
    public InventoryItemData itemData;

    private BoxCollider2D myCollider;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

        myCollider.isTrigger = true;
        mySpriteRenderer.sprite = itemData.Icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if ( !inventory ) return;
        
        if (inventory.InventorySystem.AddToInventory(itemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
