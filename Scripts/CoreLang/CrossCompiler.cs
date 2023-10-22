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
#if UNITY_EDITOR_OSX
        var python = new Process();
        python.StartInfo.FileName = "/usr/bin/python3";
        python.StartInfo.Arguments = $"\"{Application.dataPath}/program.py\"";
        python.StartInfo.RedirectStandardOutput = true;
        python.StartInfo.UseShellExecute = false;
        python.Start();
        python.WaitForExit();
        UnityEngine.Debug.Log(python.StandardOutput.ReadToEnd());
    #endif
    #if UNITY_EDITOR_WIN
        var python = Process.Start(Application.dataPath + "/pythonInterpreter/win/python.exe",
                $"\"{Application.dataPath}/program.py\"");
    #endif
    }
    public static void StartCrossCompile()
    {
        entryPoint.nextNode.CrossCompile();
    }
}
