using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Helpers
{
    public static class SecureStringHelpers
    {

        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null) return string.Empty;

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                if (Marshal.PtrToStringUni(unmanagedString) is not string getString)
                    return string.Empty;

                return getString;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }

        }

    }
}
