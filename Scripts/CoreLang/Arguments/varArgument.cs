using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class varArgument : InstructionArgument
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
            WarningUI.instance.DisplayWarning("Condition not assigned", "Instructions like while, repeat and if require a condition");
        }
    }
}
