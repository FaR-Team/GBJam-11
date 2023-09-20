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
    public Vector3 SpawnPosition;

    public Transform thisDoorSpawnRoomPosition;

    public void WhatDoorType(DoorType doorType)
    {
        switch (doorType)
        {
            case DoorType.Top:

                break;
            default:
                break;
        }
    }
}
