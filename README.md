# mem.dll
A simple and easy to use .NET memory editing class for hacking games and apps.

Example code:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mem;
namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Module m = new Module();

            System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcesses();
            
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            foreach (System.Diagnostics.Process pp in p) {
                if (pp.ProcessName == "ac_client") {
                    proc = pp;
                }
                
            }
            
            m.init(proc);
            int[] offsets = {0x140};
            while (true) {
                m.write(m.getadr(proc.MainModule.BaseAddress + 0x0017E0A8, offsets), "int", "5000000");
                System.Threading.Thread.Sleep(1000);
            }
            
        }
    }
}
 
```


Go to the wiki for more information about functions.
