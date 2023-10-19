using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDisplay : MonoBehaviour
{

}
[CreateAssetMenu(menuName = "node/Entry")]
public class EntryData : ScriptableObject, LangNode
{
    public LangNode next { get; set; }
    public string displayName { get; set; } = "";
    public GameObject template;
    public void Exec()
    {
        next.Exec();
    }
    public void Spawn()
    {
        GameObject clone = Instantiate(template);
        
    }
    public void UpdateUI()
    {
        throw new System.Exception("But why would you do that?");
    }
}