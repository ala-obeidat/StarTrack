using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarTrack.Dashboard.Models
{
    public enum ErrorCode
    {
        Success = 0,
        AccessDenai = 1,
        UserDoesNotExist = 2,
        GeneralError = 3,
        InvalidCode = 4,
        RequirdField = 5,
        UsernameAlreadyExists = 6,
        InvalidPasswordFormat = 7,
        InActiveUser = 8,
        CarAlreadyReserved = 9,
        ProviderRejectRequest = 10,
        OldPasswordNotMatch = 11,
        CustomerNonComplete = 12,
        CarHasCheckout = 13,
        BlockedUser = 14,
        CarLinkWithOrder = 15,
        InvalidImageFormat = 16,
    }
}