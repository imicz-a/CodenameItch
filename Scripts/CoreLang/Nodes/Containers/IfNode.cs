using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfNode : ContainerNode
{
    public ElseNode enode;
    public override void CrossCompile()
    {
        CrossCompiler.AddLine("if(");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += "):";
        CrossCompiler.AddTab();
        if (nextNode == enode)
        {
            CrossCompiler.AddLine("pass");
        }
        CrossCompiler.CompileNext(nextNode);
    }
}
