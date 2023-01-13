using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class BoutonScript : MonoBehaviour
{


    [SerializeField]
    private MainBoard MainBoard;
    [SerializeField]
    private AppManager _AppManager;

    [SerializeField]
    private Button thisbutton;

    private void Start()
    {
        //C# : on inscrit la fonction LooseMessage à l'event OnGameWin.
        //grace à la propriété static nous devons juste trouver la classe AppManager
        //Button thisbutton =  GetComponent<Button>();
        //thisbutton.onClick.RemoveAllListeners();
        thisbutton.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        _AppManager.GuessPress();
    }

}
