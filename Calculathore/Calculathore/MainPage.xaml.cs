using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculathore
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string[] operands = new string[2];
        private string operation;
        private string operand_modifier;
        private int maxLength;
        public MainPage()
        {
            operands[0] = "0";
            operands[1] = "0";
            operand_modifier = null;
            operation   = null;
            maxLength   = 10;
            InitializeComponent();
        }

        private int GetActiveArgument()
        {
            int index = 0;
            if (operation != null)
            {
                index = 1;
            }
            return index;
        }

        void ButtonClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ("0123456789.".Contains(b.Text))
            {
                SetOperand(b);
            }
            else if("√%".Contains(b.Text))
            {
                operand_modifier = b.Text;
                Evaluate();
            }
            else if("÷*-+".Contains(b.Text))
            {
                if (operands[1] != "0")
                {
                    Evaluate();
                }
                else
                {
                    SetOperation(b);
                }
            }
            else if(b.Text == "=")
            {
                Evaluate();
            }

        }


        void SetOperand(Button b)
        {
            int index = GetActiveArgument();

            if ((b.Text == ".") && (!operands[index].Contains(b.Text)) && (operands[index].Length < maxLength))
            {
                operands[index] += b.Text;
            }
            if ((b.Text == "0") && (operands[index].Any(s => s.ToString() != "0".ToString())))
            {
                operands[index] += b.Text;
            }
            else if ("123456789".Contains(b.Text))
            {
                if (operands[index] == "0")
                {
                    operands[index] = b.Text;
                }
                else if (operands[index].Length < maxLength)
                {
                    operands[index] += b.Text;
                }
            }
            ScreenUpdate();
        }


        void SetOperation(Button b)
        {
            operation = b.Text;
            ScreenUpdate();
        }


        void ClearButtonClicked(object sender, EventArgs e)
        {
            operands[0] = "0";
            operands[1] = "0";
            operation = null;

            ScreenUpdate();
        }


        void SignButtonClicked(object sender, EventArgs e)
        {
            int index = GetActiveArgument();
            if (operands[index][0] != '-')
            {
                operands[index] = "-" + operands[index];
            }
            else
            {
                operands[index] = operands[index].Substring(1);
            }
            ScreenUpdate();
        }


        private async void Evaluate()
        {
            int index = GetActiveArgument();

            if (operands[0] == "err")
            {
                operands[1] = "0";
                operation = null;
                ScreenUpdate();
                return;
            }
            else if (operand_modifier != null)
            {
                switch (operand_modifier)
                {
                    case "√":
                        operands[index] = Math.Sqrt(double.Parse(operands[index])).ToString();
                        ScreenUpdate();
                        return;
                        break;
                    case "%":
                        if (index == 1)
                        {
                            operands[1] = (double.Parse(operands[1]) * double.Parse(operands[0]) / 100).ToString();
                        }
                        else
                        {
                            operands[0] = "0";
                            ScreenUpdate();
                            return;
                        }
                        break;
                }
            }


            double result = 0;
            switch (operation)
            {
                case "÷":
                    if (operands[1] == "0")
                    {
                        operands[0] = "err";
                        operands[1] = null;
                        operation = null;
                        ScreenUpdate();
                        return;
                    }
                    else
                    {
                        result = double.Parse(operands[0]) / double.Parse(operands[1]);
                    }
                    break;
                case "*":
                    result = double.Parse(operands[0]) * double.Parse(operands[1]);
                    break;
                case "-":
                    result = double.Parse(operands[0]) - double.Parse(operands[1]);
                    break;
                case "+":
                    result = double.Parse(operands[0]) + double.Parse(operands[1]);
                    break;
            }
            operands[0] = result.ToString();
            operands[1] = "0";
            operation = null;
            ScreenUpdate();
        }


        void ScreenUpdate()
        {
            int index = GetActiveArgument();
            Screen.Text = $"{operands[index]}";
        }

    }
}
