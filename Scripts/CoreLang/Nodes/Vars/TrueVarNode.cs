using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueVarNode : VariableNode
{
    public TMPro.TMP_InputField namefield;
    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += name;
    }
    public void onEndEdit(string s)
    {
        name = s;
        namefield.interactable = false;
        namefield.image.raycastTarget = false;
        RecalculateSize();
    }
    void RecalculateSize()
    {
        float sum = namefield.preferredWidth + 20;
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sum);
    }
}
