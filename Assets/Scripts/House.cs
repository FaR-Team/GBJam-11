using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public static House instance;
    public GameObject[] roomPrefabs;
    public GameObject selectedPrefab;
    public Dictionary<Vector3, Room> Habitaciones = new Dictionary<Vector3, Room>();

    void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }

    public Room SpawnRoom(Vector3 position)
    {
        if (!Habitaciones.TryGetValue(position, out Room room)) 
        {
            RandomizeRoom();
            Room _room = Instantiate(selectedPrefab, position, Quaternion.identity).GetComponent<Room>();
            _room.Init();
            Habitaciones.Add(position, _room);
            _room.cameraVector = new Vector3(position.x - 1, position.y - 1, -3);
            return _room;
        }
        else return GetRoom(position);
    }

    public Room GetRoom(Vector3 position)
    {
        if (Habitaciones.TryGetValue(position, out Room room)) 
        {
            room.cameraVector = new Vector3(position.x - 1, position.y - 1, -3);
            return room;
        }
        else return null;
    }

    public void TransitionToRoom(Vector3 position)
    {
        StartCoroutine(MoveCamNextRoom(position));
    }

    IEnumerator MoveCamNextRoom(Vector3 position)
    {
        float elapsedTime = 0;
        float waitTime = .7f;

        while (elapsedTime < waitTime)
        {
            Camera.main.transform.position = Vector3.Lerp(position, Camera.main.transform.position, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    void RandomizeRoom()
    {
        int index = Random.Range(0, roomPrefabs.Length);
        selectedPrefab = roomPrefabs[index];
    }
}