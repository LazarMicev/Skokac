using Skocko;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Training.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Skocko1
{
    public partial class Form1 : Form
    {
        public int TimeLeft { get; set; } = 60;

        List<MyButton> buttons = new List<MyButton>();
        List<MyButton> result = new List<MyButton>();
        List<myCircle> circles = new List<myCircle>();

        List<int> combination = new List<int>();
        List<int> guesses = new List<int>();

        int counter = 0;
        int correct = 0;
        int semiCorrect = 0;        
        int whichCircle = 0;

        int total = 0;
        int countGuess = 0;
        int countCombination = 0;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void myButton37_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void myButton38_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
       
        public Form1()
        {
            InitializeComponent();
            foreach (MyButton button in flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.BackgroundImageLayout = ImageLayout.Zoom;
                buttons.Add(button);
            }
            foreach (MyButton button in flowLayoutPanel2.Controls.OfType<Button>())
            {
                button.BackgroundImageLayout = ImageLayout.Zoom;
                result.Add(button);
            }
            foreach (myCircle button in panel3.Controls.OfType<myCircle>())
            {
                button.BackgroundImageLayout = ImageLayout.Zoom;
                circles.Add(button);
            }
            circles.Reverse();
            crateWiningCombination();

            foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
            {
                button.Enabled = false;
            }

            label1.Text = "01:00";
        }
       
        private void crateWiningCombination()
        {
            Random random = new Random();
            for(int i = 0; i < 4; i++)
            {
                combination.Add(random.Next(1,7));
            }
        }

        private void addNewGuess(int num)
        {
            guesses.Add(num);
            switch (num)
            {
                case 1:
                    buttons[counter].BackgroundImage = Properties.Resources.icona1;
                    break;
                case 2:
                    buttons[counter].BackgroundImage = Properties.Resources._1;
                    break;
                case 3:
                    buttons[counter].BackgroundImage = Properties.Resources._2;
                    break;
                case 4:
                    buttons[counter].BackgroundImage = Properties.Resources._4;
                    break;
                case 5:
                    buttons[counter].BackgroundImage = Properties.Resources._3;
                    break;
                case 6:
                    buttons[counter].BackgroundImage = Properties.Resources._6;
                    break;
                default:
                    buttons[counter].BackgroundImage = Properties.Resources.icona1;
                    break;
            }
            counter++;

            if (counter % 4 == 0)
            {
                checkResult();
                guesses.Clear();
                int j = 0;
                for (int i = whichCircle;i < whichCircle+4; i++)
                {                    
                    if(j < correct)
                    {
                        circles[i].ColorInside = Color.Red;
                        j++;
                        continue;
                    }
                    if(j < correct + semiCorrect)
                    {
                        circles[i].ColorInside = Color.Orange;
                        j++;
                        continue;
                    }
                }
                whichCircle = whichCircle+4;
            }
            if (counter >= 24 || correct == 4)
            {
                timer1.Stop();
                counter = 0;
                showAnswers();
                restartGameButton.Text = "NEW GAME";
                if (correct == 4 && TimeLeft > 0)
                {
                    
                    MessageBox.Show(this, "Congratulations you have correctly guessed the combination!", "Information", MessageBoxButtons.OK);
                    
                }
                else
                {
                    MessageBox.Show(this,"You haven't guessed the combination, start a NEW GAME!", "Information", MessageBoxButtons.OK);
                }
                correct = 0;
                foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
                {
                    button.Enabled = false;
                }
                   
            }
            if (TimeLeft <= 0)
            {
                correct = 0;
                foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
                {
                    button.Enabled = false;
                }
            }
        }
               

        private void myButton29_Click(object sender, EventArgs e)
        {
            addNewGuess(1);            
        }

        private void myButton30_Click(object sender, EventArgs e)
        {
            addNewGuess(2);
        }

        private void myButton31_Click(object sender, EventArgs e)
        {
            addNewGuess(3);
        }

        private void myButton32_Click(object sender, EventArgs e)
        {
            addNewGuess(4);
        }

        private void myButton33_Click(object sender, EventArgs e)
        {
            addNewGuess(5);
        }

        private void myButton34_Click(object sender, EventArgs e)
        {
            addNewGuess(6);
        }


        private void checkApe(int z)
        {
            countCombination = 0;
            countGuess = 0;
            foreach (int i in guesses)
            {
                if (i == z)
                {
                    countGuess++;
                }
            }
            foreach (int i in combination)
            {
                if (i == z)
                {
                    countCombination++;
                }
            }
            if (countGuess > countCombination)
            {
                total += countCombination;
            }
            else
            {
                total += countGuess;
            }
        }

        private void checkResult()
        {
            correct = 0;
            countGuess = 0;
            countCombination = 0;
            total = 0;
            for (int j = 1; j < 7; j++)
            {
                checkApe(j);
            }
            for (int i = 0; i < 4; i++)
            {

                if (guesses[i] == combination[i])
                {

                    correct++;
                }
            }
            semiCorrect = total - correct;
        }

        private void myButton35_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            TimeLeft = 0;
            showAnswers();
            foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
            {
                button.Enabled = false;
            }
            
        }
        
        private void hideAnswer()
        {
            foreach(MyButton btn in result)
            {                
                btn.BackgroundImage = null;
                btn.Text = "?";
            }
        }

        private void restartGameButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            TimeLeft = 59;
            combination.Clear();
            whichCircle = 0;
            counter = 0;
            crateWiningCombination();
            hideAnswer();
            foreach (myCircle crl in circles)
            {
                crl.ColorInside = Color.White;
            }
            foreach (MyButton btn in buttons)
            {
                btn.BackgroundImage = null;
            }
            foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
            {
                button.Enabled = false;
                
            }
            restartGameButton.Text = "RESTART GAME";
            progressBar1.Value = TimeLeft;
            Invalidate();
            
        }

        private void showAnswers()
        {
            int counter1 = 0;
            foreach (int i in combination)
            {
                switch (i)
                {
                    case 1:
                        result[counter1].BackgroundImage = Properties.Resources.icona1;

                        break;
                    case 2:
                        result[counter1].BackgroundImage = Properties.Resources._1;

                        break;
                    case 3:
                        result[counter1].BackgroundImage = Properties.Resources._2;
                        break;
                    case 4:
                        result[counter1].BackgroundImage = Properties.Resources._4;
                        break;
                    case 5:
                        result[counter1].BackgroundImage = Properties.Resources._3;
                        break;
                    case 6:
                        result[counter1].BackgroundImage = Properties.Resources._6;
                        break;
                    default:
                        result[counter1].BackgroundImage = Properties.Resources.icona1;
                        break;
                }
                result[counter1].Text = "";
                counter1++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (TimeLeft > 0)
            {
                TimeLeft--;
            }
            else
            {
                timer1.Stop();
                counter = 0;
                showAnswers();
                restartGameButton.Text = "NEW GAME";
                if (correct == 4)
                {
                    MessageBox.Show(this, "Congratulations you have correctly guessed the combination!", "Information", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(this, "You haven't guessed the combination, start a NEW GAME!", "Information", MessageBoxButtons.OK);
                }
                correct = 0;
                foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
                {
                    button.Enabled = false;
                }
            }
            progressBar1.Value = TimeLeft;
            label1.Text = $"{TimeLeft / 60} : {TimeLeft % 60}";
            
        }

        private void myButton36_Click(object sender, EventArgs e)
        {
            if(TimeLeft > 57) {
                timer1.Start();
                foreach (MyButton button in flowLayoutPanel3.Controls.OfType<MyButton>())
                {
                    button.Enabled = true;
                }
            }
            
        }
    }
}
