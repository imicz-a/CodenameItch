using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LangNode
{
    public LangNode next { get; set; }
    public void Exec();
    public void Spawn();
    public void UpdateUI();
    public string displayName { get; set; }
}