using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    /// <summary>
    /// DateTime时间类型扩展
    /// </summary>
    public static class DateTimeHelper {

        /// <summary>
        /// 获取与js一致的时间戳
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetTime(this DateTime time) {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            if (time < start) throw new Exception("时间不能小于1970年1月1日");
            TimeSpan ts = time - start;
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

    }
