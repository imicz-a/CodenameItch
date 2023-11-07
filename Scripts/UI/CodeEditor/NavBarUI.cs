using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavBarUI : MonoBehaviour
{
    public void RunWrapper()
    {
        CrossCompiler.StartCrossCompile();
        ProgramOutput.instance.Open();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Return))
        {
            RunWrapper();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O))
        {
            ProgramOutput.instance.handleButton();
        }
    }
}
