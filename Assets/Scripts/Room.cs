using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public DoorData[] doors;
    public Vector3 cameraVector;
    public bool isMain = false;

    public int paletteNum;

    public void Start() 
    {
        if (isMain)
        {
            House.instance.Habitaciones.Add(this.transform.position, this);
            paletteNum = 0;
            cameraVector = new Vector3(transform.position.x - 1, transform.position.y - 1, -3);
        }
    }

    public void Init()
    {
        paletteNum = Random.Range(0, ColourChanger.instance.colorPalettes.Length);
        cameraVector = new Vector3(transform.position.x - 1, transform.position.y - 1, -3);
    }
}
