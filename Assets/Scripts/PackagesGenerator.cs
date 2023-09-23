using UnityEngine;
using Random = UnityEngine.Random;

public class PackagesGenerator : MonoBehaviour
{
    [SerializeField] private FurnitureOriginalData[]   possibleFurnitures;
    [SerializeField] private GameObject     packageGO;
    [SerializeField] private GameObject     mainDoor;

    private void Start()
    {
        InvokeRepeating("GeneratePackage", 2f, 5f);
    }

    private void GeneratePackage()
    {
        if (packageGO.activeInHierarchy) return;

        packageGO.SetActive(true);
        packageGO.GetComponent<Package>().furnitureInPackage = possibleFurnitures[GetRandomValue()];
    }

    private int GetRandomValue()
    {
        return Random.Range(0, possibleFurnitures.Length);
    }
}