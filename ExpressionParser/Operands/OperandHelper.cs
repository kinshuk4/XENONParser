using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser.Operands
{
    public class OperandHelper
    {
        /// <summary>
        /// Factory method to create corresponding Operands.
        /// Extended this method to create newer datatypes.
        /// </summary>
        /// <param name="szVarName"></param>
        /// <param name="varType"></param>
        /// <param name="varValue"></param>
        /// <returns></returns>
        static public Operand CreateOperand(string szVarName, Type varType, object varValue)
        {
            Operand oprResult = null;
            switch (varType.ToString())
            {
                case "System.Int32":
                case "System.Int64":
                    oprResult = new LongOperand(szVarName, varValue);
                    return oprResult;
                //case System.Decimal:
                //case System.Single:
                //	oprResult = new DecimalOperand( szVarName, varValue );
                //	return oprResult;
                //	break;
            }
            throw new RPN_Exception("Unhandled type : " + varType.ToString());
        }
        static public Operand CreateOperand(string szVarName, Type varType)
        {
            return OperandHelper.CreateOperand(szVarName, varType, null);
        }
    }
	
}
