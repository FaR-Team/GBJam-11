using System;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public FurnitureOriginalData furnitureInventory;
    public int money;
    public TextMeshProUGUI moneyText;
    public GameObject packageUI;


    public void UpdateMoney(int intMoney)
    {
        money += intMoney;
        moneyText.text = money.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Package")) return;

        if (furnitureInventory != null) return;

        furnitureInventory = Package._furnitureInPackage;
        Package.package.SetActive(false);
        EnablePackageUI(true);
        
        Debug.Log("inventario asignado");
    }

    public void EnablePackageUI(bool enabled)
    {
        packageUI.SetActive(enabled);
    }
}
