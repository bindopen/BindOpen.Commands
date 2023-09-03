using BindOpen.System.Data.Helpers;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;

namespace BindOpen.Labs.Commands.Tests
{
    public static class SystemData
    {
        static IBdoScope _appScope = null;

        /// <summary>
        /// The global scope.
        /// </summary>
        public static IBdoScope Scope
        {
            get
            {
                if (_appScope == null)
                {
                    _appScope = BdoScoping.NewScope();
                    _appScope.LoadExtensions(q => q.AddAssemblyFrom<OptionFake>());
                }

                return _appScope;
            }
        }

        static string _workingFolder;
        static IBdoScriptInterpreter _scriptInterpreter;

        /// <summary>
        /// The global working folder.
        /// </summary>
        public static string WorkingFolder
        {
            get
            {
                if (_workingFolder == null)
                {
                    _workingFolder = (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();
                }

                return _workingFolder;
            }
        }

        public static IBdoScriptInterpreter ScriptInterpreter
        {
            get
            {
                if (_scriptInterpreter == null)
                {
                    _scriptInterpreter = Scope.Interpreter; ;
                }

                return _scriptInterpreter;
            }
        }
    }
}