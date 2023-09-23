using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public FurnitureOriginalData furnitureInventory;
    [SerializeField] private GameObject packageUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Package")) return;

        if (furnitureInventory != null) return;

        furnitureInventory = Package._furnitureInPackage;
        Package.package.SetActive(false);
        packageUI.SetActive(true);
        
        Debug.Log("inventario asignado");
    }
}
