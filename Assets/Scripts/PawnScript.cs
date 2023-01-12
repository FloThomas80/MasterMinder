using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PawnScript : MonoBehaviour,IUsableObject
{
    [SerializeField]
    private AppManager _AppManager;

    [SerializeField] private Material[] _Colors;
    [SerializeField] private Renderer _renderer;

    int indxColor;
    private void Start()
    {
        _Colors = _AppManager.GetColors();
    }



    public void UseObject()
    {
        if (indxColor >= _Colors.Length-1)
            indxColor = 0; 
        else
            indxColor++;
        _renderer.material = _Colors[indxColor];
    }

    public void ChangeColorto(int ColorChoice)
    {
        _renderer.material = _Colors[ColorChoice];
    }

    public int GetColorIndex()
    { 
        for (int i = 0; i < _Colors.Length; i++)
        {
            if (_Colors[i].color == _renderer.material.color)
            {
                indxColor = i;
            }
        }
        return indxColor;
    }
    private void ActivatePawn()
    {
        GetComponent<Collider>().enabled= true;
    }
    public void DeActivatePawn()
    {
        GetComponent<Collider>().enabled = false;
    }


}
