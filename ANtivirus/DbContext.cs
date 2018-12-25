using System;
using System.Collections.Generic;
using System.Text;

namespace ANtivirus
{
   public class DbContext
    {
        public enum MalwareTypes
        {
            Trojan,
            Keylogger,
            Crypter
        }
       
        public static Dictionary<string, MalwareTypes> signatures = new Dictionary<string, MalwareTypes>
        {
            {"CreateRemoteThread",MalwareTypes.Trojan},
            {"NtUnmapViewOfSection",MalwareTypes.Trojan},
            {"GetAsyncKeyState",MalwareTypes.Trojan},
            {"JOIN",MalwareTypes.Trojan},
            {"PRIVMSG",MalwareTypes.Trojan},
            {"GetWindowText",MalwareTypes.Keylogger},
            {"SetWindowsHookEx",MalwareTypes.Keylogger},
            {"GeForegroundwindow",MalwareTypes.Keylogger},
            {"RijndaelManaged",MalwareTypes.Crypter},
            {"MD5CryptoServiceProvider",MalwareTypes.Crypter}
        };

        public static Dictionary<string, MalwareTypes> GetAll()
        {
            return signatures;
        }
    }
}
