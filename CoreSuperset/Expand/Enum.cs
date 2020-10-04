using System;
using System.ComponentModel;

namespace DCSP {
    /// <summary>
    /// Enum枚举类型扩展
    /// </summary>
    public static class EnumHelper {

        /// <summary>
        /// 获取enum的描述属性[Description]的值值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value) {
            if (value == null)
                return "";

            System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
                return value.ToString();
            else
                return (attribArray[0] as DescriptionAttribute).Description;
        }
    }

}