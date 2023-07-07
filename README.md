**ВИЗУЕЛНО ПРОГРАМИРАЊЕ ПРОЕКТНА ЗАДАЧА**  
**НАСЛОВ НА ТЕМАТА: СКОКАЧ**  

**Вовед**  

Проектот ја илустрира играта Скокач (Skočko на српски) со неколку дополнителни функционалности. Идејата ја добивме од познатиот српски квиз Слагалица, каде со овој проект ќе имате можност да се запознаете со правилата на играта, како и можност за играње.  

**Објаснување на проблемот:**  

Играта е направена на тој начин што корисникот или победува или губи. За да се започне играта потребно е корисникот да кликне на копчето START и притоа е ограничен со време од минута за да ја заврши играта. За таа цел е воведен тајмер каде корисникот преостанатото време може да го следи горе лево. Главната цел на корисникот, а тоа значи и победа, е да се погоди комбинација од 4 елементи (рандом изгенерирани). Има максимум 6 елементи, а тоа се: скокач, детелина, лист, срце, баклава, ѕвезда, и притоа елементите може да се повторуваат и местоположбата е битна. Еден пример за рандом изгенерирана комбинација е: срце, срце, лист, детелина, но оваа комбинација е различна од комбинацијата: срце, детелина, лист, срце (елементите се исти, но местоположбата е различна). Корисникот има 6 можности за да ја погоди комбинацијата и при секој обид (бира 4 елементи) на десната страна добива feedback за тоа колку елементи има погодено и колку од нив се на место. Црвено обоено крукче значи дека корисникот има погодено елемент кој е на точно место, а портокалово обоено крукче значи дека корисникот погодил елемент кој не е на место. Прво се прикажуваат црвените крукчиња, а потоа портокаловите. Во делот со прашалници се крие изгенерираната комбинација. Доколку корисникот кликне на копчето SHOW ANSWER изгенерираната комбинација се прикажува, тајмерот се стопира и копчињата за избор на елемент се disabled. Доколку корисникот го избере копчето RESTART GAME, играта се рестартира, се задава нова комбинација, и со клик на копчето START може корисникот да започне одново да игра. По истекување на времето играта завршува и корисникот ја добива назад изгенерираната комбинација.  

**Решение на проблемот:**  

Целата игра се базира на една форма каде се извршува целосната процедура и позадинска логика, во нашиот случај во Form1. Дополнително користиме две класи за дизајн на копчиња, квадрат (MyButton) и круг (MyCircle) кои наследуваат од класата Button. Дизајнот на Form1 се состои од 4 FlowLayoutPannels кои во себе содржат копчиња од класите MyButton или MyCircle. Целта на користењето на FlowLayoutPannel - от е можноста за поразбирлив код, подобро подредување на елементите и полесно селектирање на параметрите од панелот. Дополнително користиме еден ProgressBar кој го прикажува преостанатото време на корсникот и две копчиња за приказ на резултатот и започување на нова игра.  

**Објаснување на една класа и една функција од изворен код:**  

**Функција AddNewGuess(int num):**  

Целта на функцијата е додавање на нов обид од страна на корисникот од 6 - те можности што ги има. На секој обид (избор на 4 елементи) се проверува со помош на функција checkResult() колку од елементите се на свое место, и колку елементите се погодени, но не се на свое место. Понатаму во зависност од резултатите кружните копчиња во вториот FlowLayoutPanel се обојуваат црвено, портокалово или воопшто не. Откако ќе се проверат резултатите се проверува дали корисникот има уште можности за обид, дали обидот се совпаѓа со генерираната комбинација или пак времето дали истекло. Во првите два случаи, било кој да е исполнет, тајмерот се стопира и се прикажува изгенерираната комбинација со помош на функцијата showAnswers(). Ако корисникот ја погодил комбинацијата добива повратен одговор во форма на MessageBox каде пишува "Congratulations you have correctly guessed the combination!", а во спротивно доколку нема повеќе можности за погодување или доколку тајмерот истекол корисникот добива повратен одговор во форма на MessageBox каде пишува "You haven't guessed the combination, start a NEW GAME!" и копчето за рестартирање добива нов текст: NEW GAME. На крај по истекот на времето или немањето на можности за обид, или по точен погодок се оневозможуваат копчињата за избор на елемент.  

```C#
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
```
**Класа myCircle : Button:**  

Класата содржи две променливи borderColor и colorInside кои имаат свои getteri и setteri, и се properties за button - от. Дополнително ја оverride - нуваме OnPaint() функцијата каде се исцртува копчето и се пополнува внатрешноста со соодветна боја.  
```C#
public class myCircle : Button
    {
        private Color borderColor = Color.Black;
        private Color colorInside = Color.White;

        [Category("A")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Refresh();
            }
        }

        [Category("A")]
        [DefaultValue(typeof(Color), "White")]
        public Color ColorInside
        {
            get { return colorInside; }
            set
            {
                colorInside = value;
                Refresh();
            }
        }

        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectBorder = new RectangleF(1, 1, Width - 2, Height - 2);

            pevent.Graphics.FillEllipse(new SolidBrush(colorInside), rectSurface);
            using (Pen borderPen = new Pen(borderColor, 1))
            {
                pevent.Graphics.DrawEllipse(borderPen, rectBorder);
            }
        }

       
    }
```
**Упатство:**   

1.	Целта на играта е да погодите рандом изгенерирана комбинација од 4 елементи. За избор на елемент кликнете врз посакуваниот елемент од понудените елементи.
2.	Имате 6 можности (обиди) за да ја погодите комбинацијата.
3.	При секој обид, од левата страна, го внесувате својот предлог за комбинација од 4 елементи од 6 можни (скокач, детелина, лист, срце, баклава, ѕвезда). Внесете ги елементите во редослед во кој мислите дека се наоѓаат во изгенерираната комбинација.
4.	По секој внес на комбинација, на десната страна на екранот добивате feedback. Црвено обоените крукчиња ќе ви покажат колку елементи сте погодиле кои се на точното место, додека портокалово обоените крукчиња ќе ви покажат колку елементи сте погодиле кои не се на точното место.
5.	Играта има тајмер од минута. Потрудете се да ја погодите комбинацијата пред истекот на времето. Ако не успеете, компјутерот ќе ја прикаже изгенерираната комбинација во делот со прашалници.
6.	По завршување на играта, можете да започнете нова игра со кликнување на копчето RESTART GAME и кликнување на копчето START за да може да почне да се одбројува време.
7.	Доколку изберете копче SHOW ANSWER изгенерираната комбинација ќе ја добиете во делот кај прашалниците, но играта завршува и немате можност за избор на елемент.
8.	Во моментот кога ќе ја погодите комбинацијата, истата ќе ви биде прикажана во делот со прашалници.

Во продолжение е даден изгледот на играта.  
![image](https://github.com/LazarMicev/Skokac/assets/108904154/ad3ec584-a6e8-4686-93c7-e0cede98f967)  
**Изработилe:  
Јана Јанкоска 216031  
Лазар Мицев 213225**


