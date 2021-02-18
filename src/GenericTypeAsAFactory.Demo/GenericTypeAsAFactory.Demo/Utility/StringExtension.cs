﻿using System.Web;

namespace GenericTypeAsAFactory.Demo.Utility
{
    public static class StringExtension
    {
        /// <summary>
        /// HTML 編碼
        /// </summary>
        /// <param name="value">數值</param>
        /// <returns></returns>
        public static string HtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        /// <summary>
        /// HTML 解碼
        /// </summary>
        /// <param name="value">數值</param>
        /// <returns></returns>
        public static string HtmlDecode(this string value)
        {
            return HttpUtility.HtmlDecode(value);
        }
    }
}
