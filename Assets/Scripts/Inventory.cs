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
        if(money < 0) moneyText.text = "0";
        else moneyText.text = money.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Package")) return;

        if (furnitureInventory != null) return;
        TimerManager.StopTimer();

        furnitureInventory = Package._furnitureInPackage;
        Package.package.SetActive(false);
        EnablePackageUI(true);
    }

    public void EnablePackageUI(bool enabled)
    {
        packageUI.SetActive(enabled);
    }
}
