using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base interface for all language nodes - all those nodes you programm with
/// </summary>
public interface LangNode
{
    public LangNode nextNode { get; set; }
    public void Init();
    public void CrossCompile();
}
