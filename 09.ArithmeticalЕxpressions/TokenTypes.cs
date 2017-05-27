using System;
using System.Collections.Generic;
using System.Text;

namespace _09.ArithmeticalЕxpressions
{
    public static class TokenType
    {
        //fields
        private static string noneToken = "noneToken";
        private static string digit = "digit";
        private static string arithmeticOperator = "arithmeticOperator";
        private static string parenthesesAndComma = "parenthesesAndComma";
        private static string mathematicalFunctions = "mathematicalFunctions";

        //properties
        public static string NoneToken
        {
            get
            {
                return noneToken;
            }
        }
        public static string Digit
        {
            get
            {
                return digit;
            }
        }
        public static string ArithmeticOperator
        {
            get
            {
                return arithmeticOperator;
            }
        }
        public static string ParenthesesAndComma
        {
            get
            {
                return parenthesesAndComma;
            }
        }
        public static string MathematicalFunctions
        {
            get
            {
                return mathematicalFunctions;
            }
        }
    }
}
