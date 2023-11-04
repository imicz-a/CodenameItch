using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trueVarArgument : InstructionArgument
{
    [SerializeField] GameObject varField;
    public override void recieveArgument(VariableNode node)
    {
        base.recieveArgument(node);
        varField.SetActive(false);
        //inputelem.ignoreLayout = true;
    }
    public override void derecieveArgument()
    {
        varField.SetActive(true);
        //inputelem.ignoreLayout = false;
        base.derecieveArgument();

    }
    public override void CrossCompile()
    {
        if (isAssigned)
        {
            assignedVar.CrossCompile();
        }
        else
        {
            WarningUI.instance.DisplayWarning("Variable not assigned", "A set instruction requires a variable assigned");
        }
    }
}
