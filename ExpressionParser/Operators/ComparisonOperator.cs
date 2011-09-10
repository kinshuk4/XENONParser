using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Operations;
using ExpressionParser.Design;

namespace ExpressionParser.Operators
{
    /// <summary>
    /// Comparison Operator Class providing evaluation services for "==", "!=","<", "<=", ">", ">=" operators.
    /// </summary>
    public class ComparisonOperator : Operator
    {
        public ComparisonOperator(char cOperator)
            : base(cOperator)
        {
        }
        public ComparisonOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        //{"==", "!=","<", "<=", ">", ">="}
        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is IComparisonOperations))
                throw new RPN_Exception("Argument invalid in ComparisonOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "==":
                    return ((IComparisonOperations)lhs).EqualTo(rhs);
                case "!=":
                    return ((IComparisonOperations)lhs).NotEqualTo(rhs);
                case "<":
                    return ((IComparisonOperations)lhs).LessThan(rhs);
                case "<=":
                    return ((IComparisonOperations)lhs).LessThanOrEqualTo(rhs);
                case ">":
                    return ((IComparisonOperations)lhs).GreaterThan(rhs);
                case ">=":
                    return ((IComparisonOperations)lhs).GreaterThanOrEqualTo(rhs);
            }
            throw new RPN_Exception("Unsupported Comparison operation " + m_szOperator);
        }
    }
	
}
