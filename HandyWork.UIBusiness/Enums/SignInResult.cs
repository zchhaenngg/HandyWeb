using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Enums
{
    public enum SignInResult
    {
        // Summary:
        //     Sign in was successful
        Success = 1,
        /// <summary>
        /// 账号被锁定*一天内尝试n次登录都失败了
        /// </summary>
        LockedOut = 2,
        /// <summary>
        /// 用户名不正确
        /// </summary>
        UserNameError = 3,
        /// <summary>
        /// 密码不正确
        /// </summary>
        PasswordError = 4,
        /// <summary>
        /// 账号已禁用
        /// </summary>
        Invalid = 5,
        /// <summary>
        ///密码过期
        /// </summary>
        SuccessRehashNeeded = 6
    }
}
