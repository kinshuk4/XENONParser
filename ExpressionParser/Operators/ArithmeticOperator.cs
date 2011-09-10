using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;
using ExpressionParser.Operations;

namespace ExpressionParser.Operators
{
    /// <summary>
    /// Arithmetic Operator Class providing evaluation services for "+-/*%" operators.
    /// </summary>
    public class ArithmeticOperator : Operator
    {
        public ArithmeticOperator(char cOperator)
            : base(cOperator)
        {
        }
        public ArithmeticOperator(string szOperator)
            : base(szOperator)
        {
        }
        //bool bBinaryOperator = true;

        public override IOperand Eval(IOperand lhs, IOperand rhs)
        {
            if (!(lhs is IArithmeticOperations))
                throw new RPN_Exception("Argument invalid in ArithmeticOperator.Eval - Invalid Expression : lhs");
            switch (m_szOperator)
            {
                case "+":
                    return ((IArithmeticOperations)lhs).Plus(rhs);
                case "-":
                    return ((IArithmeticOperations)lhs).Minus(rhs);
                case "*":
                    return ((IArithmeticOperations)lhs).Multiply(rhs);
                case "/":
                    return ((IArithmeticOperations)lhs).Divide(rhs);
                case "%":
                    return ((IArithmeticOperations)lhs).Modulo(rhs);
            }
            throw new RPN_Exception("Unsupported Arithmetic operation " + m_szOperator);
        }
    }

}
