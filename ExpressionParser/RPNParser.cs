using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using ExpressionParser.Design;
using ExpressionParser.Operations;
using ExpressionParser.Operands;
using ExpressionParser.Operators;

namespace ExpressionParser
{
	#region RPN
	/// <summary>
	/// Summary description for RPNParser.
	/// </summary>
	public class RPNParser
	{
		public RPNParser()
		{
		}
		public object EvaluateExpression( string szExpr, Type varType, bool bFormula, Hashtable htValues )
		{
			ArrayList arrExpr = GetPostFixNotation( szExpr, varType, bFormula );
			return EvaluateRPN(arrExpr, varType, htValues );
		}

		#region RPN_Parser
		/// <summary>
		/// 
		/// 
		/// 1.	Initialize an empty stack (string stack), prepare input infix expression and clear RPN string 
		///	2.	Repeat until we reach end of infix expression 
		///		I.	Get token (operand or operator); skip white spaces 
		///		II.	If token is: 
		///			a.	Left parenthesis: Push it into stack 
		///			b.	Right parenthesis: Keep popping from the stack and appending to 
		///				RPN string until we reach the left parenthesis.
		///				If stack becomes empty and we didn't reach the left parenthesis 
		///				then break out with error "Unbalanced parenthesis" 
		///			c.	Operator: If stack is empty or operator has a higher precedence than 
		///				the top of the stack then push operator into stack. 
		///				Else if operator has lower precedence then we keep popping and 
		///				appending to RPN string, this is repeated until operator in stack 
		///				has lower precedence than the current operator. 
		///			d.	An operand: we simply append it to RPN string. 
		///		III.	When the infix expression is finished, we start popping off the stack and 
		///				appending to RPN string till stack becomes empty. 
		///
		/// </summary>
		/// <param name=szExpr></param>
		/// 		
		public ArrayList GetPostFixNotation( string szExpr, Type varType, bool bFormula )
		{
			Stack stkOp = new Stack();
			ArrayList arrFinalExpr = new ArrayList();
			string szResult = "";

			Tokenizer tknzr = new Tokenizer( szExpr );
			foreach(Token token in tknzr)
			{
				string szToken = token.Value.Trim();
				if( szToken.Length == 0 )
					continue;
				if( !OperatorHelper.IsOperator(szToken) )
				{
					Operand oprnd = OperandHelper.CreateOperand( szToken, varType );
					oprnd.ExtractAndSetValue( szToken, bFormula );
					arrFinalExpr.Add( oprnd );

					szResult += szToken;
					continue;
				}
				string szOp = szToken;
				if( szOp == "(" )
				{
					stkOp.Push( szOp );
				}
				else if( szOp == ")" )
				{
					string szTop;
					while( (szTop = (string)stkOp.Pop()) != "(")
					{
						IOperator oprtr = OperatorHelper.CreateOperator( szTop );
						arrFinalExpr.Add( oprtr );

						szResult += szTop;
						
						if( stkOp.Count == 0 )
							throw new RPN_Exception( "Unmatched braces!" );
					}
				}
				else
				{
					if( stkOp.Count == 0 || (string)stkOp.Peek() == "(" 
						|| OperatorHelper.IsHigherPrecOperator( szOp, (string)stkOp.Peek()) )
					{
						stkOp.Push( szOp );
					}
					else
					{
						while( stkOp.Count != 0 )
						{
							if( OperatorHelper.IsLowerPrecOperator( szOp, (string)stkOp.Peek()) 
								|| OperatorHelper.IsEqualPrecOperator( szOp, (string)stkOp.Peek()) )
							{
								string szTop = (string)stkOp.Peek();
								if( szTop == "(" )
									break;
								szTop = (string)stkOp.Pop();
								
								IOperator oprtr = OperatorHelper.CreateOperator( szTop );
								arrFinalExpr.Add( oprtr );

								szResult += szTop;
							}
							else
								break;
						}
						stkOp.Push( szOp );
					}
				}
			}
			while( stkOp.Count != 0 )
			{
				string szTop = (string)stkOp.Pop();
				if( szTop == "(" )
					throw new RPN_Exception("Unmatched braces");
				
				IOperator oprtr = OperatorHelper.CreateOperator( szTop );
				arrFinalExpr.Add( oprtr );

				szResult += szTop;
			}
			return arrFinalExpr;
		}

		#endregion

		public string Convert2String( ArrayList arrExpr )
		{
			string szResult = "";
			foreach( object obj in arrExpr )
			{
				szResult += obj.ToString();
			}
			return szResult;
		}


		#region RPN_Evaluator

