using System;
using UnityEngine;

[CreateAssetMenu(fileName ="new Furniture", menuName ="Muebles")]
public class FurnitureOriginalData : ScriptableObject
{
    public string Name;
    public Vector2Int size;
    public GameObject prefab;
    public FurnitureOriginalData[] compatibles; //Los objetos que son compatibles con este objeto, más no así, los que este son compatibles con.
}