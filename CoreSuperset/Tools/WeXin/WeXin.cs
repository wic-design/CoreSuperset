using System;
using System.Collections.Generic;
using System.Text;

namespace WeXin {
    /// <summary>
    /// 微信公众号的基类
    /// </summary>
    public class mp {

        /// <summary>
        /// appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// appSecret
        /// </summary>
        public string appSecret { get; set; }

        /// <summary>
        /// 全局token
        /// </summary>
        private static string global_token { get; set; }

        /// <summary>
        /// 全局token过期时间
        /// </summary>
        private static DateTime global_token_expires { get; set; }


        /// <summary>
        /// 微信公众号的基类
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appSecret"></param>
        public mp(string appid,string appSecret) {
            this.appid = appid;
            this.appSecret = appSecret;

            

        }

    }
}