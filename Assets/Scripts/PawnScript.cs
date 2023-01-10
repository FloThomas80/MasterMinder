using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PawnScript : MonoBehaviour,IUsableObject
{
    

    [SerializeField]
    private AppManager _AppManager;

    [SerializeField]
    private int IDPawn;

    [SerializeField]
    private MainBoard _MainBoard;
    private Material[] _Colors;

    int indx;
    private void Start()
    {
        indx = 0;
        _Colors = _AppManager.GetColors();
        GetComponent<Renderer>().material = _Colors[indx];
    }



    public void UseObject()
    {
        
        if (indx < _Colors.Length)
            indx++;
        else
            indx = 0; 
        GetComponent<Renderer>().material = _Colors[indx];

        _MainBoard.SetUserGuess(IDPawn, indx);
    }



}
