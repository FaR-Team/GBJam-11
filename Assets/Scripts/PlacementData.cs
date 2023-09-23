using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementData
{
    public List<Vector2Int> occupiedPositions;

    public FurnitureOriginalData furniture;
    public PlacementData(List<Vector2Int> occupiedPositions, FurnitureOriginalData furniture)
    {
        this.occupiedPositions = occupiedPositions;
        this.furniture = furniture;
    }

    public bool IsCompatibleWith(FurnitureOriginalData furnitureData) //es compatible con, esta data.
    {
        return furniture.compatibles.Contains(furnitureData);
    }
}