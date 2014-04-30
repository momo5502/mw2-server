using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MW2_Server
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
       {
           AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
           {
               var assemblyName = args.Name.Split(',').First();

               if (assemblyName == "Mono.Nat")
               {
                   return Assembly.Load(Properties.Resources.Mono_Nat);
               }

               return null;
           };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
