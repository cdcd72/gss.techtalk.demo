namespace JwtAuth.Demo.Security
{
    /// <summary>
    /// 資料保護意圖字串陣列定義
    /// 1. 意圖不一定要用字串陣列表示，但如果是用字串表示，就必需要讓它為唯一且不易被外人猜中(但保護的意圖仍要能很明顯)
    /// 2. 不同的意圖字串或字串陣列創造的 Data Protector 皆各自可獨立加解密，但 A 不能解 B 保護的資料，反之亦然
    /// 3. 意圖字串或字串陣列不可為 null
    /// 4. 意圖字串或字串陣列可以動態傳入，這樣建立出來的 Data Protector 就會各自獨立(ex. 依據每個使用者建立自己的 Data Protector)
    ///    PS. 但千萬注意如果是動態傳入，就要小心有人會偷偷嘗試你的意圖字串或字串陣列...
    /// </summary>
    public static class DataProtectionPurposeStrings
    {
        public static readonly string[] DbPassword = new string[] { "Secret", "DbPassword" };
    }
}
