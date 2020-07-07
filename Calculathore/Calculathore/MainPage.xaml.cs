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
        private double operand1;
        private double operand2;
        private int operation;
        private double result;


        public MainPage()
        {
            operand1  = 0;
            operand2  = 0;
            operation = 0;
            result    = 0;
            InitializeComponent();
        }


        void Addition(object sender, EventArgs args)
        {
            operation = 0;
        }

        void Substraction(object sender, EventArgs args)
        {
            operation = 1;
        }

        void Multiplication(object sender, EventArgs args)
        {
            operation = 2;
        }

        void Division(object sender, EventArgs args)
        {
            operation = 3;
        }

        void Result(object sender, EventArgs args)
        {
            switch(operation)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

    }
}
