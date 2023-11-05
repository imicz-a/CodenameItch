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
        if (s == string.Empty)
            return;
        if(s == "file" || s == "_")
        {
            namefield.text = string.Empty;
            return;
        }
        name = s;
        Destroy(namefield);
        RecalculateSize();
        var par = namefield.transform.GetChild(0);
        foreach(Transform child in par)
        {
            if(child.gameObject.name == "Caret")
            {
                Destroy(child.gameObject);
            }
        }
    }
    new void RecalculateSize()
    {
        float sum = namefield.preferredWidth + 20;
        (transform as RectTransform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sum);
    }
}
