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
        public static ErrorInfo InvalidUserName
        {
            get
            {
                return new ErrorInfo("User", ValidatorResource.InvalidUserName);
            }
        }

        public static ErrorInfo DuplicateUserName
        {
            get
            {
                return new ErrorInfo("User", ValidatorResource.DuplicateUserName);
            }
        }
        public static ErrorInfo InvalidPassword
        {
            get
            {
                return new ErrorInfo("User", ValidatorResource.InvalidPassword);
            }
        }

        public static ErrorInfo InvalidRole
        {
            get
            {
                return new ErrorInfo("Role", ValidatorResource.InvalidRole);
            }
        }
        public static ErrorInfo DuplicateRole
        {
            get
            {
                return new ErrorInfo("Role", ValidatorResource.DuplicateRole);
            }
        }

        public static ErrorInfo InvalidPermission
        {
            get
            {
                return new ErrorInfo("Permission", ValidatorResource.InvalidPermission);
            }
        }
        public static ErrorInfo DuplicatePermission
        {
            get
            {
                return new ErrorInfo("Permission", ValidatorResource.DuplicatePermission);
            }
        }
    }
}
