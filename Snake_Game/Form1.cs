using System.Reflection.Emit;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        int xallar = 0;
        int istiqamət = 0;
        List<PictureBox> beden = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Oyundan çıxış edilsinmi?", "Sual", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Click += pictureBox1_Click;
            pictureBox2.Click += pictureBox2_Click;
            pictureBox3.Click += pictureBox3_Click;
            pictureBox4.Click += pictureBox4_Click;

            YeniOyun();
        }
        private void YeniOyun()
        {
            timer1.Stop();
            xallar = 0;
            label2.Text = "0";
            istiqamət = 0;

            for (int i = 1; i < beden.Count; i++)
            {
                panel1.Controls.Remove(beden[i]);
            }
            beden.Clear();
            beden.Add(snake);

            snake.Size = new Size(20, 20);
            snake.BackColor = Color.Green;
            snake.Location = new Point(panel1.Width / 2, panel1.Height / 2);

            YemYarat();
            timer1.Start();
        }
        private void YemYarat()
        {
            Random r = new Random();
            int x = r.Next(0, panel1.Width - food.Width);
            int y = r.Next(0, panel1.Height - food.Height);
            food.Location = new Point(x, y);
            food.BringToFront();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = beden.Count - 1; i > 0; i--)
            {
                beden[i].Location = beden[i - 1].Location;
            }

            Point p = snake.Location;

            if (istiqamət == 1) p.Y -= 20;
            else if (istiqamət == 2) p.Y += 20;
            else if (istiqamət == 3) p.X -= 20;
            else if (istiqamət == 4) p.X += 20;

            snake.Location = p;

            if (snake.Left < 0 || snake.Top < 0 ||
                snake.Right > panel1.Width || snake.Bottom > panel1.Height)
            {
                timer1.Stop();
                DialogResult d = MessageBox.Show("GAME OVER", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (d == DialogResult.OK)
                {
                    YeniOyun();
                }
                return;
            }

            if (snake.Bounds.IntersectsWith(food.Bounds))
            {
                xallar++;
                label2.Text = xallar.ToString();

                PictureBox yeniHisse = new PictureBox();
                yeniHisse.Size = new Size(20, 20);
                yeniHisse.BackColor = Color.Green;

                yeniHisse.Location = beden[beden.Count - 1].Location;

                panel1.Controls.Add(yeniHisse);
                beden.Add(yeniHisse);

                YemYarat();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (istiqamət != 2) istiqamət = 1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (istiqamət != 1) istiqamət = 2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (istiqamət != 3) istiqamət = 4;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (istiqamət != 4) istiqamət = 3;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
