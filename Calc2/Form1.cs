using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc2
{
    public partial class NicksCalc : Form
    {

        public NicksCalc()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // GLOBALS
        String firstOperand;
        String operatorSym;
        bool haveOperand = false;
        bool readyFlip = false;
        
        // METHODS
        private void button_Click(object sender, EventArgs e)
        {
            if (readyFlip)
            {
                label1.Text = "";
                readyFlip = false;
            }
            Button current = (Button)sender; 
            label1.Text += current.Text;
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            Button current = (Button)sender;
            if (haveOperand)
            {
                Evaluate(sender, e);
            }
            operatorSym = current.Text;
            haveOperand = true;
            firstOperand = label1.Text;
            readyFlip = true;
        }

        private void EvaluateUnary(object sender, EventArgs e)
        {
            Button current = (Button)sender;
            string unaryOperator = current.Text;
            int operand = Convert.ToInt32(label1.Text);
            if (unaryOperator == "Sigma")
            {
                int sum = 0;
                for (int i = 1; i < operand; i++)
                {
                    if (operand % i == 0)
                    {
                        sum += i;
                    }
                }
                label1.Text = Convert.ToString(sum);
            }
            else if (unaryOperator == "Totient")
            {
                int count = 0;
                for (int i = 1; i < operand; i++)
                {
                    for (int j = i; j > 0; j--)
                    {
                        if ((operand % j == 0) && (i % j == 0))
                        {
                            if (j == 1)
                            {
                                count += 1;
                            }
                            break;
                        }
                    }
                }
                label1.Text = Convert.ToString(count);
            }
            else if (unaryOperator == "For Rome!")
            {
                string[] symbols = new string[7] {"I","V","X","L","C","D","M"}; // 1 - 1000
                label1.Text = "";
                for (int i = 100, h = (symbols.Length - 1)/ 2 - 1; i >= 1; i /= 10, h--)
                {
                    int j = (operand % (10 * i)) / i;
                    if ((j % 5) <= 3)
                    {
                        label1.Text += (j / 5 > 0 ? symbols[2*h+1] : "");
                        for (int k = 0; k < j % 5; k++)
                        {
                            label1.Text += symbols[2*h];
                        }
                    }
                    else
                    {
                        label1.Text += (j / 5 > 0 ? symbols[2*h] + symbols[2*(h+1)] : symbols[2*h] + symbols[2*h+1]); // figure out
                    }
                }
                readyFlip = true;
            }
        }

        private void Evaluate(object sender, EventArgs e)
        {
            if (haveOperand)
            {
                if (operatorSym == "+")
                {
                    label1.Text = Convert.ToString(Convert.ToInt32(firstOperand) + Convert.ToInt32(label1.Text));
                }
                else if (operatorSym == "-")
                {
                    label1.Text = Convert.ToString(Convert.ToInt32(firstOperand) - Convert.ToInt32(label1.Text));
                }
                else if (operatorSym == "*")
                {
                    label1.Text = Convert.ToString(Convert.ToInt32(firstOperand) * Convert.ToInt32(label1.Text));
                }
                else if (operatorSym == "/")
                {
                    label1.Text = Convert.ToString(Convert.ToInt32(firstOperand) / Convert.ToInt32(label1.Text));
                }
                else if (operatorSym == "%")
                {
                    label1.Text = Convert.ToString(Convert.ToInt32(firstOperand) % Convert.ToInt32(label1.Text));
                }
                else if (operatorSym == "^")
                {
                    int count = Convert.ToInt32(label1.Text);
                    int operand = Convert.ToInt32(firstOperand);
                    int product = 1;
                    while (count > 0)
                    {
                        count--;
                        product *= operand; 
                    }
                    label1.Text = Convert.ToString(product);
                }
                else if (operatorSym == "GCD")
                {
                    int operand1 = Convert.ToInt32(firstOperand);
                    int operand2 = Convert.ToInt32(label1.Text);
                    if (operand1 > operand2)
                    {
                        for (int i = operand1; i > 0; i--)
                        {
                            if ((operand1 % i == 0) && (operand2 % i == 0)) {
                                label1.Text = Convert.ToString(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = operand2; i > 0; i--)
                        {
                            if ((operand1 % i == 0) && (operand2 % i == 0))
                            {
                                label1.Text = Convert.ToString(i);
                                break;
                            }
                        }
                    }
                }
                else if (operatorSym == "LCD")
                {
                    int operand1 = Convert.ToInt32(firstOperand);
                    int operand2 = Convert.ToInt32(label1.Text);
                    if (operand1 > operand2)
                    {
                        for (int i = operand1; i < operand1*operand2+1; i++)
                        {
                            if ((i % operand1 == 0) && (i % operand2 == 0))
                            {
                                label1.Text = Convert.ToString(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = operand2; i < operand1 * operand2 + 1; i++)
                        {
                            if ((i % operand1 == 0) && (i % operand2 == 0))
                            {
                                label1.Text = Convert.ToString(i);
                                break;
                            }
                        }
                    }

                }
                haveOperand = false;
                readyFlip = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            haveOperand = false;
            readyFlip = false;
        }
    }
}
