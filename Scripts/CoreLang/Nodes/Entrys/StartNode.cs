using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : MonoBehaviour, LangNode
{
    public LangNode nextNode { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void CrossCompile()
    {
        throw new System.NotImplementedException("An start node is not cross compiled!");
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }
}
