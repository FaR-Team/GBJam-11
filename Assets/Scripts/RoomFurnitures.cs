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
        bool placeOnTop = false;

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
                if(!PlacementDatasInPosition.ContainsKey(pos)) break;

                // Se fija si todas las posiciones que va a ocupar el objeto estan dentro de las posiciones que ocupa el objeto debajo, si es compatible y si no hay ya algo encima
                canPlace = positionToOccupy.Intersect(PlacementDatasInPosition[pos].occupiedPositions).Count() ==
                           positionToOccupy.Count() 
                           && PlacementDatasInPosition[pos].furniture.compatibles.Contains(data)
                           && PlacementDatasInPosition[pos].instantiatedFurnitureOnTop == null;
                
                placeOnTop = canPlace;
                
                break;
            }
        }
        
        if(!canPlace) return false;
        
        Vector3 vector = new Vector3(position.x, position.y, 0);

        // Guardamos el objeto que instanciamos en en cada PlacementData
        GameObject furniturePrefab = Instantiate(furnitureData.prefab, vector, Quaternion.Euler(furnitureData.VectorRotation));
        
        if(!placeOnTop) positionToOccupy.ForEach(pos => PlacementDatasInPosition[pos].instantiatedFurniture = furniturePrefab);
        else
        {
            // Si el objeto va encima de otro, lo guardamos en el PlacementData
            positionToOccupy.ForEach(pos =>
            {
                PlacementDatasInPosition[pos].instantiatedFurnitureOnTop = furniturePrefab;
                PlacementDatasInPosition[pos].furnitureOnTop = data;
            });
        }
        
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

    public void RemoveDataInPositions(List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            PlacementDatasInPosition.Remove(pos);
        }
    }

    public void RemoveTopObjectInPositions(List<Vector2> positions)
    {
        foreach (var pos in positions)
        {
            PlacementDatasInPosition[pos].furnitureOnTop = null;
            //PlacementDatasInPosition[pos].instantiatedFurnitureOnTop = null;
        }
    }
}