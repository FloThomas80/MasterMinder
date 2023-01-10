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
    private AppManager _AppManager;

    [SerializeField]
    private Button GuessButton;
    [SerializeField]
    private GameObject[] GoodPlace;
    [SerializeField]
    private GameObject[] WrongPlace;
    [SerializeField]
    private GameObject Row;

    private int _Goods;
    private int _Wrongs;
    private Vector3 RowCurrentposition;

    private int[] Solution = new int[4];
    private IUsableObject Touched;
    private int[] UserGuess= new int[4];
    // Start is called before the first frame update
    void Start()
    {
        ChooseAnswer();
        RowCurrentposition = Row.transform.position;
    }


    private void Update()
    {
        FindObject();
        UseTarget(FindObject());
        //GuessButton.clicked.Invoke();
        Guess();
    }

    public IUsableObject FindObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Touched = hit.collider.GetComponent<IUsableObject>();
                return Touched;
            }
        }
        return null;
    }

    private void UseTarget(IUsableObject usableObject)
    {
        if (Input.GetMouseButtonDown(0) && Touched != null)
        {
            usableObject.UseObject();
        }
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


    public void SetUserGuess(int Rank, int Color)
    {
        UserGuess[Rank] = Color;
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
                //TempGuess.Remove(TempSol[i]);
                _Wrongs++;
            }
        }
        
    }


    private void CheckGoodWrong()
    {
        foreach (GameObject Go in GoodPlace)
        {
            Go.SetActive(false);
        }
        if (_Goods > 0)
        {
            for (int i = 0; i < _Goods; i++)
            {
                GoodPlace[i].SetActive(true);
            }
        }

        foreach (GameObject Go in WrongPlace)
        {
            Go.SetActive(false);
        }
        if (_Wrongs > 0)
        {
            for (int i = 0; i < _Wrongs; i++)
            {
                WrongPlace[i].SetActive(true);
            }
        }
    
            _Goods= 0;
            _Wrongs= 0;
    }

    private void CreateNewLine()
    {
        RowCurrentposition.y = RowCurrentposition.y - 1.335f;
        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x,RowCurrentposition.y, Camera.main.transform.localPosition.y);
        Row = Instantiate(Row, RowCurrentposition, Quaternion.identity);
    }
    private void Guess()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Compare();
            CheckGoodWrong();
            CreateNewLine();
        }
    }
}
