using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockMarketAnalysis
{
    class StrategyHandler
    {
        //to make a new c# script where the user can define their own functions or plots...
        public static void newStrategy()
        {
            //opening up a save file dialog, 
            //here the user will make their file and the program will auto generate some code 
            //and attempt to open it in a txt editor
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "C# Files (*.cs)|*.cs";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\Strategies";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(dialog.OpenFile()))
                    {
                        string className = dialog.FileName.Split('\\').Last();
                        className = className.Replace(".cs", "");
                        //some auto generated code
                        sw.WriteLine(
                            "using StockMarketAnalysis;\n" +
                            "using System.Collections.Generic;\n" +
                            "using System;\n" +
                            "\n" +
                            "namespace Strategy\n" +
                            "{\n" +
                            "    class " + className + "\n" +
                            "    {\n" +
                            "        //this function will excecuted when the \"Execute Strategy\" button is pressed \n" +
                            "        public static void Main()\n" +
                            "        {\n" +
                            "            //its a good idea to update the data so that the Strategies.Data is reflecting what is being shown on the graph\n" +
                            "            Strategies.updateData();\n" +
                            "        \n" +
                            "        \n" +
                            "        }\n" +
                            "    }\n" +
                            "}"
                            );

                        sw.Flush();
                        sw.Close();
                    }

                    //then open the file in vs code (preferably)
                    try
                    {
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("code", "-g " + dialog.FileName);
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                        //if the user doesnt have vs code, then prompt them to download it, then open it with a default program
                        if (MessageBox.Show("Visual Studio Code is recommened... visit download site?", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start("https://code.visualstudio.com/download");
                        }
                        Process.Start(dialog.FileName);
                    }
                }
            }
        }

        //to edit an existing c# script
        public static void openStrategy()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "C# files (*.cs)|*.cs";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\Strategies";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Process process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo("code", "-g " + dialog.FileName);
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                        //if the user doesnt have vs code, then prompt them to download it, then open it with a default program
                        if (MessageBox.Show("Visual Studio Code is recommened, download?", "Visit", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                        {
                            Process.Start("https://code.visualstudio.com/download");
                        }
                        Process.Start(dialog.FileName);
                    }

                }
            }
        }

        //to compile and run the user made code
        public static void executeStrategy()
        {
            //getting the code to execute
            string codeToExecute = "";
            string className = "";
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "C# files (*.cs)|*.cs";
                dialog.FilterIndex = 2;
                dialog.InitialDirectory = @"C:\Users\Public\Documents\Strategies";
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    className = dialog.FileName.Split('\\').Last();
                    className = className.Replace(".cs", "");
                    using (StreamReader sr = new StreamReader(dialog.OpenFile()))
                    {
                        codeToExecute = sr.ReadToEnd();
                    }
                }
            }

            //actually trying to execute the code
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            // Reference to external libraries
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("StockMarketAnalysis.exe");

            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            parameters.GenerateExecutable = false;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, codeToExecute);
            //errors
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.Append("Line " + error.Line + ": ");
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                    
                }

                MessageBox.Show("Errors found:\n" + sb.ToString());
                return;
            }

            Assembly assembly = results.CompiledAssembly;
            Type program = assembly.GetType("Strategy." + className);
            MethodInfo main = program.GetMethod("Main");
            
            main.Invoke(null, null);
            return;
        }
    }
}
