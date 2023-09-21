using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    DoorData[] doors;

    public bool isTopOpen = false;
    public bool isBottomOpen = false;
    public bool isLeftOpen = false;
    public bool isRightOpen = false;

    public Vector3 cameraVector = new Vector3(0,0,0);


}
