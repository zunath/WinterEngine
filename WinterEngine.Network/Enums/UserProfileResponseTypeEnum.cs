using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WinterEngine.Network.Enums
{
    public enum UserProfileResponseTypeEnum
    {
        [Description("Successful")]
        Successful = 1,
        [Description("Error: Username already exists.")]
        UsernameAlreadyExists = 2,
        [Description("Error: Invalid password.")]
        InvalidPassword = 3,
        [Description("Unknown error. Please try again.")]
        Failure = 4,
        [Description("Error: Passwords don't match.")]
        PasswordMismatch = 5,
        [Description("Error: Account does not exist.")]
        AccountNotExist = 6,
        [Description("Error: An account is already attached to this email address.")]
        AccountForEmailAlreadyExists = 7,
        [Description("Account has not been activated.")]
        AccountNotActivated = 8
    }
}
