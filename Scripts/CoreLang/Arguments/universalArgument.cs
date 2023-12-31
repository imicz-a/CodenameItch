using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class universalArgument : InstructionArgument
{
    public TMPro.TMP_InputField input;
    public override void CrossCompile()
    {
        base.CrossCompile();
        if (isAssigned)
        {
            assignedVar.CrossCompile();
        }
        else
        {
            if(decimal.TryParse(input.text, out _))
            {
                CrossCompiler.ccompiledString += input.text;
            }
            else
            {
                CrossCompiler.ccompiledString += $"\"{input.text}\"";
            }
        }
    }
    public returnType returnReturnType()
    {
        base.CrossCompile();
        if (isAssigned)
        {
            return assignedVar.rtype;
        }
        else
        {
            if (decimal.TryParse(input.text, out _))
            {
                return returnType.num;
            }
            else
            {
                return returnType.text;
            }
        }
    }
    public override void recieveArgument(VariableNode node)
    {
        base.recieveArgument(node);
        input.gameObject.SetActive(false);
    }
    public override void derecieveArgument()
    {
        input.gameObject.SetActive(true);
        //inputelem.ignoreLayout = false;
        base.derecieveArgument();
    }
}
