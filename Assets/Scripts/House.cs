using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public static House instance;

    public GameObject[] roomPrefabs;
    public GameObject[] roomTopPrefabs;
    public GameObject[] roomBottomPrefabs;
    public GameObject[] roomLeftPrefabs;
    public GameObject[] roomRightPrefabs;
    public GameObject[] roomTopLeftCornerPrefabs;
    public GameObject[] roomTopRightCornerPrefabs;
    public GameObject[] roomBottomLeftCornerPrefabs;
    public GameObject[] roomBottomRightCornerPrefabs;
    
    public GameObject selectedPrefab;
    
    public Dictionary<Vector3, Room> Habitaciones = new Dictionary<Vector3, Room>();

    public static Room currentRoom;


    public int roomHeight = 9;
    public int roomWidth = 10;

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
            int x = (int)position.x / roomWidth;
            int y = (int)position.y / roomHeight;
            WichRoomToSpawnPos(x, y);
            Room _room = Instantiate(selectedPrefab, position, Quaternion.identity).GetComponent<Room>();
            _room.Init();
            Habitaciones.Add(position, _room);
            _room.cameraVector = new Vector3(position.x, position.y, -3);
            return _room;
        }
        else return GetRoom(position);
    }

    public void WichRoomToSpawnPos(int x, int y)
    {
        if (x == -7 && y == 0)
        {
            RandomizeRoom("TopLeftCorner");
        }
        else if (x == 8 && y == 0)
        {
            RandomizeRoom("TopRightCorner");
        }
        else if (x == -7 && y == -15)
        {
            RandomizeRoom("BottomLeftCorner");
        }
        else if (x == 8 && y == -15)
        {
            RandomizeRoom("BottomRightCorner");
        }
        else if (x == -7)
        {
            RandomizeRoom("left");
        }
        else if (x == 8)
        {
            RandomizeRoom("right");
        }
        else if (y == 0)
        {
            RandomizeRoom("Top");
        }
        else if (y == -15)
        {
            RandomizeRoom("Bottom");
        }
        else
        {
            RandomizeRoom("center");
        }
    }

    public Room GetRoom(Vector3 position)
    {
        if (Habitaciones.TryGetValue(position, out Room room))
        {
            room.cameraVector = new Vector3(position.x, position.y, -3);
            return room;
        }
        else return null;
    }


    public void TransitionToRoom(Vector3 position, int color)
    {
        // Si ya estamos en la habitacion, no mover, pensar mejor forma de hacer que no se interactue con las
        // puertas de la habitacion en la que ya estamos
        if (_mainCam.transform.position == position) return;
        StopAllCoroutines();
        StartCoroutine(MoveCamNextRoom(position, color));
    }

    IEnumerator MoveCamNextRoom(Vector3 position, int color)
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
        ColourChanger.instance.ChangeColour(color);
        yield return null;
    }

    void RandomizeRoom(string type)
    {
        if (type == "center")
        {
            int index = Random.Range(0, roomPrefabs.Length);
            selectedPrefab = roomPrefabs[index];
        }
        else if (type == "Top")
        {
            int index = Random.Range(0, roomTopPrefabs.Length);
            selectedPrefab = roomTopPrefabs[index];
        }
        else if (type == "Bottom")
        {
            int index = Random.Range(0, roomBottomPrefabs.Length);
            selectedPrefab = roomBottomPrefabs[index];
        }
        else if (type == "left")
        {
            int index = Random.Range(0, roomLeftPrefabs.Length);
            selectedPrefab = roomLeftPrefabs[index];
        }
        else if (type == "Right")
        {
            int index = Random.Range(0, roomRightPrefabs.Length);
            selectedPrefab = roomRightPrefabs[index];
        }
        else if (type == "TopLeftCorner")
        {
            int index = Random.Range(0, roomTopLeftCornerPrefabs.Length);
            selectedPrefab = roomTopLeftCornerPrefabs[index];
        }
        else if (type == "TopRightCorner")
        {
            int index = Random.Range(0, roomTopRightCornerPrefabs.Length);
            selectedPrefab = roomTopRightCornerPrefabs[index];
        }
        else if (type == "BottomLeftCorner")
        {
            int index = Random.Range(0, roomBottomLeftCornerPrefabs.Length);
            selectedPrefab = roomBottomLeftCornerPrefabs[index];
        }
        else if (type == "BottomRightCorner")
        {
            int index = Random.Range(0, roomBottomRightCornerPrefabs.Length);
            selectedPrefab = roomBottomRightCornerPrefabs[index];
        }
    }
}