using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("Added " + quantity + " of " + itemName + " to inventory.");
        // Add logic to store the item in your inventory system
    }
    public GameObject InventoryMenu;
    private bool menuActivated;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}
