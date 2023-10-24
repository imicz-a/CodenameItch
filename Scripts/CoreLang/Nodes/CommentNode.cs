using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentNode : LangNode
{
    public override void CrossCompile()
    {
        nextNode.CrossCompile();
    }
}
