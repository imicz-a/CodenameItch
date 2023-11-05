using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteToFileNode : InstructionNode
{
    public override void CrossCompile()
    {
        CrossCompiler.AddLine("with open(");
        arguments[1].CrossCompile();
        CrossCompiler.ccompiledString += ", 'w') as file:";
        CrossCompiler.AddLine("\tfile.write(");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += ")";
        CrossCompiler.CompileNext(nextNode);
    }
}
