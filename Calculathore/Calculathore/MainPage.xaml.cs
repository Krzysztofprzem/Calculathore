﻿using System;
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
        private string result;


        public MainPage()
        {
            operands[0] = null;
            operands[1] = null;
            operation   = null;
            result      = null;
            InitializeComponent();
        }

        void ButtonClicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ("0123456789.".Contains(b.Text))
            {
                SetOperand(b);
            }
            else if("÷*-+".Contains(b.Text))
            {
                SetOperation(b);
            }
            else if(b.Text == "=")
            {
                Evaluate();
            }

        }


        void SetOperand(Button b)
        {
            int index;
            if (operation == null)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }

            if ((b.Text == ".") && (!operands[index].Contains(b.Text)))
            {
                operands[index] += b.Text;
            }
            if ((b.Text == "0") && ((operands[index] == null) || (operands[index].Any(s => s.ToString() != "0".ToString()))))
            {
                operands[index] += b.Text;
            }
            else if ("123456789".Contains(b.Text))
            {
                if (operands[index] == "0")
                {
                    operands[index] = b.Text;
                }
                else
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
            operands[1] = null;
            operation = null;

            ScreenUpdate();
        }

        void BackspaceButtonClicked(object sender, EventArgs e)
        {
            ScreenUpdate();
        }

        private void Evaluate()
        {
            if ((operands[1] == null) || (operands[0] == "err"))
            {
                operands[1] = null;
                operation = null;
                ScreenUpdate();
                return;
            }

            // if first operand is infinifty,
            /*
            if (operands[0].Contains("inf"))
            {
                // then there is specific situation.
                // For * and ÷ operations, if second operand is smaller than 0, then result is
                // an additive inverse for first operand
                if (!"*÷".Contains(operation))
                {
                    if(double.Parse(operands[1])<0)
                    {
                        operands[0] = (operands[0] == "inf") ? "-inf" : "inf";
                    }
                    operands[1] = null;
                    operation = null;
                    ScreenUpdate();
                    return;
                }
                else
                {
                    operands[1] = null;
                    operation = null;
                    ScreenUpdate();
                    return;
                }
            }
            */

            double result = 0;
            switch (operation)
            {
                case "÷":
                    if (double.Parse(operands[1]) == 0)
                    {
                        //operands[0] = (double.Parse(operands[0])) >= 0 ? "inf" : "-inf";
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
            operands[1] = null;
            operation = null;
            ScreenUpdate();
        }

        void ScreenUpdate()
        {
            if (operation == null)
            {
                Screen.Text = $"{operands[0]}";
            }
            else if (operands[1] == null)
            {
                Screen.Text = $"{operands[0]} {operation}";
            }
            else
            {
                Screen.Text = $"{operands[0]} {operation} {operands[1]}";
            }
        }

    }
}
