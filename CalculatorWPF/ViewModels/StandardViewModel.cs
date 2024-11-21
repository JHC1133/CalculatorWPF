using CalculatorWPF.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorWPF.Models;

namespace CalculatorWPF.ViewModels
{
    public class StandardViewModel : ArithmeticViewModel
    {
        private readonly StandardCalculatorModel _calculatorModel;

        public StandardViewModel()
        {
            _calculatorModel = new StandardCalculatorModel();
            SetMaxChar();
            InitICommands();          
        }

        protected override void SetMaxChar()
        {
            _displayMaxChar = 21;
        }

        protected override void InitICommands()
        {
            NumberCommand = new RelayCommand(ExecuteNumberCommand);
            BackspaceCommand = new RelayCommand(ExecuteBackspaceCommand);
            EqualsCommand = new RelayCommand(ExecuteEqualsCommand);
            ClearCommand = new RelayCommand(ExecuteClearCommand);
            SetOperatorCommand = new RelayCommand(ExecuteSetOperatorCommand);
        }

        protected override void ExecuteClearCommand(object parameter)
        {
            ResetDisplay();
            _calculatorModel.Clear();
        }

        protected override void ExecuteEqualsCommand(object parameter)
        {
            if (decimal.TryParse(DisplayResult.Replace(" ", ""), out decimal currentValue))
            {
                _calculatorModel.EnterNumber(currentValue);
                _calculatorModel.Calculate();
                DisplayOperation = ($"{_calculatorModel.Operand1.ToString()} {_calculatorModel.CurrentOperator} {_calculatorModel.Operand2} =");
                DisplayResult = _calculatorModel.Result.ToString();
            }

        }

        protected override void ExecuteSetOperatorCommand(object parameter)
        {
            if (decimal.TryParse(DisplayResult.Replace(" ", ""), out decimal currentValue))
            {
                _calculatorModel.Clear();
                _calculatorModel.EnterNumber(currentValue);
                DisplayOperation = DisplayResult + " " + parameter.ToString() + " ";
                _calculatorModel.SetOperator(parameter.ToString());
                _hasStoredOperand = true;
            }
        }

        protected override void ExecuteNumberCommand(object parameter)
        {

            if (parameter is string number)
            {
                if (_initialDisplayState)
                {
                    DisplayResult = string.Empty;
                    _calculatorModel.Clear();
                    _initialDisplayState = false;
                }

                if (_hasStoredOperand)
                {
                    DisplayResult = string.Empty;
                    _hasStoredOperand = false;
                }

                if (_displayResult.Length < _displayMaxChar)
                {
                    DisplayResult += number;

                    if (decimal.TryParse(DisplayResult.Replace(" ", ""), out decimal currentValue))
                    {
                        _calculatorModel.EnterNumber(currentValue);
                    }

                    Debug.WriteLine("_displaycontent:" + DisplayResult);
                }
            }

            DisplayContentFontSetter();
        }

        

        private void ExecuteBackspaceCommand(object parameter)
        {
            if (DisplayResult.Length > 1)
            {
                DisplayResult = DisplayResult.Substring(0, DisplayResult.Length - 1);

                if (decimal.TryParse(DisplayResult.Replace(" ", ""), out decimal parsedValue))
                {
                    _calculatorModel.EnterNumber(parsedValue);
                }

                Debug.WriteLine("Backspace executed - normal, DisplayResult: " + DisplayResult);
            }
            else
            {
                // If only 1 character is left (or nothing), reset to the initial state
                DisplayResult = _initialSum;
                _initialDisplayState = true;
                _calculatorModel.Clear(); // Clear any stored values or calculations

                Debug.WriteLine("Backspace executed - reset, DisplayResult: " + DisplayResult);
            }

            // Optionally adjust font size based on the new display content
            DisplayContentFontSetter();
        }   



    }
}
