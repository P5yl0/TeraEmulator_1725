using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Data.Interfaces;
using Microsoft.CSharp;
using Utils;

namespace Tera.AdminEngine.AdminCommands
{
    class Script : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            string[] args = msg.Split(' ');

            switch (args[0])
            {
                case "run":
                    string dir = Directory.GetCurrentDirectory() + @"\scripts\" + args[1] + ".cs";
                    var script = new Tera.AdminEngine.Script();
                    CompilerResults results =
                        new CSharpCodeProvider(new Dictionary<string, string> {{"CompilerVersion", "v4.0"}}).
                            CompileAssemblyFromSource(new CompilerParameters(
                                                          Array.ConvertAll(AppDomain.CurrentDomain.GetAssemblies(),
                                                                           x => x.Location)) {GenerateInMemory = true},
                                                      File.ReadAllText(dir));
                    try
                    {
                        script = (Tera.AdminEngine.Script)Activator.CreateInstanceFrom(Assembly.GetEntryAssembly().CodeBase,
                                                                       "Tera.AdminEngine." + args[1].Replace(".cs", ""))
                                              .Unwrap();
                    }
                    catch
                    {
                        if (results.Errors.Count > 0)
                        {
                            foreach (CompilerError err in results.Errors)
                                Log.Error("Script compile {0} error: {1}", args[1].Replace(".cs", ""), err.ErrorText);

                            Log.Info("Script {0} not exists!", args[1].Replace(".cs", ""));
                        }
                        else
                        {
                            script =
                                results.CompiledAssembly.CreateInstance("Tera.AdminEngine." + args[1].Replace(".cs", ""))
                                as
                                Tera.AdminEngine.Script;
                        }
                    }

                    if (script != null)
                    {
                        script.Connection = connection;
                        script.Run(args[1]);
                    }

                    break;
            }
        }
    }
}
