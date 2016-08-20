using HandyWork.Common.Model;
using HandyWork.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    public static class Errors
    {
        public static ErrorInfo InvalidUserName => new ErrorInfo("User", ValidatorResource.InvalidUserName);
        public static ErrorInfo DuplicateUserName => new ErrorInfo("User", ValidatorResource.DuplicateUserName);
        public static ErrorInfo InvalidPassword => new ErrorInfo("User", ValidatorResource.InvalidPassword);
        public static ErrorInfo InvalidRole => new ErrorInfo("Role", ValidatorResource.InvalidRole);
        public static ErrorInfo DuplicateRole => new ErrorInfo("Role", ValidatorResource.DuplicateRole);
        public static ErrorInfo InvalidPermission => new ErrorInfo("Permission", ValidatorResource.InvalidPermission);
        public static ErrorInfo DuplicatePermission => new ErrorInfo("Permission", ValidatorResource.DuplicatePermission);
    }
}
