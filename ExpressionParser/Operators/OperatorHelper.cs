using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operators
{
    public class OperatorHelper
    {
        /// <summary>
        /// Factory method to create Operator objects.
        /// </summary>
        /// <param name="szOperator"></param>
        /// <returns></returns>
        static public IOperator CreateOperator(string szOperator)
        {
            IOperator oprtr = null;
            if (OperatorHelper.IsArithmeticOperator(szOperator))
            {
                oprtr = new ArithmeticOperator(szOperator);
                return oprtr;
            }
            if (OperatorHelper.IsComparisonOperator(szOperator))
            {
                oprtr = new ComparisonOperator(szOperator);
                return oprtr;
            }
            if (OperatorHelper.IsLogicalOperator(szOperator))
            {
                oprtr = new LogicalOperator(szOperator);
                return oprtr;
            }
            throw new RPN_Exception("Unhandled Operator : " + szOperator);
        }
        static public IOperator CreateOperator(char cOperator)
        {
            return CreateOperator(new string(cOperator, 1));
        }
        /// Some helper functions.
        public static bool IsOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllOps, currentOp.Trim());
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsArithmeticOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllArithmeticOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsComparisonOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllComparisonOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }
        public static bool IsLogicalOperator(string currentOp)
        {
            int nPos = Array.IndexOf(m_AllLogicalOps, currentOp);
            if (nPos != -1)
                return true;
            else
                return false;
        }
        #region Precedence
        /// Precedence is determined by relative indices of the operators defined in 
        /// in m_AllOps variable

        /// <summary>
        /// Summary of IsLowerPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsLowerPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx < nPrevIdx)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summary of IsHigherPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsHigherPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx > nPrevIdx)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Summary of IsEqualPrecOperator.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// 		
        public static bool IsEqualPrecOperator(string currentOp, string prevOp)
        {
            int nCurrIdx;
            int nPrevIdx;
            GetCurrentAndPreviousIndex(m_AllOps, currentOp, prevOp, out nCurrIdx, out nPrevIdx);
            if (nCurrIdx == nPrevIdx)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Summary of GetCurrentAndPreviousIndex.
        /// </summary>
        /// <param name=allOps></param>
        /// <param name=currentOp></param>
        /// <param name=prevOp></param>
        /// <param name=nCurrIdx></param>
        /// <param name=nPrevIdx></param>
        /// 		
        private static void GetCurrentAndPreviousIndex(string[] allOps, string currentOp, string prevOp,
            out int nCurrIdx, out int nPrevIdx)
        {
            nCurrIdx = -1;
            nPrevIdx = -1;
            for (int nIdx = 0; nIdx < allOps.Length; nIdx++)
            {
                if (allOps[nIdx] == currentOp)
                {
                    nCurrIdx = nIdx;
                }
                if (allOps[nIdx] == prevOp)
                {
                    nPrevIdx = nIdx;
                }
                if (nPrevIdx != -1 && nCurrIdx != -1)
                {
                    break;
                }
            }
            if (nCurrIdx == -1)
            {
                throw new RPN_Exception("Unknown operator - " + currentOp);
            }
            if (nPrevIdx == -1)
            {
                throw new RPN_Exception("Unknown operator - " + prevOp);
            }

        }
        #endregion
        #region RegEx
        /// <summary>
        /// This gets the regular expression used to find operators in the input
        /// expression.
        /// </summary>
        /// <param name="exType"></param>
        /// <returns></returns>
        static public string GetOperatorsRegEx(ExpressionType exType)
        {
            StringBuilder strRegex = new StringBuilder();
            if ((exType & ExpressionType.ET_ARITHMETIC).Equals(ExpressionType.ET_ARITHMETIC))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szArthmtcRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szArthmtcRegEx);
                }
            }
            if ((exType & ExpressionType.ET_COMPARISON).Equals(ExpressionType.ET_COMPARISON))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szCmprsnRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szCmprsnRegEx);
                }
            }
            if ((exType & ExpressionType.ET_LOGICAL).Equals(ExpressionType.ET_LOGICAL))
            {
                if (strRegex.Length == 0)
                {
                    strRegex.Append(m_szLgclRegEx);
                }
                else
                {
                    strRegex.Append("|" + m_szLgclRegEx);
                }
            }
            if (strRegex.Length == 0)
                throw new RPN_Exception("Invalid combination of ExpressionType value");
            return "(" + strRegex.ToString() + ")";
        }
        /// <summary>
        /// Expression to pattern match various operators
        /// </summary>
        static string m_szArthmtcRegEx = @"[+\-*/%()]{1}";
        static string m_szCmprsnRegEx = @"[=<>!]{1,2}";
        static string m_szLgclRegEx = @"[&|]{2}";
        #endregion

        public static string[] AllOperators
        {
            get
            {
                return m_AllOps;
            }
        }

        /// <summary>
        /// All Operators supported by this module currently.
        /// Modify here to add more operators IN ACCORDANCE WITH their precedence.
        /// Additionally add into individual variables to support some helper methods above.
        /// </summary>
        static string[] m_AllOps = { "||", "&&", "|", "^", "&", "==", "!=",
									   "<", "<=", ">", ">=", "+", "-", "*", "/", "%", "(", ")" };
        static string[] m_AllArithmeticOps = { "+", "-", "*", "/", "%" };
        static string[] m_AllComparisonOps = { "==", "!=", "<", "<=", ">", ">=" };
        static string[] m_AllLogicalOps = { "&&", "||" };
    }
}
