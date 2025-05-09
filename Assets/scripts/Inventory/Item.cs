using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [SerializeField] private Sprite sprite;
   [TextArea] [SerializeField] private string itemDescription;
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player picked up item!");

            if (inventoryManager != null)
            {
                inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
                Debug.Log("Item added to inventory: " + itemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("InventoryManager is not found!");
            }
        }
    }
}