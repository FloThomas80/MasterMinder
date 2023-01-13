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
    [SerializeField]
    private GameObject _LooseMessage;
    [SerializeField]
    private GameObject LatPannel;



    [SerializeField] private AppManager AppManager;
    private void OnEnable()
    {
        //C# : on inscrit la fonction LooseMessage à l'event OnGameWin.
        //grace à la propriété static nous devons juste trouver la classe AppManager
        AppManager.OnGameWin += WinMessage;
        AppManager.OnGameLoose += LooseMessage;
    }

    // Update is called once per frame
    private void WinMessage()
    {
        _WinMessage.SetActive(true);
        _WinAnim.SetActive(true);
        LatPannel.SetActive(false);

        StartCoroutine(MouveCam());

    }

    private void LooseMessage()
    {
        _LooseMessage.SetActive(true);
        LatPannel.SetActive(false);
        StartCoroutine(MouveCam());

    }

    private IEnumerator MouveCam()
    {
        for (int i = 0; i < 50; i++)
        {

        
        Vector3 start_pos = Camera.main.transform.localPosition;
        Vector3 offset = new Vector3(0, -0.33f, -0.2f);
        Vector3 FinalPose = start_pos + offset;
        Camera.main.transform.localPosition = FinalPose;
        yield return new WaitForSeconds(.01f);
        }
    }


}
