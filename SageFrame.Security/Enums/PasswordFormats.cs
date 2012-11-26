using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Security.Enums
{
    public enum PasswordFormats
    {
        CLEAR=1,
        ONE_WAY_HASHED=2,
        ENCRYPTED_AES=3,
        ENCRYPTED_RSA=4
    }
}
