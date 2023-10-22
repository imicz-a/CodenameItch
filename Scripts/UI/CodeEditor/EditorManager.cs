using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField] StartNode entryPoint;
    private void Start()
    {
        CrossCompiler.entryPoint = entryPoint;
    }
}
