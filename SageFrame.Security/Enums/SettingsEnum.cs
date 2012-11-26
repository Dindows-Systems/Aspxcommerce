using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Security.Entities
{
    public enum SettingsEnum
    {
        DUPLICATE_USERS_ACROSS_PORTALS,
        DUPLICATE_ROLES_ACROSS_PORTALS,
        DEFAULT_PASSWORD_FORMAT=1,
        SELECTED_PASSWORD_FORMAT,
        DUPLICATE_EMAIL_ALLOWED,
        ENABLE_CAPTCHA,
        DEFAULT_CAPTCHA_STATUS=0

    }
}
