using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncDecreaseOperator : InstructionNode
{
    public string op;
    public override void CrossCompile()
    {
        CrossCompiler.AddLine(string.Empty);
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += $" {op} ";
        arguments[1].CrossCompile();
        CrossCompiler.CompileNext(nextNode);
    }
}
