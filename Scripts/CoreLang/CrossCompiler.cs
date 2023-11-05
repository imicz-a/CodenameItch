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
    static string tabstring = string.Empty;
    public static StartNode entryPoint;
    static List<TrueVarNode> varNodes = new();
    static Process python;
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
        tabstring = tabstring.Remove(tabstring.Length - 1);
    }
    public static void AddVar(TrueVarNode node)
    {
        varNodes.Add(node);
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
        tabstring = string.Empty;
        UnityEngine.Debug.Log("Finalizing compilation");
        if (python != null)
        {
            UnityEngine.Debug.Log("Python already present");
            return;
        }
        File.WriteAllText(Application.dataPath + "/program.py", ccompiledString);
        python = new Process();
        if(Application.platform == RuntimePlatform.OSXEditor)
            python.StartInfo.FileName = "/usr/bin/python3";
        if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            python.StartInfo.FileName = Application.dataPath + "/pythonInterpreter/win/python.exe";
        python.StartInfo.Arguments = $"\"{Application.dataPath}/program.py\"";
        python.StartInfo.RedirectStandardOutput = true;
        python.StartInfo.RedirectStandardError = true;
        python.StartInfo.UseShellExecute = false;
        python.EnableRaisingEvents = true;
        python.Exited += new System.EventHandler(OnPythonExit);
        UnityEngine.Debug.Log("Python setup complete");
        python.Start();
        ccompiledString = string.Empty;
    }
    static void OnPythonExit(object sender, System.EventArgs eventArgs)
    {
        UnityEngine.Debug.Log($"Python exited with code {python.ExitCode}");
        EditorManager.pythonExitAction = delegate { pythonExitAction(); };
    }
    static void pythonExitAction()
    {
        if (python.ExitCode != 0)
        {
            ProgramOutput.instance.Write(python.StandardOutput.ReadToEnd() + "\n" + python.StandardError.ReadToEnd());
        }
        else
        {
            var stdout = python.StandardOutput.ReadToEnd();
            UnityEngine.Debug.Log(stdout);
            ProgramOutput.instance.Write(stdout);
            UnityEngine.Debug.Log("Output written");
        }
        UnityEngine.Debug.Log("Disposing of python");
        python.Dispose();
        python = null;
    }
    public static void StartCrossCompile()
    {
        ccompiledString = string.Empty;
        foreach(var v in varNodes)
        {
            AddLine($"{v.name} = None");
        }
        if (entryPoint.nextNode != null)
            entryPoint.nextNode.CrossCompile();
    }
}

class pythonExitEventArgs : System.EventArgs
{
    public Process python;
}