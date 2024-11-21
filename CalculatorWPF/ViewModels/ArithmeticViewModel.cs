using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CalculatorWPF.Core;
using CalculatorWPF.Models;

namespace CalculatorWPF.ViewModels
{
    public abstract class ArithmeticViewModel : BaseViewModel
    {
        protected const string _initialSum = "0";
        protected string _displayOperation;
        protected string _displayResult;
        protected bool _initialDisplayState;
        protected bool _hasStoredOperand;
        protected int _displayMaxChar;
        protected int _initialFontSize;
        protected int _displayFontSize;

        public ICommand PosNegToggleCommand { get; protected set; }
        public ICommand CommaCommand { get; protected set; }
        public ICommand EqualsCommand { get; protected set; }
        public ICommand NumberCommand { get; protected set; }
        public ICommand BackspaceCommand { get; protected set; }
        public ICommand ClearCommand { get; protected set; }
        public ICommand SetOperatorCommand { get; protected set; }

        public int DisplayFontSize { get => _displayFontSize; set { _displayFontSize = value; OnPropertyChanged(); } }
        public string DisplayOperation { get => _displayOperation; set { _displayOperation = value; OnPropertyChanged(); } }
        public string DisplayResult { get => _displayResult; set { _displayResult = value; OnPropertyChanged(); } }


        protected ArithmeticViewModel()
        {
            _initialFontSize = 47;
            _displayOperation = string.Empty;
            _displayResult = _initialSum;
            _initialDisplayState = true;
            _displayFontSize = _initialFontSize;
        }

        protected abstract void SetMaxChar();
        protected abstract void InitICommands();
        protected abstract void ExecuteNumberCommand(object parameter);
        protected abstract void ExecuteSetOperatorCommand(object parameter);
        protected abstract void ExecuteEqualsCommand(object parameter);
        protected abstract void ExecuteClearCommand(object parameter);


        private void ChangeDisplayFontSize(int size)
        {
            DisplayFontSize = size;
        }

        protected void ResetDisplay()
        {
            DisplayResult = "0";
            DisplayOperation = string.Empty;
            _initialDisplayState = true;
        }

        protected void DisplayContentFontSetter()
        {
            if (_displayResult.Length > 11)
            {
                switch (_displayResult.Length)
                {
                    case 12:
                        ChangeDisplayFontSize(_initialFontSize);
                        break;
                    case 13:
                        ChangeDisplayFontSize(38);
                        break;
                    case 14:
                        ChangeDisplayFontSize(36);
                        break;
                    case 15:
                        ChangeDisplayFontSize(34);
                        break;
                    case 16:
                        ChangeDisplayFontSize(32);
                        break;
                    case 17:
                        ChangeDisplayFontSize(30);
                        break;
                }
            }
        }
    }
}
