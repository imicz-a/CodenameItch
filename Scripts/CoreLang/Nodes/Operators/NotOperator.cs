using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotOperator : VariableNode
{
    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += "(not (";
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += $"))";
    }
}
