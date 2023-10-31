using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableNode : LangNode
{
    InstructionArgument assignedArgument;
    public override void Grab()
    {
        base.Grab();
        if(assignedArgument != null)
        {
            assignedArgument.derecieveArgument();
            assignedArgument = null;
        }
    }
}