using System.Collections.Generic;
using JwtAuth.Demo.Model;

namespace JwtAuth.Demo.Services
{
    public class UserService : IUserService
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "neil", "puipui" },
            { "admin", "securePa55" }
        };

        /// <summary>
        /// 使用者是否存在
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <returns></returns>
        public bool IsExistUser(string userName) => users.ContainsKey(userName);

        /// <summary>
        /// 是否為有效使用者
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public bool IsValidUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            return users.TryGetValue(userName, out var p) && p == password;
        }

        /// <summary>
        /// 取得使用者角色
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <returns></returns>
        public string GetUserRole(string userName)
        {
            if (!IsExistUser(userName))
                return string.Empty;

            if (userName == "admin")
                return UserRoles.Admin;

            return UserRoles.BasicUser;
        }
    }
}
