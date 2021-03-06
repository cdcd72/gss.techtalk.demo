namespace JwtAuth.Demo.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 使用者是否存在
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <returns></returns>
        bool IsExistUser(string userName);

        /// <summary>
        /// 是否為有效使用者
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        bool IsValidUser(string userName, string password);

        /// <summary>
        /// 取得使用者角色
        /// </summary>
        /// <param name="userName">使用者名稱</param>
        /// <returns></returns>
        string GetUserRole(string userName);
    }
}
