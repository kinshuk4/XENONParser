using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;
using ExpressionParser.Operations;

namespace ExpressionParser.Operands
{
    /// <summary>
    /// Operand corresponding to the Long (Int32/Int64) datatypes.
    /// </summary>
    public class LongOperand : Operand, IArithmeticOperations, IComparisonOperations
    {
        public LongOperand(string szVarName, object varValue)
            : base(szVarName, varValue)
        {
        }
        public LongOperand(string szVarName)
            : base(szVarName)
        {
        }
        public override string ToString()
        {
            return m_szVarName;
        }
        public override void ExtractAndSetValue(string szValue, bool bFormula)
        {
            m_VarValue = !bFormula ? Convert.ToInt64(szValue) : 0;
        }
        /// IArithmeticOperations methods.  Return of these methods is again a LongOperand
        public IOperand Plus(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Plus : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value + (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Minus(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Minus : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value - (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Multiply(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new ArgumentException("Argument invalid in LongOperand.Multiply : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value * (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Divide(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Divide : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value / (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand Modulo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.Modulo : rhs");
            LongOperand oprResult = new LongOperand("Result", Type.GetType("System.Int64"));
            oprResult.Value = (long)this.Value % (long)((Operand)rhs).Value;
            return oprResult;
        }

        /// IComparisonOperators methods.  Return values are always BooleanOperands type
        public IOperand EqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.== : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = (long)this.Value == (long)((Operand)rhs).Value;
            return oprResult;
        }
        public IOperand NotEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.!= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value != (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThan(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.< : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value < (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand LessThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.<= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value <= (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThan(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.> : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value > (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand GreaterThanOrEqualTo(IOperand rhs)
        {
            if (!(rhs is LongOperand))
                throw new RPN_Exception("Argument invalid in LongOperand.>= : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((long)this.Value >= (long)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
    }
}
