using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public static House instance;
    public GameObject[] roomPrefabs;
    public GameObject selectedPrefab;
    public static Dictionary<Vector3, Room> Habitaciones = new Dictionary<Vector3, Room>();

    void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

    public Room SpawnRoom(Vector3 position, DoorData.DoorType doorType)
    {
        if (!Habitaciones.TryGetValue(position, out Room room)) 
        {
            RandomizeRoom();
            Room _room = Instantiate(selectedPrefab, position, Quaternion.identity).GetComponent<Room>();
            Habitaciones.Add(position, _room);
            _room.cameraVector = new Vector3(position.x - 1, position.y - 1, -3);

            switch (doorType)
            {
                case DoorData.DoorType.Top:
                    _room.isTopOpen = true;
                    break;
                case DoorData.DoorType.Bottom:
                    _room.isBottomOpen = true;
                    break;
                case DoorData.DoorType.Left:
                    _room.isLeftOpen = true;
                    break;
                case DoorData.DoorType.Right:
                    _room.isRightOpen = true;
                    break;
                default:
                    break;
            }
            return _room;
        }
        else return null;
    }

    public Room GetRoom(Vector3 position)
    {
        if (Habitaciones.TryGetValue(position, out Room room)) 
        {
            Room _room = room;
            _room.cameraVector = new Vector3(position.x - 1, position.y - 1, -3);
            return _room;
        }
        else return null;
    }

    void RandomizeRoom()
    {
        int index = Random.Range(0, roomPrefabs.Length);
        selectedPrefab = roomPrefabs[index];
    }
}