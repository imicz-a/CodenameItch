using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericOperator : VariableNode
{
    public string opertor;
    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += "(";
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += $") {opertor} (";
        arguments[1].CrossCompile();
        CrossCompiler.ccompiledString += ")";
    }
}
