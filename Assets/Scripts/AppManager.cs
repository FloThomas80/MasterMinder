using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    //private static List<int> AnswerColors = new List<int>();
    //private static List<int> ResultColors = new List<int>();
    [SerializeField]
    private Material[] AnswerColors = new Material[] {};// where 1 red, 2 blue, 3 green, 4 orange, 5 purple, 6 yellow
    [SerializeField]
    private Material[] ResultColors = new Material[] {}; // where 1 is White 2 is Black


    public Material[] GetColors()
    {
        return AnswerColors;
    }

}
