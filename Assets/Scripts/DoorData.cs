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

    public bool isUnlocked = false;

    public DoorType doorType;
    public Vector3 spawnPoint;
    private Room NextRoom;
    private Room thisRoom;
    private Transform roomPosition;
    private Vector3 roomCameraPosition;

    void Start()
    {
        gameObject.layer = 7;
        roomPosition = transform.parent;
        thisRoom = transform.parent.GetComponent<Room>();

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

    public void UnlockOtherRoomsDoor()
    {
        switch (doorType)
        {
            case DoorType.Top:
                NextRoom.doors[1].isUnlocked = true;
                break;
            case DoorType.Bottom:
                if (NextRoom.isMain) return;
                NextRoom.doors[0].isUnlocked = true;
                break;
            case DoorType.Left:
                NextRoom.doors[3].isUnlocked = true;
                break;
            case DoorType.Right:
                NextRoom.doors[2].isUnlocked = true;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isUnlocked)
            {
                NextRoom = House.instance.SpawnRoom(spawnPoint);
                House.instance.TransitionToRoom(thisRoom.cameraVector, thisRoom.paletteNum);
                //House.instance.currentRoom = NextRoom;
                isUnlocked = true;
                UnlockOtherRoomsDoor();
            }
            else if (isUnlocked)
            {
                NextRoom = House.instance.GetRoom(spawnPoint);
                House.instance.TransitionToRoom(thisRoom.cameraVector, thisRoom.paletteNum);
               // House.instance.currentRoom = NextRoom;
            }
        }
        else Debug.Log("NO FUNCIONA");
    }
}