using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fifteenPuzzle
{
    public partial class Form1 : Form
    {
        Button[] buttons = new Button[15];
        int[] sequence = new int[16];
        Point voidCell = new Point(300, 300);
        int a = 0;
        public Form1()
        {
            InitializeComponent();
            Size = new Size(400 + Size.Width - ClientSize.Width, 400 + Size.Height - ClientSize.Height);
            MinimumSize = Size;
            MaximumSize = Size;
            PictureBox bg = new PictureBox();
            Controls.Add(bg);
            bg.Location = new Point(0, 0);
            bg.Size = new Size(400, 400);
            bg.BackColor = Color.AliceBlue;
        }

        private void buttons_Click(object sender, EventArgs e) {
            for (int i = 0; i < buttons.Length; i++) {
                if (sender.Equals(buttons[i])) {
                    a = i;
                }
            }
            timer1.Start();
        }

        private void initButton(Point location, int i) {
            Button element = new Button();
            Controls.Add(element);
            element.Location = location;
            element.Size = new Size(100, 100);
            element.Text = i.ToString();
            element.Name = "btn" + i.ToString();
            element.Click += new System.EventHandler(buttons_Click);
            element.BringToFront();
            buttons[i - 1] = element;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random r = new Random();
            int[] arr = new int[15];
            for (int i = 0; i < arr.Length; i++)
            {
                bool exit = false;
                do
                {
                    exit = true;
                    arr[i] = r.Next(1, 16);
                    for (int j = 0; j < i; j++) {
                        if (arr[i] == arr[j]) {
                            exit = false;
                        }
                    }
                } while (!exit);               
            }
            int index = 0;
            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++) {
                    if (!(y == 3 && x == 3)) {
                        initButton(new Point(x * 100, y * 100), arr[index]);
                        index++;
                    }                    
                }
            }
            index = 0;
            foreach (int element in arr)
            {
                sequence[index] = element;
                index++;
            }
            sequence[index] = 15;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool up = (buttons[a].Location.X == voidCell.X && buttons[a].Location.Y == voidCell.Y - 100);
            bool down = (buttons[a].Location.X == voidCell.X && buttons[a].Location.Y == voidCell.Y + 100);
            bool right = (buttons[a].Location.X == voidCell.X + 100 && buttons[a].Location.Y == voidCell.Y);
            bool left = (buttons[a].Location.X == voidCell.X - 100 && buttons[a].Location.Y == voidCell.Y);
            if (up || down || right || left) {
                Point temp = buttons[a].Location;
                buttons[a].Location = voidCell;
                voidCell = temp;

                int indexSeq = 0;
                int indexVoid = 0;
                for (int i = 0; i < sequence.Length; i++)
                {
                    if (i == a) {
                        indexSeq = i;
                    }
                    if (i == 15) {
                        indexVoid = i;
                    }
                }

                int tmp = sequence[indexVoid];
                sequence[indexVoid] = sequence[indexSeq];
                sequence[indexSeq] = tmp;
            }
            foreach (int element in sequence)
            {
                Console.WriteLine(element);
            }
            timer1.Stop();            
        }
    }
}
