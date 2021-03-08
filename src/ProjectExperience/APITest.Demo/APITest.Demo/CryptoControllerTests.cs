using JwtAuth.Demo.Dto;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace APITest.Demo
{
    public class CryptoControllerTests : TestBase
    {
        [SetUp]
        public void Setup() =>
            // 確保各測試案例之間認證機制彼此獨立
            Client.DefaultRequestHeaders.Authorization = null;

        #region EncryptTests

        /// <summary>
        /// 加密失敗(使用者權限無效)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task EncryptFailedWithInvalidRoleTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil", // Role = BasicUser
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var encryptRequest = new EncryptRequest()
            {
                Text = "xxx123"
            };

            // Target
            var response = await PostAsync("api/crypto/encrypt", encryptRequest, loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// 加密失敗(必填未填)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task EncryptFailedWithNotInputRequiredInfoTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "admin", // Role = Admin
                Password = "securePa55"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var encryptRequest = new EncryptRequest();

            // Target
            var response = await PostAsync($"api/crypto/encrypt", encryptRequest, loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 加密成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task EncryptSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "admin", // Role = Admin
                Password = "securePa55"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var text = "xxx123";

            var encryptRequest = new EncryptRequest()
            {
                Text = text
            };

            // Target
            var encryptResult = await PostResultAsync<EncryptResult>($"api/crypto/encrypt", encryptRequest, loginResult.AccessToken);

            Assert.AreNotEqual(text, encryptResult.Text);
        }

        #endregion

        #region DecryptTests

        /// <summary>
        /// 解密失敗(使用者權限無效)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DecryptFailedWithInvalidRoleTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "neil", // Role = BasicUser
                Password = "puipui"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var decryptRequest = new DecryptRequest()
            {
                Text = "CfDJ8D0PAJDD5E1LjqSV-gIsHN9YMz63wRlnYk_r_xqrXnJvnURHi_7utH0hL2PvfCy0hpdkBtPyIACTQm0Lpb8gh2vHjrGIDsNOyfrBQyV9bB2IrIq8Ot7StShBzPijb8edrw"
            };

            // Target
            var response = await PostAsync("api/crypto/decrypt", decryptRequest, loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// 解密失敗(必填未填)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DecryptFailedWithNotInputRequiredInfoTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "admin", // Role = Admin
                Password = "securePa55"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var decryptRequest = new DecryptRequest();

            // Target
            var response = await PostAsync($"api/crypto/decrypt", decryptRequest, loginResult.AccessToken);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// 解密成功
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DecryptSuccessTestAsync()
        {
            var loginRequest = new LoginRequest()
            {
                UserName = "admin", // Role = Admin
                Password = "securePa55"
            };

            var loginResult = await PostResultAsync<LoginResult>("api/account/login", loginRequest);

            var text = "xxx123";

            var encryptRequest = new EncryptRequest()
            {
                Text = text
            };

            var encryptResult = await PostResultAsync<EncryptResult>($"api/crypto/encrypt", encryptRequest, loginResult.AccessToken);

            var decryptRequest = new DecryptRequest()
            {
                Text = encryptResult.Text
            };

            // Target
            var decryptResult = await PostResultAsync<DecryptResult>($"api/crypto/decrypt", decryptRequest, loginResult.AccessToken);

            Assert.AreEqual(text, decryptResult.Text);
        }

        #endregion
    }
}
