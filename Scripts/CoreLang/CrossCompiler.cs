using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

/// <summary>
/// Class for converting node to python code
/// </summary>
public static class CrossCompiler
{
    static string ccompiledString;
    static string tabstring;
    public static StartNode entryPoint;
    // Start is called before the first frame update
    /// <summary>
    /// adds a line to cross compiled code
    /// </summary>
    /// <param name="line">line to add</param>
    public static void AddLine(string line)
    {
        ccompiledString += tabstring + line + "\n";
    }
    public static void AddTab()
    {
        tabstring += '\t';
    }
    public static void RemoveTab()
    {
        tabstring.Remove(tabstring.Length - 1, 1);
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
        entryPoint.nextNode.CrossCompile();
    }
}
