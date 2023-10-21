using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class LangNodeManager
{
    static StartNode entryPoint;
    /// <summary>
    /// Created a LangNode based on type you provide
    /// </summary>
    /// <param name="nodeType">This is the type of node to create</param>
    public static string compiledString;
    public static void CreateNode(System.Type nodeType)
    {
        if (!nodeType.GetInterfaces().Any((System.Type t) =>
        {
            if (t == typeof(LangNode))
                return true;
            return false;
        }))
            throw new System.Exception("this is not a lang node");

        GameObject go = new GameObject();
        var c = (LangNode)go.AddComponent(nodeType);
        c.Init();
    }
    public static void CrossCompile()
    {
        entryPoint.nextNode.CrossCompile();
    }
}
