using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace mem
{

    public class Module
    {
        [DllImport("Kernel32.dll")]
        static extern IntPtr OpenProcess(
        uint dwDesiredAccess,
        bool bInheritHandle,
        int dwProcessId);

        [DllImport("Kernel32.dll")]
        static extern bool ReadProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        out int lpNumberOfBytesRead);

        [DllImport("Kernel32.dll")]
        static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        byte[] lpBuffer,
        int nSize,
        out int lpNumberOfBytesRead);
         static IntPtr FindDMAAddy(IntPtr hProc, IntPtr ptr, int[] offsets)
        {
            var buffer = new byte[IntPtr.Size];
            foreach (int i in offsets)
            {
                ReadProcessMemory(hProc, ptr, buffer, buffer.Length, out var read);

                ptr = (IntPtr.Size == 4)
                ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(buffer, 0)), i)
                : ptr = IntPtr.Add(new IntPtr(BitConverter.ToInt64(buffer, 0)), i);
            }
            return ptr;
        }

        Process p;
        public void init(Process process)
        {
            p = process;
            OpenProcess(0xFFFF, false, process.Id);
            

            
        }
        public bool write(IntPtr adress,string type, string value) {
            byte[] buffer = { };
            if (type.ToLower() == "int")
            {
                buffer = BitConverter.GetBytes(int.Parse(value));
            }
            else if (type.ToLower() == "float") {
                buffer = BitConverter.GetBytes(float.Parse(value));
            }
            else if (type.ToLower() == "double")
            {
                buffer = BitConverter.GetBytes(double.Parse(value));
            }
            else if (type.ToLower() == "string")
            {
                buffer = Encoding.UTF8.GetBytes(value);
            }

            int read = 0;
            bool x = WriteProcessMemory(p.Handle, adress,buffer,buffer.Length,out read);
            return x;
        }
        public IntPtr getadr(IntPtr adress, int[] offsets) {



            return FindDMAAddy(p.Handle, adress, offsets);

        }
        
    }
}
