﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PackagesGenerator : MonoBehaviour
{
    [SerializeField] private FurnitureOriginalData[] allFurnitures;
    [SerializeField] private FurnitureOriginalData wife;
    private bool isWife;
    private int quantityOfObjectSpawned = 0;

    [SerializeField] private GameObject packageGO;
    [SerializeField] private GameObject mainDoor;

    [SerializeField] private List<FurnitureOriginalData> possibleFurnitures;
    [SerializeField] private List<FurnitureOriginalData> deletedFurnitures;
    private void Start()
    {
        InvokeRepeating("GeneratePackage", 2f, 5f);
        SetPossibleFurnitures();
    }

    private void GeneratePackage()
    {
        if (packageGO.activeInHierarchy) return;

        packageGO.SetActive(true);

        FurnitureOriginalData packageData = packageGO.GetComponent<Package>().furnitureInPackage = GetRandomFurniture();
        gameObject.transform.parent.GetComponent<MainRoom>().CheckIfLose(packageData);
    }

    private FurnitureOriginalData GetRandomFurniture()
    {

        if(quantityOfObjectSpawned >= 25 && Random.Range(0, 1f) <= 0.1f && !isWife)
        {
            isWife = true;
            Debug.Log("Sweetieee. I'm Home <3");
            return wife;
        }

        if (deletedFurnitures.Count != 0 && Random.Range(0, 1f) <= 0.5f)
        {
            int index_deleted = GetRandomValueIn(deletedFurnitures);
            FurnitureOriginalData value = deletedFurnitures[index_deleted];
            deletedFurnitures.RemoveAt(index_deleted);
            possibleFurnitures.Add(value);
        }
        if (possibleFurnitures.Count == 0)
        {
            deletedFurnitures.Clear();
            SetPossibleFurnitures();
        }

        int index_possibles = GetRandomValueIn(possibleFurnitures);

        FurnitureOriginalData furniture = possibleFurnitures[index_possibles];
        possibleFurnitures.RemoveAt(index_possibles);
        deletedFurnitures.Add(furniture);

        quantityOfObjectSpawned++;

        return furniture;
    }

    private int GetRandomValueIn(List<FurnitureOriginalData> list)
    {
        return Random.Range(0, list.Count);
    }
    private void SetPossibleFurnitures()
    {
        foreach (var f in allFurnitures)
        {
            possibleFurnitures.Add(f);
        }

    }
}

public class Timer : MonoBehaviour
{

    private void Update()
    {
        
    }
}