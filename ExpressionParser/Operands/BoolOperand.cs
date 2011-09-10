using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;
using ExpressionParser.Operations;

namespace ExpressionParser.Operands
{
    /// <summary>
    /// Operand corresponding to Boolean Type
    /// </summary>
    public class BoolOperand : Operand, ILogicalOperations
    {
        public BoolOperand(string szVarName, object varValue)
            : base(szVarName, varValue)
        {
        }
        public BoolOperand(string szVarName)
            : base(szVarName)
        {
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public override void ExtractAndSetValue(string szValue, bool bFormula)
        {
            m_VarValue = !bFormula ? Convert.ToBoolean(szValue) : false;
        }
        public IOperand AND(IOperand rhs)
        {
            if (!(rhs is BoolOperand))
                throw new RPN_Exception("Argument invalid in BoolOperand.&& : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((bool)this.Value && (bool)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
        public IOperand OR(IOperand rhs)
        {
            if (!(rhs is BoolOperand))
                throw new RPN_Exception("Argument invalid in BoolOperand.|| : rhs");
            BoolOperand oprResult = new BoolOperand("Result");
            oprResult.Value = ((bool)this.Value || (bool)((Operand)rhs).Value) ? true : false;
            return oprResult;
        }
    }
	
	
}
