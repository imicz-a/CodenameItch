using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : ContainerNode
{
    public override void CrossCompile()
    {
        CrossCompiler.AddLine("for _ in range(");
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += "):";
        base.CrossCompile();
    }
}
