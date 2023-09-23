using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomFurnitures : MonoBehaviour
{
    public Dictionary<Vector2Int, PlacementData> PlacementDatasInPosition = new();

    public void PlaceFurniture(Vector2Int position, FurnitureData furnitureData)
    {
        var data = furnitureData.originalData;

        List<Vector2Int> positionToOccupy = CalculatePositions(position, furnitureData.size);

        bool canPlace = positionToOccupy.Any(x => !PlacementDatasInPosition.ContainsKey(x) || Physics2D.OverlapCircle(x, 0.2f));

        if (canPlace)
        {
            foreach (var pos in positionToOccupy)
            {
                PlacementDatasInPosition[pos] = new PlacementData(positionToOccupy, data);
            }
        }
        else
        {
            foreach (var pos in positionToOccupy)
            {
                PlacementDatasInPosition[pos].furniture.compatibles.Contains(data);
            }
        }
        Vector3 veco = new Vector3(position.x, position.y, 0);

        Instantiate(furnitureData.prefab, veco, Quaternion.identity);
    }

    private List<Vector2Int> CalculatePositions(Vector2Int position, Vector2Int size)
    {
        List<Vector2Int> returnVal = new();
        for (int x = 0; x < Mathf.Abs(size.x); x++)
        {
            for (int y = 0; y < Mathf.Abs(size.y); y++)
            {
                returnVal.Add(position + new Vector2Int(x, y));
            }
        }
        return returnVal;
    }
}