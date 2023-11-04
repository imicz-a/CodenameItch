using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableNode : LangNode
{
    public returnType rtype;
    public InstructionArgument assignedArgument;
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
public enum returnType
{
    text,
    num,
    boolean,
    noidea,
    notApplicable,
}