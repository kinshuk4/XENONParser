using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Operations;
using ExpressionParser.Design;

namespace ExpressionParser.Operators
{
    /// <summary>
    /// Logical Operator Class providing evaluation services for && and || operators.
    /// </summary>
    public class LogicalOperator : Operator
    {
        public LogicalOperator(char cOperator)
            : base(cOperator)
        {
        }
        public LogicalOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        //{"&&", "||"}
        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is ILogicalOperations))
                throw new RPN_Exception("Argument invalid in LogicalOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "&&":
                    return ((ILogicalOperations)lhs).AND(rhs);
                case "||":
                    return ((ILogicalOperations)lhs).OR(rhs);
            }
            throw new RPN_Exception("Unsupported Logical operation " + m_szOperator);
        }
    }

}
