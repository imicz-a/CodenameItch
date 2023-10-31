using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableAdder : MonoBehaviour
{
    public GameObject template;
    public RectTransform varParent;
    public RectTransform varParentContainer;
    int numofvars = 0;
    public void AddVar()
    {
        numofvars++;
        varParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, numofvars * 40);
        varParent.anchoredPosition = new Vector2(0, numofvars * 20);
        varParentContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + numofvars * 40);
        var clone = Instantiate(template, varParent);
    }
}