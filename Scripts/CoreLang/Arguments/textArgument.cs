using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textArgument : InstructionArgument
{
    public TMPro.TMP_InputField input;
    public override void recieveArgument(VariableNode node)
    {
        base.recieveArgument(node);
        input.gameObject.SetActive(false);
    }
    public override void CrossCompile()
    {
        if (isAssigned)
        {
            assignedVar.CrossCompile();
        }
        else
        {
            CrossCompiler.ccompiledString += input.text;
        }
    }
}
