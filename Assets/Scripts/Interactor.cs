using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayer;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(Inventory playerInventory)
    {
        var gridPosition = GridManager.PositionToCellCenter(transform.position);
        // Si existe un PlacementData en el punto de interaccion, 
        if (House.instance.currentRoom.roomFurnitures.PlacementDatasInPosition.TryGetValue(gridPosition, out PlacementData placementData))
        {
            Debug.Log("hay placement data en: " + (Vector2) gridPosition);
            if (playerInventory.furnitureInventory != null)
            {
                return;
            }
            
            if (placementData.furnitureOnTop == null)
            {
                playerInventory.furnitureInventory = placementData.furniture;
                playerInventory.EnablePackageUI(true);
                Destroy(placementData.instantiatedFurniture);
                House.instance.currentRoom.roomFurnitures.RemoveDataInPositions(placementData.occupiedPositions);
                
            }
            else
            {
                playerInventory.furnitureInventory = placementData.furnitureOnTop;
                playerInventory.EnablePackageUI(true);
                Destroy(placementData.instantiatedFurnitureOnTop);
                House.instance.currentRoom.roomFurnitures.RemoveTopObjectInPositions(placementData.occupiedPositions);
                
            }
            
            return;
        }
        else Debug.Log("no hay placement data en: " + (Vector2) gridPosition);
        
        // Si no hay PlacementData, chequear si hay una puerta
        var door = Physics2D.OverlapCircle(transform.position, 0.2f, doorLayer);
        
        if (door)
        {
            // Desbloquear habitacion si hay guita
            door.TryGetComponent(out DoorData doorData);
            
            if(doorData) doorData.BuyNextRoom();
        }
    }
}