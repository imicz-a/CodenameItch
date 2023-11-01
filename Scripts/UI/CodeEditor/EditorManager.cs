using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] StartNode entryPoint;
    public static System.Action pythonExitAction;
    private void Start()
    {
        CrossCompiler.entryPoint = entryPoint;
    }
    private void Update()
    {
        if(pythonExitAction != null)
        {
            pythonExitAction.Invoke();
            pythonExitAction = null;
        }
    }
}
