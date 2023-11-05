using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElseNode : ContainerNode
{
    public override void CrossCompile()
    {
        CrossCompiler.RemoveTab();
        CrossCompiler.AddLine("else:");
        base.CrossCompile();
    }
    public override void Grab() { }
}
