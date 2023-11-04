using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numArgument : InstructionArgument
{
    public TMPro.TMP_InputField input;
    public override void recieveArgument(VariableNode node)
    {
        base.recieveArgument(node);
        input.gameObject.SetActive(false);
        //inputelem.ignoreLayout = true;
    }
    public override void derecieveArgument()
    {
        input.gameObject.SetActive(true);
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
            CrossCompiler.ccompiledString += input.text;
        }
    }
    public void onEndEdit()
    {
        if (input.text == string.Empty)
            return;
        if (!decimal.TryParse(input.text, out _))
        {
            WarningUI.instance.DisplayWarning("This should be a number",
                "The text inside this box must be a number");
            input.text = string.Empty;
        }
    }
}
