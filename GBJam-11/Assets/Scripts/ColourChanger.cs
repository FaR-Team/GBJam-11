#pragma warning disable CS0108
#pragma warning disable CS0108
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColourChanger : MonoBehaviour
{
  public Material mat;
  /*
    _Darkest
    _Dark
    _Light
    _Lightest
  */
    public Color Darkest, Dark, Light, Lightest;
    public ColorPalette[] colorPalettes;
    private MeshRenderer renderer;
    public int num;

    void Start()
    {
        renderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(num >= colorPalettes.Length) num = 0;
        
        renderer.material.SetColor("_Darkest", colorPalettes[num].Darkest);
        renderer.material.SetColor("_Dark", colorPalettes[num].Dark);
        renderer.material.SetColor("_Light", colorPalettes[num].Light);
        renderer.material.SetColor("_Lightest", colorPalettes[num].Lightest);
    }
}