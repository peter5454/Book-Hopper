using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Book_Hopper.Helpers
{
    public static class SecureStringHelper
    {
        public static bool AreSecureStringsEqual(SecureString ss1, SecureString ss2)
        {
            if (ss1 == null || ss2 == null)
                return false;

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);

                return Marshal.PtrToStringBSTR(bstr1) == Marshal.PtrToStringBSTR(bstr2);
            }
            finally
            {
                if (bstr1 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr1);

                if (bstr2 != IntPtr.Zero)
                    Marshal.ZeroFreeBSTR(bstr2);
            }
        }
        public static SecureString ConvertToSecureString(string plainText)
        {
            if (plainText == null)
                throw new ArgumentNullException(nameof(plainText));

            var secureString = new SecureString();

            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }

            return secureString;
        }
        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

    }

}