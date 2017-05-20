using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09.ArithmeticalЕxpressions
{
    static class TokenType
    {
        //field
        private static string noneToken;
        private static string digit;
        private static string arithmeticOperator;
        private static string parenthesesAndComma;
        private static string mathematicalFunctions;

        //property
        public static string NoneToken
        {
            get
            {
                noneToken = "noneToken";
                return noneToken;
            }
        }
        public static string Digit
        {
            get
            {
                digit = "digit";
                return digit;
            }
        }
        public static string ArithmeticOperator
        {
            get
            {
                arithmeticOperator = "arithmeticOperator";
                return arithmeticOperator;
            }
        }
        public static string ParenthesesAndComma
        {
            get
            {
                parenthesesAndComma = "parenthesesAndComma";
                return parenthesesAndComma;
            }
        }
        public static string MathematicalFunctions
        {
            get
            {
                mathematicalFunctions = "mathematicalFunctions";
                return mathematicalFunctions;
            }
        }
    }
}
