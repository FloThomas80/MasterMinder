using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class MainBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] MasterMindAnswer;

    [SerializeField]
    private GameObject[] GuessAnswer;

    [SerializeField]
    private AppManager _AppManager;
    [SerializeField]
    private GameObject PawnScript;
    [SerializeField]
    private GameObject RowScript;

    [SerializeField]
    private Button GuessButton;



    [SerializeField]
    private GameObject[] Rows;

    private int _Goods;
    private int _Wrongs;

    private int[] Solution = new int[4];
    public int[] UserGuess = new int[4];

    private int rowNbr = 0;

    int[] TempColors;
    // Start is called before the first frame update
    void Start()
    {
        ChooseAnswer();
        Raz();
    }


    private void Update()
    {
        //FindObject();
 
        //GuessButton.clicked.Invoke();
        Guess();
    }

    



    private void ChooseAnswer()
    {
        for (int i = 0; i < MasterMindAnswer.Length; i++) //randomize
        {
            Material[] _Colors = _AppManager.GetColors();

            int RndMColor = Random.Range(0, _Colors.Length);
            MasterMindAnswer[i].GetComponent<Renderer>().material = _Colors[RndMColor];
            Solution[i] = RndMColor;
        }
        Debug.Log("A choice Has been made" + Solution[0]+" "+Solution[1]+ " " + Solution[2]+ " " + Solution[3]);
    }


    public void SetUserGuess(int[] Color)
    {
        UserGuess = Color;
    }


    private void Compare()
    {
        List<int> TempSol = Solution.ToList();
        List<int> TempGuess = UserGuess.ToList();

        for (int i = 0; i < UserGuess.Length; i++)
        {
            if (UserGuess[i] == Solution[i])
            {
                TempSol.Remove(UserGuess[i]);
                TempGuess.Remove(UserGuess[i]);
                _Goods++;
            }
        }

        for (int i = 0; i < TempGuess.Count; i++)
        {
            if (TempSol.Contains(TempGuess[i]))
            {
                TempSol.Remove(TempGuess[i]);
                _Wrongs++;
            }
        }
    }

    private void Raz()
    {
        _Goods = 0;
        _Wrongs = 0;
        for (int i = 0; i < Rows[rowNbr].GetComponent<RowScript>().GoodPlace.Length; i++)
        {
            Rows[rowNbr].GetComponent<RowScript>().GoodPlace[i].SetActive(false);
            Rows[rowNbr].GetComponent<RowScript>().WrongPlace[i].SetActive(false);
        }
        
    }
    private void CheckGoodWrong()
    {
        foreach (GameObject Go in Rows[rowNbr].GetComponent<RowScript>().GoodPlace)
        {
            //Go.SetActive(false);
            //}
            if (_Goods > 0)
            {
                for (int i = 0; i < _Goods; i++)
                {
                    Rows[rowNbr].GetComponent<RowScript>().GoodPlace[i].SetActive(true);
                }
            }
        }

        foreach (GameObject Go in Rows[rowNbr].GetComponent<RowScript>().WrongPlace)
        {
            //Go.SetActive(false);
            //}
            if (_Wrongs > 0)
            {
                for (int i = 0; i < _Wrongs; i++)
                {
                    Rows[rowNbr].GetComponent<RowScript>().WrongPlace[i].SetActive(true);
                }
            }
        }
    }

    private void NextLine()
    {
            Rows[rowNbr].GetComponent<RowScript>().DeActivateLine();

            rowNbr++;
            Rows[rowNbr].SetActive(true);
            
            StartCoroutine(WaitCoroutine());

            float newCamPos = (Camera.main.transform.localPosition.y) - 1.35f;
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, newCamPos, Camera.main.transform.localPosition.z); 
      }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        Raz();
        Rows[rowNbr].GetComponent<RowScript>().SetRowColors(UserGuess);
        

    }
    private void Guess()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (rowNbr < 11)
            {
                SetUserGuess(Rows[rowNbr].GetComponent<RowScript>().GetRowColors());
                Compare();
                CheckGoodWrong();
                if (_Goods == 4)
                {
                    _AppManager.GameWin();
                }
                else
                NextLine();
            }
        }
    }
}
