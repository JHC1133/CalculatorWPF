using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWPF.Models
{
    public class StandardCalculatorModel
    {
        private decimal _operand1;
        private decimal _operand2;
        private decimal _result;
        private string _currentOperator;
        private bool _initState = true;
        private bool _isOperatorSet = false;

        public decimal Operand1 { get => _operand1; }
        public decimal Operand2 { get => _operand2; }
        public decimal Result { get => _result; }
        public string CurrentOperator { get => _currentOperator; }

        public void EnterNumber(decimal number)
        {
            if (_isOperatorSet)
            {
                _operand2 = number;
            }
            else
            {
                _operand1 = number;
            }
        }

        public void SetOperator(string operatorSymbol)
        {
            if (_isOperatorSet)
            {
                return;
            }

            _currentOperator = operatorSymbol;
            _isOperatorSet = true;
        }

        public void Calculate()
        {
            switch (_currentOperator)
            {
                case "+":
                    _result = _operand1 + _operand2; 
                    break;
                case "-":
                    _result = _operand1 - _operand2;
                    break;
                case "*":
                    _result = _operand1 * _operand2;
                    break;
                case "/":
                    if (_result != 0)
                    {
                        _result = _operand1 / _operand2;
                    }
                    else
                    {
                        throw new DivideByZeroException("DO NOT DIVIDE BY ZERO!!");
                    }
                    break;
            }

            Debug.WriteLine("Current value mode:" + _operand1);
            Debug.WriteLine("Current stored value model: " + _operand2);
        }

        public void Clear()
        {
            _operand2 = 0;
            _operand1 = 0;
            _currentOperator = string.Empty;
            _initState = true;
            _isOperatorSet = false;
            //_result = 0;
        }
    }
}
