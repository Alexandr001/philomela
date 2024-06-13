using System.Text;
using Philomela.Application.Common.Services.Interfaces;

namespace Philomela.Application.Common.Services
{
    /// <inheritdoc />
    internal sealed class CalculationService : ICalculationService
    {
        /// <inheritdoc />
        public string GetReversePolishEntry(string infix)
        {
            infix = infix.Replace(" ", string.Empty);
            StringBuilder postfix = new();
            Stack<char> stack = new();

            foreach (char c in infix)
            {
                if (char.IsLetterOrDigit(c))
                {
                    postfix.Append(c);
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfix.Append(stack.Pop());
                    }

                    stack.Pop();
                }
                else
                {
                    while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                    {
                        postfix.Append(stack.Pop());
                    }

                    stack.Push(c);
                }
            }

            while (stack.Count > 0)
            {
                postfix.Append(stack.Pop());
            }

            return postfix.ToString();
        }

        /// <inheritdoc />
        public double CalculateExpression(string postfix)
        {
            Stack<double> stack = new();

            foreach (char c in postfix)
            {
                if (char.IsDigit(c))
                {
                    stack.Push(c - '0');
                }
                else
                {
                    double rightOperand = stack.Pop();
                    double leftOperand = stack.Pop();

                    switch (c)
                    {
                    case '+':
                        stack.Push(leftOperand + rightOperand);
                        break;
                    case '-':
                        stack.Push(leftOperand - rightOperand);
                        break;
                    case '*':
                        stack.Push(leftOperand * rightOperand);
                        break;
                    case '/':
                        stack.Push(leftOperand / rightOperand);
                        break;
                    case '^':
                        stack.Push(Math.Pow(leftOperand, rightOperand));
                        break;
                    }
                }
            }

            return stack.Pop();
        }

        /// <summary>
        ///     Получить приоритет действий.
        /// </summary>
        /// <param name="op"> Оператор.</param>
        /// <returns></returns>
        private int GetPriority(char op)
        {
            return op switch
            {
                    '+' or '-' => 1,
                    '*' or '/' => 2,
                    '^' => 3,
                    _ => -1,
            };
        }
    }
}
