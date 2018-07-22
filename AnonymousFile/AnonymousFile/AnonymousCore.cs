// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Anonymous
{
    using System;

    /// <summary>
    /// 提供透明字串與 <see langword="int"/> 型別間的核心轉換函式
    /// </summary>
    public static class AnonymousCore
    { 
        #region Fields

        private const char HIGH_BIT_CHARACTER = (char)0xFFFF;
        private const char LOW__BIT_CHARACTER = (char)0xFFFE;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 透過識別碼轉換為對應之透明字串
        /// </summary>
        /// <param name="id">識別碼</param>
        /// <returns>透明字串</returns>
        public static string Convert(int id)
        {
            var target = new char[32];
            for (int i = 31; i >= 0; i--)
                target[i] = Convert((id & (1 << i)) != 0);

            return new string(target);
        }

        /// <summary>
        /// 從透明字串轉換為對應的識別碼
        /// </summary>
        /// <param name="str">透明字串</param>
        /// <returns>識別碼</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="NotSupportedException"/>
        public static int Convert(string str)
        {
            if (str.Length > 31)
                throw new ArgumentOutOfRangeException("Too many elements to be converted to a single int");

            if (Validate(str))
                throw new NotSupportedException("Find some illegal characters");

            int val = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert(str[i]))
                {
                    val |= 1 << i;
                }
            }
            return val;
        }

        /// <summary>
        /// 驗證字串是否為合法字串
        /// </summary>
        /// <param name="str">待驗證字串</param>
        /// <returns>如果為合法字串則為 <see langword="true"/> ， 反之則為 <see langword="false"/></returns>
        public static bool Validate(string str)
        {
            if (str is null)
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != HIGH_BIT_CHARACTER || str[i] != LOW__BIT_CHARACTER)
                    return false;
            }

            return true;
        }

        private static char Convert(bool b) => b ? HIGH_BIT_CHARACTER : LOW__BIT_CHARACTER;

        private static bool Convert(char c) => c == HIGH_BIT_CHARACTER;

        #endregion Methods
    }
}