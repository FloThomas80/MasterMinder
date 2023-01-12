using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private GameObject _WinMessage;
    [SerializeField]
    private GameObject _WinAnim;

    [SerializeField] private AppManager AppManager;
    private void OnEnable()
    {
        //C# : on inscrit la fonction LooseMessage à l'event OnGameWin.
        //grace à la propriété static nous devons juste trouver la classe AppManager
        AppManager.OnGameWin += WinMessage;
    }

    // Update is called once per frame
    private void WinMessage()
    {
        _WinMessage.SetActive(true);
        _WinAnim.SetActive(true);
    }
}
