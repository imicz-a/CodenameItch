using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNumberNode : VariableNode
{
    public TMPro.TMP_InputField input;

    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += "float(input(";
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += "))";
    }
}
