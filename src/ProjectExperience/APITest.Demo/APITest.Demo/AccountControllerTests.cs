using JwtAuth.Demo.Dto;
using NUnit.Framework;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace APITest.Demo
{
    public class AccountControllerTests : TestBase
    {
        [SetUp]
        public void Setup() =>
            // 確保各測試案例之間認證機制彼此獨立
            Client.DefaultRequestHeaders.Authorization = null;

        #region LoginTests

        /// <summary>
        /// 登入失敗(必填未填)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LoginFailedWithNotInputRequiredInfoTestAsync()
        {
            var loginRequest = new LoginRequest();

            // Target
            var response = await PostAsync("api/account/login", loginRequest);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 登入失敗(無效使用者)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LoginFailedWithInvalidUserTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "impuisible"
            };

            // Target
            var response = await PostAsync("api/account/login", loginRequest);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 登入成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LoginSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "puipui"
            };

            // Target
            var result = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            Assert.IsTrue(result != null && loginRequest.UserName == result.UserName);
        }

        #endregion

        #region LogoutTests

        /// <summary>
        /// 登出失敗
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LogoutFailedTestAsync()
        {
            // Target
            var response = await GetAsync("api/account/logout");

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// 登出成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LogoutSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            // Target
            var response = await GetAsync("api/account/logout", loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        #endregion

        #region RefreshTokenTests

        /// <summary>
        /// 刷新權杖失敗(必填未填)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task RefreshTokenFailedWithNotInputRequiredInfoTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var refreshTokenRequest = new RefreshTokenRequest();

            // Target
            var response = await PostAsync("api/account/refreshtoken", refreshTokenRequest, loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 刷新權杖成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task RefreshTokenSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var refreshTokenRequest = new RefreshTokenRequest()
            {
                RefreshToken = loginResult.RefreshToken
            };

            // 睡一秒，避免因為時間關係，產生相同存取權杖...
            Thread.Sleep(1000);

            // Target
            var result = await PostResultAsync<LoginResult>("api/account/refreshtoken", refreshTokenRequest, loginResult.AccessToken);

            Assert.IsTrue(result != null &&
                          loginResult.AccessToken != result.AccessToken &&
                          loginResult.RefreshToken != result.RefreshToken);
        }

        #endregion

        #region GetCurrentUserTests

        /// <summary>
        /// 取得使用者資訊成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetCurrentUserSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil",
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            // Target
            var result = await GetResultAsync<LoginResult>("api/account/user", loginResult.AccessToken);

            Assert.IsTrue(result != null && loginRequest.UserName == result.UserName);
        }

        #endregion
    }
}
