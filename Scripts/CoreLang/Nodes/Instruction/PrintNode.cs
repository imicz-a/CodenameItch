using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintNode : InstructionNode
{
    public TMPro.TMP_InputField input;

    public override void CrossCompile()
    {
        CrossCompiler.AddLine("print(\"");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += "\")";
        CrossCompiler.CompileNext(nextNode);
    }
}
