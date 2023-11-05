using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOperator : VariableNode
{
    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += "(";
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += ") ** (";
        arguments[1].CrossCompile();
        CrossCompiler.ccompiledString += "(";
    }
}
