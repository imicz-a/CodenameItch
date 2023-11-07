using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNode : VariableNode
{
    public TMPro.TMP_InputField input;

    public override void CrossCompile()
    {
        CrossCompiler.ccompiledString += "input(";
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += ")";
    }
}
