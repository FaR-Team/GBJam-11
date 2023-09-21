using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public static House instance;
    public GameObject[] roomPrefabs;
    public GameObject selectedPrefab;
    public Dictionary<Vector3, Room> Habitaciones = new Dictionary<Vector3, Room>();

    [SerializeField] private float roomTransitionTime = .3f;
    private Camera _mainCam;

    void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }

        _mainCam = Camera.main;
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
        // Si ya estamos en la habitacion, no mover, pensar mejor forma de hacer que no se interactue con las
        // puertas de la habitacion en la que ya estamos
        if (_mainCam.transform.position == position) return;
        StopAllCoroutines();
        StartCoroutine(MoveCamNextRoom(position));
    }

    IEnumerator MoveCamNextRoom(Vector3 position)
    {
        Vector3 initialCameraPos = _mainCam.transform.position;
        // Igualamos la distancia en Z para evitar problemas
        position.z = initialCameraPos.z;
        
        float elapsedTime = 0;
       // float waitTime = roomTransitionTime;

        while (elapsedTime < roomTransitionTime)
        {
            _mainCam.transform.position = Vector3.Lerp(initialCameraPos, position, elapsedTime / roomTransitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Para que se complete el Lerp
        _mainCam.transform.position = Vector3.Lerp(initialCameraPos, position, 1f);
        yield return null;
    }

    void RandomizeRoom()
    {
        int index = Random.Range(0, roomPrefabs.Length);
        selectedPrefab = roomPrefabs[index];
    }
}