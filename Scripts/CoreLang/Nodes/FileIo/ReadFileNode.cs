using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadFileNode : InstructionNode
{
    public override void CrossCompile()
    {
        CrossCompiler.AddLine("with open(");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += ", 'r') as file:";
        CrossCompiler.AddLine("\t");
        arguments[1].CrossCompile();
        CrossCompiler.ccompiledString += " = file.read()";
        CrossCompiler.CompileNext(nextNode);
    }
}
