using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField]
    private Material[] AnswerColors = new Material[] {};// where 1 red, 2 blue, 3 green, 4 orange, 5 purple, 6 yellow
    [SerializeField]
    private Material[] ResultColors = new Material[] {}; // where 1 is White 2 is Black

    private IUsableObject Touched;
    private void Update()
    {
        UseTarget(FindObject());
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

    public Material[] GetColors()
    {
        return AnswerColors;
    }
    private void UseTarget(IUsableObject usableObject)
    {
        if (Input.GetMouseButtonDown(0) && Touched != null)
        {
            usableObject.UseObject();
        }
    }


}
