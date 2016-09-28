using HandyWork.Common.Authority;
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
        public static Error InvalidUserName => new Error("User", ValidatorResource.InvalidUserName);
        public static Error DuplicateUserName => new Error("User", ValidatorResource.DuplicateUserName);
        public static Error InvalidPassword => new Error("User", ValidatorResource.InvalidPassword);
        public static Error InvalidRole => new Error("Role", ValidatorResource.InvalidRole);
        public static Error DuplicateRole => new Error("Role", ValidatorResource.DuplicateRole);
        public static Error InvalidPermission => new Error("Permission", ValidatorResource.InvalidPermission);
        public static Error DuplicatePermission => new Error("Permission", ValidatorResource.DuplicatePermission);
    }
}
