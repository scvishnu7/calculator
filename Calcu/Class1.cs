using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Calcu
{
    class Expression
    {
        public string CurDRG = "D";
        private string exper;
        private ExperItem[] ItemInFix = new ExperItem[100];
        private ExperItem[] ItemPostFix = new ExperItem[100];
        private int IndexItemInFix = 0;
        private int IndexItemPostFix = 0;
        public double PrevAns;
        public double Mem;
        //MYFUCNTIONS 

        public Expression(string Exper)
        {
            this.exper = Exper;
        }
        public bool Str2ExperItem()
        {
            //I think it is now complete without the Trigonometry fucntion

            int i = 0;
            char ch1, ch2;

            string StrOperator = "", StrOperand = "";
            exper = "(" + exper + ")0";   //appending and prepending for this very for loop
            int Len = exper.Length;
            //TEst//
            Console.WriteLine("Exper = " + exper);
            ch2 = exper[i++];
            ExperItem LastItem = new ExperItem();
            ExperItem LastItem1 = new ExperItem();
            for (; i < Len; i++) // for each char
            {
                ch1 = ch2;
                ch2 = exper[i];
                //from left to right
                if (IsDigit(ch1))  //both are digit go on
                    StrOperand += ch1;
                else if(IsAlpha(ch1)) //both are alph go on
                    StrOperator += ch1;

                if (IsDigit(ch1) && IsAlpha(ch2))  //from operand to operator
                {
                    ItemInFix[IndexItemInFix] = new ExperItem();
                    ItemInFix[IndexItemInFix].Data = StrOperand;
                    ItemInFix[IndexItemInFix].Type = "Operand";
                    IndexItemInFix++;
                    StrOperand = "";
                    LastItem1 = LastItem;
                    LastItem.Data = StrOperand;
                    LastItem.Type = "Operand";
                }
                else if (IsAlpha(ch1) && IsDigit(ch2)) //from opeator to operand
                {
                    ItemInFix[IndexItemInFix] = new ExperItem();
                    string CurOperator = "", CurOpType = "";
                    for (int j = 0; j < StrOperator.Length; )
                    {
                        switch (StrOperator[j])
                        {
                            case '+':
                            case '*':
                            case '/':
                            case '^':
                            case '!':
                            case '-':
                            case ')':
                                CurOperator =   Convert.ToString(StrOperator[j]);
                                CurOpType = "Binary";
                                break;
                            case '%':
                                CurOperator = "%";
                                CurOpType = "Unary";
                                break;
                            case 'e':
                                CurOperator = "e";
                                CurOpType = "Const";
                                break;
                            
                            case '(':
                                if ((LastItem.Type == "Operand")||(LastItem.Data==")") )
                                {
                                    ItemInFix[IndexItemInFix] = new ExperItem();
                                    ItemInFix[IndexItemInFix].Data = "IM";
                                    ItemInFix[IndexItemInFix].Type = "Operator";
                                    ItemInFix[IndexItemInFix].OpType = "Binary";
                                    IndexItemInFix++;
                                }

                                    CurOperator = "(";
                                    CurOpType = "Binary";

                                break;


                        }
                        


                        if ((StrOperator[j] == 's') && (StrOperator[j + 1] == 'i') && (StrOperator[j + 2] == 'n') && (StrOperator[j+3]=='h'))
                        { CurOperator = "sinh"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'c') && (StrOperator[j + 1] == 'o') && (StrOperator[j + 2] == 's') && (StrOperator[j + 3] == 'h'))
                        { CurOperator = "cosh"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 't') && (StrOperator[j + 1] == 'a') && (StrOperator[j + 2] == 'n') && (StrOperator[j + 3] == 'h'))
                        { CurOperator = "tanh"; CurOpType = "Unary"; }

                        else if ((StrOperator[j] == 'M') && (StrOperator[j + 1] == 'r'))
                        { CurOperator = "Mr"; CurOpType = "Const"; }
                        else if ((StrOperator[j] == 'A') && (StrOperator[j + 1] == 's') && (StrOperator[j + 2] == 'i') && (StrOperator[j + 3] == 'n'))
                        { CurOperator = "Asin"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'A') && (StrOperator[j + 1] == 'c') && (StrOperator[j + 2] == 'o') && (StrOperator[j + 3] == 's'))
                        { CurOperator = "Acos"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'A') && (StrOperator[j + 1] == 't') && (StrOperator[j + 2] == 'a') && (StrOperator[j + 3] == 'n'))
                        { CurOperator = "Atan"; CurOpType = "Unary"; }

                        else if ((StrOperator[j] == 'l') && (StrOperator[j + 1] == 'n'))
                        { CurOperator = "ln"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'l') && (StrOperator[j + 1] == 'o') && (StrOperator[j + 2] == 'g'))
                        { CurOperator = "log"; CurOpType = "Unary"; }

                        else if ((StrOperator[j] == 's') && (StrOperator[j + 1] == 'i') && (StrOperator[j + 2] == 'n'))
                        { CurOperator = "sin"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'c') && (StrOperator[j + 1] == 'o') && (StrOperator[j + 2] == 's'))
                        { CurOperator = "cos"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 't') && (StrOperator[j + 1] == 'a') && (StrOperator[j + 2] == 'n'))
                        { CurOperator = "tan"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 's') && (StrOperator[j + 1] == 'q') && (StrOperator[j + 2] == 'r') && (StrOperator[j + 3] == 't'))
                        { CurOperator = "sqrt"; CurOpType = "Unary"; }
                        else if ((StrOperator[j] == 'P') && (StrOperator[j + 1] == 'I'))
                        { CurOperator = "PI"; CurOpType = "Const"; }

                        else if ((StrOperator[j] == 'A') && (StrOperator[j + 1] == 'n') && (StrOperator[j + 2] == 's'))
                        { CurOperator = "Ans"; CurOpType = "Const"; }
                       

                        j += CurOperator.Length;

                        ItemInFix[IndexItemInFix] = new ExperItem();
                        ItemInFix[IndexItemInFix].Data = CurOperator;
                        ItemInFix[IndexItemInFix].Type = "Operator";
                        ItemInFix[IndexItemInFix].OpType = CurOpType;
                        IndexItemInFix++;
                        CurOperator = "";

                        LastItem1 = LastItem;
                        LastItem.Data = CurOperator;
                        LastItem.Type = "Operator";
                        LastItem.OpType = CurOpType;
                    }//end of Operator parsing for loop
                    StrOperator = "";
                }


            }

            for (i = 0; i < IndexItemInFix; i++)
                Console.WriteLine(ItemInFix[i].Data + "  " + ItemInFix[i].Type + " " + ItemInFix[i].OpType);
            return true;

        }//end Function Str2ExperItem
        public bool InFix2PostFix()
        {
            Stack StackOperator = new Stack();
            int i;
            IndexItemPostFix = 0;
            string symb = "";
            string StackTop = "";

            for (i = 0; i < IndexItemInFix; i++)
            {

                symb = ItemInFix[i].Data;
                StackTop = StackOperator.TopItem;
                if (ItemInFix[i].Type == "Operand")
                {
                    ItemPostFix[IndexItemPostFix] = new ExperItem();
                    ItemPostFix[IndexItemPostFix].Type = "Operand";
                    ItemPostFix[IndexItemPostFix++].Data = symb;

                }
                else if (ItemInFix[i].OpType == "Const")
                {
                    ItemPostFix[IndexItemPostFix] = new ExperItem();
                    ItemPostFix[IndexItemPostFix].Type = "Operand";
                    ItemPostFix[IndexItemPostFix++].Data = Convert.ToString(ResolveConst(ItemInFix[i].Data));
                }
                else if (symb == ")")
                {
                    //pop all item till ( and add to Postfix
                    while ((!StackOperator.IsEmpty()) && (StackTop != "("))
                    {
                        ItemPostFix[IndexItemPostFix] = new ExperItem();
                        ItemPostFix[IndexItemPostFix].Data = StackTop;
                        ItemPostFix[IndexItemPostFix].OpType = FindOpType(StackTop);
                        ItemPostFix[IndexItemPostFix++].Type = "Operator";
                        StackOperator.Pop();
                        StackTop = StackOperator.TopItem;

                    }
                    StackOperator.Pop(); // Pop the symb "("   nothing if IsEmpty();
                    //StackOperator.Push("*"); // treat 3(4+4) as  3*(4+4)

                }
                else //in case of other symb including "("
                {

                    while ((!StackOperator.IsEmpty()) && (Precedence(StackTop, symb)))
                    {
                        ItemPostFix[IndexItemPostFix] = new ExperItem();
                        ItemPostFix[IndexItemPostFix].Data = StackTop;
                        ItemPostFix[IndexItemPostFix].OpType = FindOpType(StackTop);
                        ItemPostFix[IndexItemPostFix++].Type = "Operator";

                        StackOperator.Pop();
                        StackTop = StackOperator.TopItem;
                    }
                    StackOperator.Push(symb);
                }
            }//end for loop

            while ((!StackOperator.IsEmpty()))//when all symbol scanning finished.
            {
                //pop stack items and write them to the postfix experssion
                StackTop = StackOperator.Pop();
                ItemPostFix[IndexItemPostFix] = new ExperItem();
                ItemPostFix[IndexItemPostFix].Type = "Operator";
                ItemPostFix[IndexItemPostFix++].Data = StackTop;
            }

            //Lets Print the postfix exper
            for (i = 0; i < IndexItemPostFix; i++)
                Console.Write(ItemPostFix[i].Data);
            return true;
        }
        public double CalcuPostFix()
        {
            Stack StackOperand = new Stack();
            int i = 0;
            double Data1, Data2, TempResult;
            string Operator = "";
            for (i = 0; i < IndexItemPostFix; i++)
            {
                if (ItemPostFix[i].Type == "Operand")
                    StackOperand.Push(ItemPostFix[i].Data);
                else //in case of operator found
                {

                    if (ItemPostFix[i].OpType == "Binary")
                    {
                        Data2 = Convert.ToDouble(StackOperand.Pop());
                        Data1 = Convert.ToDouble(StackOperand.Pop());
                        Operator = ItemPostFix[i].Data;
                        TempResult = Calculate(Data1, Operator, Data2);
                        StackOperand.Push(Convert.ToString(TempResult));
                    }
                    else if (ItemPostFix[i].OpType == "Unary")
                    {
                        Data1 = Convert.ToDouble(StackOperand.Pop());
                        Operator = ItemPostFix[i].Data;
                        TempResult = Calculate(Data1, Operator);
                        StackOperand.Push(Convert.ToString(TempResult));
                    }
                }
            }
            double Result = Convert.ToDouble(StackOperand.Pop());

            return Result;
        }
        public bool Precedence(string Oper1, string Oper2)
        {
            switch (Oper1)
            {
                case "+":
                    if (Oper2 == "+")
                        return true;
                    else if (Oper2 == "-")
                        return true;
                    else if (Oper2 == "*")
                        return false;
                    else if (Oper2 == "/")
                        return false;
                    else if (Oper2 == "(")
                        return false;
                    else if (Oper2 == "IM")
                        return false;

                    break;
                case "-":
                    if (Oper2 == "+")
                        return true;
                    else if (Oper2 == "-")
                        return true;
                    else if (Oper2 == "*")
                        return false;
                    else if (Oper2 == "/")
                        return false;
                    else if (Oper2 == "(")
                        return false;
                    break;
                case "*":
                    if (Oper2 == "+")
                        return true;
                    else if (Oper2 == "-")
                        return true;
                    else if (Oper2 == "*")
                        return true;
                    else if (Oper2 == "/")
                        return false;
                    else if (Oper2 == "(")
                        return false;
                    break;
                case "/":
                    if (Oper2 == "+")
                        return true;
                    else if (Oper2 == "-")
                        return true;
                    else if (Oper2 == "*")
                        return true;
                    else if (Oper2 == "/")
                        return true;
                    else if (Oper2 == "(")
                        return false;
                    break;
                case "IM":
                    if (Oper2 == "*")
                        return true;
                    else if (Oper2 == "/")
                        return true;
                    else if (Oper2 == "*")
                        return true;
                    else if (Oper2 == "(")
                        return false;
                    else if (Oper2 == "+")
                        return true;
                    else if (Oper2 == "-")
                        return true;
                    break;
                case "(":
                    if (Oper2 == "sin")
                        return true;
                    else if (Oper2 == "cos")
                        return true;
                    else if (Oper2 == "tan")
                        return true;
                    break;
                case ")":
                    if (Oper2 == "IM")
                        return true;
                    break;
                case "^":
                    /* if(Oper2 == "(")
                        return true;    */
                    return true;
                case "%":
                    if (Oper2 == "(")
                        return true;
                    return false;
                case "sqrt":
                    if (Oper2 == "(")
                        return false;
                    return true;
                case "sin":
                    if (Oper2 == "(")
                        return false;
                    return true;
                case "cos":
                    if (Oper2 == "(")
                        return false;
                    return true;
                case "tan":
                    if (Oper2 == "(")
                        return false;
                    return true;
                case "log":
                    if(Oper2 == "(")
                    return false;
                    return true;
                case "ln":
                    if (Oper2 == "(")
                        return false;
                    return true;
            }
            return false;
        }
        public bool IsAlpha(char ch)
        {
            if (ch == '.')
                return false;
            if ((ch <= '9') && (ch >= '0'))
                return false;
            else
                return true;
        }
        public bool IsDigit(char ch)
        {
            if ((ch <= '9') && (ch >= '0'))
                return true;
            else if (ch == '.')
                return true;
            else
                return false;

        }
        public string FindOpType(string Op)
        {
            switch (Op)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "IM":
                    return "Binary";
                case "sin":
                case "cos":
                case "tan":
                case "sinh":
                case "cosh":
                case "tanh":
                case "log":
                case "ln":
                case "%":
                case "sqrt":
               
                    return "Unary";

            }
            return "INVALID";
        }
        public double ResolveConst(string Const)
        {
            switch (Const)
            { 
                case "PI":
                    return 3.1415926535897932384626433832795;
                case "g":
                    return 9.8;
                case "e":
                    return 3.718281828;
                case "Ans":
                    return PrevAns;
                case "Mr":
                    return this.Mem;
               
            }
            return -0.1234;
        }
        public double Calculate(double Data1, string Operator, double Data2)
        {
            switch (Operator)
            {
                case "+":
                    return Data1 + Data2;
                case "-":
                    return Data1 - Data2;
                case "*":
                    return Data1 * Data2;
                case "/":
                    return Data1 / Data2;
                case "^":
                    return Math.Pow(Data1, Data2);
                case "IM":
                    return Data1 * Data2;
            }
            return -1.234;
        }
        public double Calculate(double Data1, string Operator)
        {
            double ConvData1 = 0.0;

            if (CurDRG == "D")
                ConvData1 = Data1 * 3.1415926535897932384626433832795 / 180;
            else if (CurDRG == "G")
                ConvData1 = Data1 * 3.1415926535897932384626433832795 / 200;

            switch (Operator)
            {
                 case "sin":
                    return Math.Sin(ConvData1);
                case "cos":
                    return Math.Cos(ConvData1);
                case "tan":
                    return Math.Tan(ConvData1);
                case "sinh":
                    return Math.Sinh(Data1);
                case "cosh":
                    return Math.Cosh(Data1);
                case "tanh":
                    return Math.Tanh(Data1);
                case "sqrt":
                    return Math.Sqrt(Data1);
                case "log":
                    return Math.Log10(Data1);
                case "ln":
                    return Math.Log(Data1);

            }
            return -0.1234;
        }

    }

    class ExperItem
    {
        public string data;
        public string type;
        public string opType;

        public string Data
        {
            get { return data; }
            set { data = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string OpType
        {
            get { return opType; }
            set { opType = value; }
        }

    }
    class Stack
    {
        private int StackSize = 100;
        private string[] item = new string[100];
        private int Top;

        public Stack()
        {
            Top = 0;
        }
        public string TopItem
        {

            get
            {
                if (IsEmpty())
                    return Convert.ToString(0.123);
                else
                    return item[Top - 1];
            }
        }
        public int Size
        {
            get { return Top; }
        }
        public bool IsEmpty()
        {
            if (Top <= 0)
                return true;
            else
                return false;
        }
        public bool IsFull()
        {
            if (Top >= StackSize)
                return true;
            else
                return false;
        }
        public bool Push(string x)
        {
            if (IsFull())
                return false;
            item[Top++] = x;
            return true;

        }
        public string Pop()
        {
            if (IsEmpty())
                return "Error<Empty Stack>";
            return (item[--Top]);
        }
    }
    class Queue
    {
        private string[] Item = new string[100];
        private int QueueSize = 100;
        private int bot;
        private int top;

        public Queue()
        {
            bot = 0;
            top = 0;
        }
        public bool IsFull()
        {
            if ((top + 1) % QueueSize == 0)
                return true;
            else
                return false;
        }
        public bool IsEmpty()
        {
            if (top == bot)
                return true;
            else
                return false;
        }

        public bool Insert(string x)
        {
            if (IsFull())
                return false;
            Item[top++] = x;
            return true;
        }
        public string Fetch()
        {
            if (IsEmpty())
                return "Error<Empty Queue>";
            return (Item[bot++]); //fetch from the bottom.
        }

    }
    public class SavedItem
    {
        public string Data;
        public int SN;
        public SavedItem()
        {
            SN = 0;
            Data = "BLANK";

        }
   }
    public class Memory
    {
        public SavedItem[] MemData = new SavedItem[100];
        int ItemCount;
        string LastAccessed;
        string LastItem;
        public Memory()
        {
            for (int i = 0; i < 100; i++)
                MemData[i] = new SavedItem();
            ItemCount = 0;
            LastAccessed = "(A+B)*(A+B)";
        }
        public string DefaultFormula
        {
            get { return LastAccessed; }
        }

        public bool Populate()
        {
            string MemLine = "";
            string SN = "00";
            string Data = "00";
            int i;
            TextWriter twr = new StreamWriter("mydata.txt", true);
            twr.Close();
            TextReader rdr = new StreamReader("mydata.txt");
            for (i = 0; i <99; i++)
            {
                MemLine = rdr.ReadLine();
                if (MemLine == "ENDFORMULA")
                    break;
                SN = MemLine.Substring(0, 2);
                Data = MemLine.Substring(2);
                MemData[i].SN = Convert.ToInt32(SN);
                MemData[i].Data = Data;
            }
            ItemCount = --i;
            LastItem = MemData[i].Data;
            for (;i < 100; i++)
            {
                MemData[i].SN = i;
                MemData[i].Data = "BLANK";
            }
            rdr.Close();
            return true;
        }
        public bool SaveData(string Data)
        {
            MemData[ItemCount + 1].SN = ItemCount + 1;
            MemData[ItemCount++].Data = Data;
            LastItem = Data;
            return SaveMem();

        }
        public string Fetch(int SN)
        {
            LastAccessed = MemData[SN].Data;
            return MemData[SN].Data;
        }
        private bool SaveMem()
        {
            string MemLine = "";
            TextWriter twr = new StreamWriter("mydata.txt");
            int i;
            for(i=0;i<100;i++)
            {
                MemLine = Convert.ToString(MemData[i].SN)+MemData[i].Data;
                twr.WriteLine(MemLine);    
            }
            twr.Close();
            return true;
        }
    }
}
