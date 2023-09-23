using UnityEngine;
using UnityEngine.InputSystem;

public class FurniturePreview : MonoBehaviour
{
    [SerializeField] private FurnitureOriginalData data;
    [SerializeField] private InputAction inputMaster;
    [SerializeField] private FurnitureData furnitureData;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Sprite[] sprites;
    
    private Vector2Int originalSize;

    /* *
     
     
     
     */
    int rotation = 0;
    private Vector2Int position;

    private void Awake()
    {
        originalSize = data.size;
        furnitureData.size = data.size;
        furnitureData.prefab = data.prefab;
        furnitureData.compatibles = data.compatibles;
        furnitureData.originalData = data;
    }
    private void Update()
    {

    }

    void PutFurniture()
    {
        Vector3Int cellPos = GridManager._grid.WorldToCell(transform.position);
        position.x = cellPos.x;
        position.y = cellPos.y;

        House.currentRoom.roomFurnitures.PlaceFurniture(position, furnitureData); //todavia no tenemos eventos
    }

    void Rotate()
    {
        rotation++;

        if (rotation >= 4) rotation = 0;

        switch (rotation)
        {
            case 0:
                furnitureData.VectorRotation = new Vector3Int(0, 0, 0);

                furnitureData.size = originalSize;
                break;
            case 1:
                furnitureData.VectorRotation = new Vector3Int(0, 0, 90);

                furnitureData.size.x = -originalSize.y;
                furnitureData.size.y = originalSize.x;
                break;
            case 2:
                furnitureData.VectorRotation = new Vector3Int(0, 0, 180);

                furnitureData.size = -originalSize;
                break;
            case 3:
                furnitureData.VectorRotation = new Vector3Int(0, 0, 270);

                furnitureData.size.x = originalSize.y;
                furnitureData.size.y = -originalSize.x;
                break;
            default:
                break;
        }
        transform.rotation = Quaternion.Euler(furnitureData.VectorRotation);
    }
}
public struct FurnitureData
{
    public FurnitureOriginalData originalData;
    public Vector2Int size;
    public GameObject prefab;
    public FurnitureOriginalData[] compatibles;
    public Vector3Int VectorRotation;
}