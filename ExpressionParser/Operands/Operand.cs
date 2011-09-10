using System;
using System.Collections.Generic;
using System.Text;
using ExpressionParser.Design;

namespace ExpressionParser.Operands
{
    /// <summary>
    /// Base class for all Operands.  Provides datastorage
    /// </summary>
    public abstract class Operand : IOperand
    {
        public Operand(string szVarName, object varValue)
        {
            m_szVarName = szVarName;
            m_VarValue = varValue;
        }
        public Operand(string szVarName)
        {
            m_szVarName = szVarName;
        }
        public override string ToString()
        {
            return m_szVarName;
        }
        public abstract void ExtractAndSetValue(string szValue, bool bFormula);
        public string Name
        {
            get
            {
                return m_szVarName;
            }
            set
            {
                m_szVarName = value;
            }
        }
        public object Value
        {
            get
            {
                return m_VarValue;
            }
            set
            {
                m_VarValue = value;
            }
        }
        protected string m_szVarName = "";
        protected object m_VarValue = null;
    }
}
