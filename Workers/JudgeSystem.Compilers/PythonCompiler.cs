using JudgeSystem.Common;
using JudgeSystem.Workers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;



namespace JudgeSystem.Compilers
{
    internal class PythonCompiler : ICompiler
    {
        public CompileResult Compile(string fileName, string workingDirectory, IEnumerable<string> sources = null)
        {
            // Since Python is an interpreted language, 
            // we don't technically "compile" python code the way we do with languages like C++. 
            // Instead, the python interpreter runs the script.
            // Here, we'll use the python command to run the script.

            string scriptFile = $"{workingDirectory}{fileName}{GlobalConstants.PythonFileExtension}";
            string processFileName = "python";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                scriptFile += CompilationSettings.PyFileExtension;
                processFileName = $"{GlobalConstants.ConsoleComamndPrefix}{CompilationSettings.SetPythonInterpreterPathCommand} & python";
            }

            var compiler = new Compiler();
            CompileResult result = compiler.Compile(scriptFile, processFileName);
            result.OutputFilePath = scriptFile;
            return result;
        }
    }
}
