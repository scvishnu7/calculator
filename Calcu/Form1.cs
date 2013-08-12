using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Calcu;

namespace Calcu
{
    public partial class Form1 : Form
    {
        public bool Offed = false;

        #region Mouse Capture Move and Release
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        #endregion
        private Memory Memo = new Memory();
        private int CursorPos = 0;
        private string Sub1="", Sub2="";
        public string CurDRG="D";
        private bool Pressedh = false;
        private bool PressedS = false;
        private bool PressedA = false;
        public string MODE = "AIRTH";
        public double Ans = 0;
        public double Mem = 0;
        private bool IsEvaluated = false;
       
        public Form1()
        {
            InitializeComponent();
           
            lblD.Show();
            lblG.Hide();
            lblR.Hide();
            lblh.Hide();
            lblS.Hide();
            lblA.Hide();
            lblMODE.Hide();
            lblSD.Hide();
            lblFRACE.Hide();
            lblFIX.Hide();
            lblSCI.Hide();
            lblM.Hide();
        }

        
        private void button28_Click(object sender, EventArgs e)
        {
            panelLCD.Hide();
            if (!Offed)
                Offed = true;
            else if (Offed)
                this.Close();
        }
        
        private void tbExpression_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("V");
            else 
            ShowExper("1");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("W");
            else 
            ShowExper("2");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("X");
            else 
            ShowExper("3");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("Q");
            else 
            ShowExper("4");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("R");
            else 
            ShowExper("5");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();
            if (PressedA)
                ShowExper("S");
            else 
            ShowExper("6");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();
            if (PressedA)
                ShowExper("L");
            else 
            ShowExper("7");
        }

        private void button26_Click(object sender, EventArgs e)
        {

            if (IsEvaluated)
                ClearMe();
            if (PressedA)
                ShowExper("M");
            else 
            ShowExper("8");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper("N");
            else 
            ShowExper("9");
        }

        private void btndot_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();

            if (PressedA)
                ShowExper(",");
            else if (PressedS)
                ;//conversion x,y to r,Th
            else
                ShowExper("0");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
                ClearMe();
            if (PressedA)
                ShowExper(":");
            else
                ;//converrsion r,th to x,y
            ShowExper(".");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            IsEvaluated = true;
            if (PressedA)
            {
                ShowExper("%");
                return;
            }
            if (PressedS)
            {
                btnC_Click(this, null);
                ShowExper("Ans");
                return;
            }
            tbExpression.Text = Sub1 + Sub2;
            Expression E1 = new Expression(tbExpression.Text);
            E1.CurDRG = this.CurDRG;
            E1.PrevAns = Ans;
            E1.Mem = this.Mem;
            E1.Str2ExperItem();
            E1.InFix2PostFix();
            double Result = E1.CalcuPostFix();
            
            tbResult.Text = Convert.ToString(Result);
            if (tbResult.Text != "")
                Ans = Convert.ToDouble(tbResult.Text);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
            {
                btnC_Click(this,null);
                ShowExper("Ans+");
                return;
            }
            
            if (PressedA)
                ShowExper("Y");
            else if (PressedS)
                ;//OR fucntion
            else
                ShowExper("+");
            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
            {
                btnC_Click(this, null);
                ShowExper("Ans*");
                return;
            }
            
            if (PressedA)
                ShowExper("T");
            else 
            ShowExper("*");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
            {
                btnC_Click(this, null);
                ShowExper("Ans-");
                return;
            }
            
            if (PressedA)
                ShowExper("Z");
            else if (PressedS)
                ;//XNOR function
            else
                ShowExper("-");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (IsEvaluated)
            {
                btnC_Click(this, null);
                ShowExper("Ans/");
                return;
            }
            
            if (PressedA)
                ShowExper("U");
            else 
            ShowExper("/");
            
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            Offed = false;
            tbExpression.Clear();
            Sub1 = "";
            Sub2 = "";
            CursorPos = 0;
            tbResult.Clear();
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            if (PressedS)
            {
                Mem = 0;
                lblM.Hide();
                return;
            }
            
