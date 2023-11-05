using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileNode : ContainerNode
{
    public override void CrossCompile()
    {
        CrossCompiler.AddLine("while(");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += "):";
        base.CrossCompile();
    }
}
