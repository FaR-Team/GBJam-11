using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    static GridManager instance;

    public static Grid _grid => instance.grid;

    private void Awake()
    {
        instance = this;
    }
}