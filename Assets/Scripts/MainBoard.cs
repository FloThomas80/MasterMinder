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

    private int[] Solution = new int[4];
    private IUsableObject Touched;
    private int[] UserGuess= new int[4];
    // Start is called before the first frame update
    void Start()
    {
        ChooseAnswer();
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
        Debug.Log("change to" + UserGuess[0] + UserGuess[1]+ UserGuess[2]+ UserGuess[3]);
    }


    private void Compare()
    {
        for (int i = 0; i < UserGuess.Length; i++)
        {
            if (UserGuess[i] == Solution[i])
            {
                Debug.Log("un bien placé");
            }else if (Solution.Contains(UserGuess[i]))
            {
                Debug.Log("un Mal placé");
            }
        }
    }

    private void Guess()
    {
            if(Input.GetKeyDown(KeyCode.U))
        {
            Compare();
        }
    }
}
