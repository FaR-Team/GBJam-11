using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : MonoBehaviour
{
    public enum DoorType
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public DoorType doorType;
    public Vector3 spawnPoint;
    private Room MyRoom;
    private Transform roomPosition;

    void Start()
    {
        roomPosition = transform.parent;
        SetSpawnPoint();
    }

    void SetSpawnPoint()
    {
        if (roomPosition == null)
        {
            roomPosition = transform.parent;
        }
        switch (doorType)
        {
            case DoorType.Top:
                spawnPoint = new Vector3(roomPosition.position.x, roomPosition.position.y + 9, roomPosition.position.z);
                break;
            case DoorType.Bottom:
                spawnPoint = new Vector3(roomPosition.position.x, roomPosition.position.y - 9, roomPosition.position.z);
                break;
            case DoorType.Left:
                spawnPoint = new Vector3(roomPosition.position.x - 10, roomPosition.position.y, roomPosition.position.z);
                break;
            case DoorType.Right:
                spawnPoint = new Vector3(roomPosition.position.x + 10, roomPosition.position.y, roomPosition.position.z);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (MyRoom == null)
            {
                MyRoom = House.instance.SpawnRoom(spawnPoint, doorType);
                Camera.main.transform.position = MyRoom.cameraVector;
            }
            else if (MyRoom != null)
            {
                House.instance.GetRoom(spawnPoint);
                Camera.main.transform.position = MyRoom.cameraVector;
            }

        }
        else
        {
            Debug.Log("NO FUNCIONA");
        }
    }
}