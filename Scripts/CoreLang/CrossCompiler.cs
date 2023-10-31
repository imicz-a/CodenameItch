using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Class for converting nodes to python code
/// </summary>
public static class CrossCompiler
{
    public static string ccompiledString;
    static string tabstring;
    public static StartNode entryPoint;
    static List<VariableNode> varNodes;
    // Start is called before the first frame update
    /// <summary>
    /// adds a line to cross compiled code
    /// </summary>
    /// <param name="line">line to add</param>
    public static void AddLine(string line)
    {
        ccompiledString += "\n" + tabstring + line;
    }
    public static void AddTab()
    {
        tabstring += '\t';
    }
    public static void RemoveTab()
    {
        tabstring.Remove(tabstring.Length - 1, 1);
    }
    public static void AddVar(VariableNode node)
    {

    }
    public static void CompileNext(LangNode next)
    {
        if(next == null)
        {
            FinalizeCompilation();
            return;
        }
        next.CrossCompile();
    }
    static void FinalizeCompilation()
    {
        File.WriteAllText(Application.dataPath + "/program.py", ccompiledString);
        var python = new Process();
        if(Application.platform == RuntimePlatform.OSXEditor)
            python.StartInfo.FileName = "/usr/bin/python3";
        if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            python.StartInfo.FileName = Application.dataPath + "/pythonInterpreter/win/python.exe";
        python.StartInfo.Arguments = $"\"{Application.dataPath}/program.py\"";
        python.StartInfo.RedirectStandardOutput = true;
        python.StartInfo.UseShellExecute = false;
        python.Start();
        python.WaitForExit();
        ProgramOutput.instance.Write(python.StandardOutput.ReadToEnd());
    }
    public static void StartCrossCompile()
    {
        foreach(var v in varNodes)
        {
            AddLine($"{v.name} = None");
        }
        entryPoint.nextNode.CrossCompile();
    }
}
