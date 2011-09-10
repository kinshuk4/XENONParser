using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operators
{
    /// <summary>
    /// Base class of all operators.  Provides datastorage
    /// </summary>
    public abstract class Operator : IOperator
    {
        public Operator(char cOperator)
        {
            m_szOperator = new String(cOperator, 1);
        }
        public Operator(string szOperator)
        {
            m_szOperator = szOperator;
        }
        public override string ToString()
        {
            return m_szOperator;
        }
        public abstract IOperand Eval(IOperand lhs, IOperand rhs);
        public string Value
        {
            get
            {
                return m_szOperator;
            }
            set
            {
                m_szOperator = value;
            }
        }
        protected string m_szOperator = "";
    }
}
