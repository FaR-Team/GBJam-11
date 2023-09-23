using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class RoomFurnitures : MonoBehaviour
{
    public Dictionary<Vector2, PlacementData> PlacementDatasInPosition = new();

    public bool PlaceFurniture(Vector2 position, FurnitureData furnitureData)
    {
        var data = furnitureData.originalData;

        List<Vector2> positionToOccupy = CalculatePositions(position, furnitureData.size);

        bool canPlace = !positionToOccupy.Any(x => PlacementDatasInPosition.ContainsKey(x) || Physics2D.OverlapCircle(x, 0.2f));

        if (canPlace)
        {
            foreach (var pos in positionToOccupy)
            {
                PlacementDatasInPosition[pos] = new PlacementData(positionToOccupy, data);
            }
        }
        else
        {
            // Mejorar esta parte, capaz fijarnos si es compatible arriba dentro del Any() y aca retornar
            foreach (var pos in positionToOccupy)
            {
                if(!PlacementDatasInPosition.ContainsKey(pos)) continue;
                canPlace = PlacementDatasInPosition[pos].furniture.compatibles.Contains(data);
            }
        }
        
        if(!canPlace) return false;
        
        Vector3 veco = new Vector3(position.x, position.y, 0);

        Instantiate(furnitureData.prefab, veco, Quaternion.Euler(furnitureData.VectorRotation));
        return true;

    }

    private List<Vector2> CalculatePositions(Vector2 position, Vector2Int size)
    {
        List<Vector2> returnVal = new();
        for (int x = 0; x < Mathf.Abs(size.x); x++)
        {
            for (int y = 0; y < Mathf.Abs(size.y); y++)
            {
                // Multiplicamos por el signo para que sepa si el size es negativo o positivo
                returnVal.Add(position + new Vector2(x * Mathf.Sign(size.x), y * Mathf.Sign(size.y)));
            }
        }
        return returnVal;
    }
}