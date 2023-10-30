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
        varParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, numofvars * 50);
        varParent.anchoredPosition = new Vector2(0, numofvars * 25);
        varParentContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100 + numofvars * 50);
    }
}
