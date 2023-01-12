using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScript : MonoBehaviour
{

    //[SerializeField]
    //GameObject[] Pawns;
    [SerializeField]
    public GameObject[] GoodPlace;
    [SerializeField]
    public GameObject[] WrongPlace;

    [SerializeField]
    private MainBoard MainBoard;

    [SerializeField]
    private PawnScript[] _AnswerPawn;
    
    // Start is called before the first frame update
    public void DeActivateLine()
    {
        foreach (PawnScript _pawn in _AnswerPawn)
        {
            _pawn.DeActivatePawn();
        }
    }

    public int[] GetRowColors()
    {
        int[] RowColors= new int[4];
        for (int i = 0; i < _AnswerPawn.Length; i++)
        {
            RowColors[i] =  _AnswerPawn[i].GetColorIndex();
        }
            
        
        return RowColors;
    }
    public void SetRowColors(int[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            _AnswerPawn[i].ChangeColorto(colors[i]);
        }
    }

}