            Offed = false;
            panelLCD.Show();
            btnC_Click(this, null);
        }


        ///Function for Result calculation.
        ///
        private void Form1_MouseDown(object sender,
        System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("O");
            else 
            ShowExper("(");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (PressedA)
                ShowExper("P");
            else 
            ShowExper(")");
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Pressedh)
                ShowExper("sinh(");
            else if (PressedS)
                ShowExper("Asin(");
            else if (PressedA)
                ShowExper("A");
            else
                ShowExper("sin(");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Pressedh)
                ShowExper("cosh(");
            else if (PressedS)
                ShowExper("Acos(");
            else if (PressedA)
                ShowExper("B");
            else
                ShowExper("cos(");
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Pressedh)
                ShowExper("tanh(");
            else if (PressedS)
                ShowExper("Atan(");
            else if (PressedA)
                ShowExper("C");
            else
                ShowExper("tan(");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (PressedS)
                ShowExper("10^");
            else if (PressedA)
                ShowExper("E");
            ShowExper("log(");

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (PressedS)
                ShowExper("e^");
            else if(PressedA)
                ShowExper("F");
            else
                ShowExper("ln(");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PressedA)
                ShowExper("I");
            else 
            ShowExper("^2");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (PressedA)
                ShowExper("G");
            else 
            ShowExper("^");
        }

        private void button17_Click(object sender, EventArgs e)
        {

            if (CursorPos == 0)
                return;
            Sub1 = Sub1.Substring(0, Sub1.Length - 1);
            tbExpression.Text = Sub1 + "|" + Sub2;
            CursorPos--;

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            IsEvaluated = false;
            if (CursorPos == 0)
                return;
            
            CursorPos--;
            
            tbExpression.Text = Sub1 + Sub2;
            Sub1 = tbExpression.Text.Substring(0, CursorPos);
            Sub2 = tbExpression.Text.Substring(CursorPos, tbExpression.Text.Length-Sub1.Length);
            
            tbExpression.Text = Sub1 +"|"+ Sub2;
            tbResult.Text = "";

        }
        private void ShowExper(string KeyPressed)
        {
            Sub1 += KeyPressed;
            tbExpression.Text = Sub1 + "|" + Sub2;
            CursorPos += KeyPressed.Length;

            if (PressedA)
            {
                PressedA = false;
                lblA.Hide();
            }
            if (PressedS)
            {
                PressedS = false;
                lblS.Hide();
            }

            if (IsEvaluated)
                IsEvaluated = false;
        }
        private void ClearMe()
        {
            tbResult.Text = "";
            tbExpression.Text = "";
            IsEvaluated = false;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (PressedA)
            {
                ShowExper("me");
                return;
            }
            if (Sub2.Length == 0)
                return;
            Sub2 = Sub2.Substring(1, Sub2.Length-1);
            tbExpression.Text = Sub1 + "|" + Sub2;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            IsEvaluated = false;
            if (CursorPos == tbExpression.Text.Length)
                return;
            tbExpression.Text = Sub1 + Sub2;
            CursorPos++;
            Sub1 = tbExpression.Text.Substring(0, CursorPos);
            Sub2 = tbExpression.Text.Substring(CursorPos, tbExpression.Text.Length - Sub1.Length);

            tbExpression.Text = Sub1 + "|" + Sub2;
            tbResult.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CurDRG == "D"){
                CurDRG = "R";
                lblD.Hide();
                lblR.Show();
                if (PressedS) //Radian = Deg * 3.1415926535897932384626433832795 / 180;
                {
                    tbExpression.Text = Sub1 + Sub2;
                    tbResult.Text = Convert.ToString((Convert.ToDouble(tbExpression.Text) * 3.1415926535897932384626433832795 / 180));

                    PressedS = false;
                    lblS.Hide();
                }
            }
            else if (CurDRG == "R")
            {
                CurDRG = "G";
                lblR.Hide();
                lblG.Show();
                if (PressedS) //Gradian = Redian*100/3.1415926535897932384626433832795 ;
                {
                    tbExpression.Text = Sub1 + Sub2;
                    tbResult.Text = Convert.ToString(Convert.ToDouble(tbExpression.Text) * 200 / 3.1415926535897932384626433832795);

                    PressedS = false;
                    lblS.Hide();
                }
            }
            else if (CurDRG == "G")
            {
                CurDRG = "D";
                lblG.Hide();
                lblD.Show();
                if (PressedS) //Deg = Gradient *1.8;
                {
                    tbExpression.Text = Sub1 + Sub2;
                    tbResult.Text = Convert.ToString(Convert.ToDouble(tbExpression.Text) * 0.9);

                    PressedS = false;
                    lblS.Hide();
                    
                }
            }

        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (PressedA)
                ShowExper("H");
            else 
            ShowExper("sqrt(");
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            PressedS = !PressedS;
            if (PressedS)
                lblS.Show();
            else 
                lblS.Hide();
        }

        private void btnAlpha_Click(object sender, EventArgs e)
        {
            PressedA = !PressedA;
            if (PressedA)
                lblA.Show();
            else
                lblA.Hide();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Pressedh = !Pressedh;
            if (Pressedh)
                lblh.Show();
            else
                lblh.Hide();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (PressedS)
                ShowExper("PI");
            if (PressedA)
                ShowExper("c");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ShowExper("-");
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("J");
            else 
                ;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("K");
            else
                ;
        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("Mr");
            else if (PressedS) //M-
            {
               
                
                if (tbResult.Text == "")
                    return;
                Mem -= Convert.ToDouble(tbResult.Text);
                if(Mem != 0)
                    lblM.Show();
                else 
                    lblM.Hide();

            }
            else//M+
            {
               
                if (tbResult.Text == "")
                    return;
                Mem += Convert.ToDouble(tbResult.Text);
                if (Mem != 0)
                    lblM.Show();
                else 
                    lblM.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("NA");
            else
                ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("Vm");
            else
                ;
        }

        private void button21_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("h");//PLANKS CONSTANT
            else
                ;
        }

        private void button20_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("G");//gRAVITIONAL CONST
            else
            {
               
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

            if (PressedA)
                ShowExper("e"); //CHARGE OF ELECTRON
            else if (PressedS)
            {
                // function to write formula to memory


            }
            else { 
            //fucntion to read current content from memory and calcualte // ask for individual value
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }

}
