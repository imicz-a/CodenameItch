using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNode : InstructionNode
{
    public TMPro.TMP_Dropdown typeTropdown;
    public override void CrossCompile()
    {
        arguments[0].CrossCompile();
        CrossCompiler.ccompiledString += " = ";
        switch (typeTropdown.value)
        {
            case (int)returnType.text:
                CrossCompiler.ccompiledString += "str(";
                arguments[1].CrossCompile();
                CrossCompiler.ccompiledString += ")";
                break;
            case (int)returnType.num:
                CrossCompiler.ccompiledString += "float(";
                arguments[1].CrossCompile();
                CrossCompiler.ccompiledString += ")";
                break;
            case (int)returnType.boolean:
                CrossCompiler.ccompiledString += "bool(";
                arguments[1].CrossCompile();
                CrossCompiler.ccompiledString += ")";
                break;
        }
        nextNode.CrossCompile();
    }
}
