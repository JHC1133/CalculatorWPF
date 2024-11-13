using CalculatorWPF.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorWPF.ViewModels
{
    public class StandardViewModel : BaseViewModel
    {
        private const int _displayMaxChar = 21;
        private const int _initialFontSize = 47;
        private const string _initialDisplayContent = "0";
        private string _displayContent;
        private bool _initialDisplayState = true;
        private int _displayFontSize = _initialFontSize;
        private decimal _rawValue = 0; 

        public ICommand NumberCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        
        public int DisplayFontSize
        {
            get => _displayFontSize;
            set
            {
                _displayFontSize = value;
                OnPropertyChanged();
                Debug.WriteLine("DisplayFontSize set");
            }
        }
        public string DisplayContent
        {
            get => _displayContent = FormatDisplay(_rawValue);
            set
            {
                _displayContent = value;
                OnPropertyChanged();
                Debug.WriteLine("DisplayContent set");
            }
        }

        public StandardViewModel()
        {
            NumberCommand = new RelayCommand(ExecuteNumberCommand);
            BackspaceCommand = new RelayCommand(ExecuteBackspaceCommand);
            DisplayContent = _initialDisplayContent;
        }

        private void InitICommands()
        {
            NumberCommand = new RelayCommand(ExecuteNumberCommand);
        }

        private void ExecuteNumberCommand(object parameter)
        {
            if (_initialDisplayState)
            {
                DisplayContent = string.Empty;
                _rawValue = 0;
                _initialDisplayState = false;
            }

            if (_displayContent.Length < _displayMaxChar)
            {
                if (parameter is string number)
                {
                    var newDisplayValue = _displayContent + number;

                    if (decimal.TryParse(newDisplayValue, out var result))
                    {
                        _rawValue = result;
                        DisplayContent = FormatDisplay(_rawValue);
                    }
                    //DisplayContent += number;
                }

                Debug.WriteLine("_displaycontent:" + DisplayContent);
            }

            DisplayContentFontSetter();
        }

        private void ChangeDisplayFontSize(int size)
        {
            DisplayFontSize = size;
        }

        //private void ExecuteBackspaceCommand(object parameter)
        //{
        //    if (_displayContent.Length > 1)
        //    {
        //        DisplayContent = _displayContent.Substring(0, _displayContent.Length - 1);

        //        Debug.WriteLine("Backspace executed");
        //    }

        //    if (_displayContent.Length <= 0)
        //    {
        //        DisplayContent = _initialDisplayContent;
        //        _initialDisplayState = true;
        //    }

        //    DisplayContentFontSetter();
        //}

        private void ExecuteBackspaceCommand(object parameter)
        {
            // Check if the display is in scientific notation format
            if (DisplayContent.Contains("E"))
            {
                // Convert scientific notation back to a regular string representation
                DisplayContent = _rawValue.ToString("#,##0");

                Debug.WriteLine("Backspace executed contains E, DisplayContent: " + DisplayContent);

            }
            else if (_displayContent.Length > 1)
            {
                Debug.WriteLine("Backspace executed > 1, DisplayContent: " + DisplayContent);

                // Perform the backspace on the display content by trimming the last character
                _displayContent = _displayContent.Substring(0, _displayContent.Length - 1);

                // Update DisplayContent with the new string
                DisplayContent = _displayContent; // This will update the ViewModel

                // Parse the updated display content back to _rawValue to keep calculations accurate
                if (decimal.TryParse(_displayContent.Replace(" ", ""), out var newValue))
                {
                    _rawValue = newValue;
                }
            }
            else
            {
                Debug.WriteLine("Backspace executed initial, DisplayContent: " + DisplayContent);

                // Reset to initial state if display length is 1 or less
                DisplayContent = _initialDisplayContent;
                _initialDisplayState = true;
                _rawValue = 0;
            }

            DisplayContentFontSetter();
            //Debug.WriteLine("Backspace executed, DisplayContent: " + DisplayContent);
        }



        private string FormatDisplay(decimal value)
        {
            string display = "";

            if (value > 1e18m || value < 1e-18m && value != 0)
            {
                display = value.ToString("0.###E+0");
            }
            else
            {
                display = Math.Abs(value) < 1000 ? value.ToString() : value.ToString("#,##0");
            }
            return display;
        }




        private void DisplayContentFontSetter()
        {
            if (_displayContent.Length > 11)
            {
                switch (_displayContent.Length)
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