		/// <summary>
		/// Algo of EvaluateRPN (source : Expression Evaluator : using RPN by lallous 
		/// in the C++/MFC section of www.CodeProject.com.
		/// 1.	Initialize stack for storing results, prepare input postfix (or RPN) expression. 
		///	2.	Start scanning from left to right till we reach end of RPN expression 
		///	3.	Get token, if token is: 
		///		I.	An operator: 
		///			a.	Get top of stack and store into variable op2; Pop the stack 
		///			b.	Get top of stack and store into variable op1; Pop the stack 
		///			c.	Do the operation expression in operator on both op1 and op2 
		///			d.	Push the result into the stack 
		///		II.	An operand: stack its numerical representation into our numerical stack. 
		///	4.	At the end of the RPN expression, the stack should only have one value and 
		///	that should be the result and can be retrieved from the top of the stack. 
		/// </summary>
		/// <param name=szExpr>Expression to be evaluated in RPNotation with
		/// single character variables</param>
		/// <param name=htValues>Values for each of the variables in the expression</param>
		/// 		
		public object EvaluateRPN( ArrayList arrExpr, Type varType, Hashtable htValues )
		{
			// initialize stack (integer stack) for results
			Stack stPad = new Stack();
			// begin loop : scan from left to right till end of RPN expression
			foreach( object var in arrExpr )
			{
				Operand op1 = null;
				Operand op2 = null;
				IOperator oprtr = null;
				// Get token
				// if token is 
				if( var is IOperand )
				{
					// Operand : push onto top of numerical stack
					stPad.Push( var );
				}
				else if( var is IOperator )
				{
					// Operator :	
					//		Pop top of stack into var 1 - op2 first as top of stack is rhs
					op2 = (Operand)stPad.Pop();
					if( htValues != null )
					{
						op2.Value = htValues[op2.Name];
					}
					//		Pop top of stack into var 2
					op1 = (Operand)stPad.Pop();
					if( htValues != null )
					{
						op1.Value = htValues[op1.Name];
					}
					//		Do operation exp for 'this' operator on var1 and var2
					oprtr = (IOperator)var;
					IOperand opRes = oprtr.Eval( op1, op2 );
					//		Push results onto stack
					stPad.Push( opRes );
				}
			}
			//	end loop
			// stack ends up with single value with final result
			return ((Operand)stPad.Pop()).Value;
		}
		#endregion
	}
	#endregion

	#region UtilClasses

	/// <summary>
	/// The given expression can be parsed as either Arithmetic or Logical or 
	/// Comparison ExpressionTypes.  This is controlled by the enums 
	/// ExpressionType::ET_ARITHMETIC, ExpressionType::ET_COMPARISON and
	/// ExpressionType::ET_LOGICAL.  A combination of these enum types can also be given.
	/// E.g. To parse the expression as all of these, pass 
	/// ExpressionType.ET_ARITHMETIC|ExpressionType.ET_COMPARISON|ExpressionType.ET_LOGICAL 
	/// to the Tokenizer c'tor.
	/// </summary>
	[Flags]
	public enum ExpressionType
	{
		ET_ARITHMETIC = 0x0001,
		ET_COMPARISON = 0x0002,
		ET_LOGICAL = 0x0004
	}
	/// <summary>
	/// Currently not used.
	/// </summary>
 	public enum TokenType
	{
		TT_OPERATOR,
		TT_OPERAND
	}
	/// <summary>
	/// Represents each token in the expression
	/// </summary>
	public class Token
	{
		public Token( string szValue )
		{
			m_szValue = szValue;
		}
		public string Value
		{
			get
			{
				return m_szValue;
			}
		}
		string m_szValue;
	}
	/// <summary>
	/// Is the tokenizer which does the actual parsing of the expression.
	/// </summary>
	public class Tokenizer : IEnumerable
	{
		public Tokenizer( string szExpression  ):this(szExpression,	ExpressionType.ET_ARITHMETIC|
																	ExpressionType.ET_COMPARISON|
																	ExpressionType.ET_LOGICAL)
		{
		}
		public Tokenizer( string szExpression, ExpressionType exType )
		{
			m_szExpression = szExpression;
			m_exType = exType;
			m_RegEx = new Regex(OperatorHelper.GetOperatorsRegEx( m_exType ));
			m_strarrTokens = SplitExpression( szExpression );
		}
		public IEnumerator GetEnumerator()
		{
			return new TokenEnumerator( m_strarrTokens );
		}
		public string[] SplitExpression( string szExpression )
		{
			return m_RegEx.Split( szExpression );
		}
		ExpressionType m_exType;
		string m_szExpression;
		string[] m_strarrTokens;
		Regex m_RegEx;
	}

	/// <summary>
	/// Enumerator to enumerate over the tokens.
	/// </summary>
	public class TokenEnumerator : IEnumerator
	{
		Token m_Token;
		int m_nIdx;
		string[] m_strarrTokens;

		public TokenEnumerator( string[] strarrTokens )
		{
			m_strarrTokens = strarrTokens;
			Reset();
		}
		public object Current
		{
			get
			{
				return m_Token;
			}
		}
		public bool MoveNext()
		{
			if( m_nIdx >= m_strarrTokens.Length )
				return false;

			m_Token = new Token( m_strarrTokens[m_nIdx]);
			m_nIdx++;
			return true;
		}
		public void Reset()
		{
			m_nIdx = 0;
		}
	}
	#region Exceptions
	/// <summary>
	/// For the exceptions thrown by this module.
	/// </summary>
	public class RPN_Exception : ApplicationException
	{
		public RPN_Exception()
		{
		}
		public RPN_Exception( string szMessage):base( szMessage)
		{
		}
		public RPN_Exception( string szMessage, Exception innerException ):base( szMessage, innerException)
		{
		}
	}
	#endregion
	#endregion

	
	
	
	#region TODO List
	/// TODO: Support for unary operators
	/// TODO: Support for bitwise & and |
	/// TODO: how to handle a combo expression with multiple braces as a logical/comparison expression?
	///  e.g. ((2+3)*2<10 || 1!=1) && 2*2==4 as a logical expression?
	/// TODO: Form to accept values for formulae
	#endregion

}
