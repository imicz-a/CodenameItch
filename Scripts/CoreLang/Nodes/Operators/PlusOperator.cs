using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOperator : VariableNode
{
    public override void CrossCompile()
    {
        returnType first = (arguments[0] as universalArgument).returnReturnType();
        returnType second = (arguments[1] as universalArgument).returnReturnType();

        if (first == returnType.num && second != returnType.num)
        {
            CrossCompiler.ccompiledString += "str(";
            arguments[0].CrossCompile();
            CrossCompiler.ccompiledString += ") + ";
            arguments[1].CrossCompile();
        }
        else if (first != returnType.num && second == returnType.num)
        {
            arguments[0].CrossCompile();
            CrossCompiler.ccompiledString += " + str(";
            arguments[1].CrossCompile();
            CrossCompiler.ccompiledString += ")";
        }
        else
        {
            arguments[0].CrossCompile();
            CrossCompiler.ccompiledString += " + ";
            arguments[1].CrossCompile();
        }
    }
}
