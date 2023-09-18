using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreation : MonoBehaviour
{
    public GameObject[] roomArray;

    public Transform thisDoorSpawnRoomPosition;

    public void CreateRoom()
    {
        Instantiate(roomArray[Random.Range(0, roomArray.Length)], thisDoorSpawnRoomPosition.position, Quaternion.identity);
    }
}
