using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintNode : LangNode
{
    public TMPro.TMP_InputField input;
    public string message;

    public override void CrossCompile()
    {
        CrossCompiler.AddLine($"print(\"{message}\")");
        CrossCompiler.CompileNext(nextNode);
    }

    public void Start()
    {
        input.onEndEdit.AddListener(delegate { onInputEnd(); });
    }

    void onInputEnd()
    {
        message = input.text;
    }
}
