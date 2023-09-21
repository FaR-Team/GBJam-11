using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomFurnitures : MonoBehaviour
{
    public Dictionary<Vector3Int, PlacementData> PlacementDatasInPosition = new();

    public bool CanPutFurnitureIn(Vector3Int key)
    {

        return PlacementDatasInPosition.ContainsKey(key);
    }

    public void PlaceFurniture(Vector3Int position, Vector2Int size)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(position, size);

        

        foreach(var pos in positionToOccupy)
        {
            if (PlacementDatasInPosition.ContainsKey(pos)) PlacementDatasInPosition[pos] = new PlacementData(positionToOccupy, id, furniture); 
            else { PlacementDatasInPosition[pos] = data; }
        }
        if (CanPutFurnitureIn(position))
        {

        }
        else
        {

        }

        PlacementDatasInPosition.Add(position, go);
    }

    private List<Vector3Int> CalculatePositions(Vector3Int position, Vector2Int size)
    {
        throw new System.NotImplementedException();
    }

    public PlacementData GetPlacementData(Vector3Int position)
    {
        return PlacementDatasInPosition.GetValueOrDefault(position, new());
    }
}
public class PlacementData
{
    public List<Vector2Int> occupiedPositions;

    public int id;

    FurnitureData furniture;
    public PlacementData(List<Vector2Int> occupiedPositions, int id, FurnitureData furniture)
    {
        this.occupiedPositions = occupiedPositions;
        this.id = id;
        this.furniture = furniture;
    }

    public bool IsCompatibleWith(FurnitureData furnitureData)
    {
        return furniture.compatibles.Contains(furnitureData);
    }
}


public class FurnitureData : ScriptableObject
{
    public int Id;
    public string Name;
    public Vector2Int size;
    public FurnitureData[] compatibles; //Los objetos que son compatibles con este objeto, más no así, los que este son compatibles con.
}


public class Furniture : MonoBehaviour
{
    FurnitureData data;
}

public class FurniturePreview : MonoBehaviour
{
    FurnitureData data;
    Vector3Int[] position;
    void PutFurniture()
    {
        RoomFurnitures actualRoom = House.ActualRoom().roomFurnitures; //todavia no tenemos eventos

        PlacementData placementData = actualRoom.GetPlacementData(position);

        actualRoom.PlaceFurniture(position, data.size);
    }
}

public enum TypeOfFurniture
{
    Altos, Bajos
}

public class GridManager : MonoBehaviour
{
    public Grid grid;
    static GridManager instance;
 
    public static Grid _grid => instance.grid;

    private void Awake()
    {
        instance = this;
    }
}