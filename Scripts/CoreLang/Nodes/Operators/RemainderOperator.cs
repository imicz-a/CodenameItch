using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainderOperator : VariableNode
{
    public override void CrossCompile()
    {
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += " % ";
        arguments[1].CrossCompile();
    }
}
