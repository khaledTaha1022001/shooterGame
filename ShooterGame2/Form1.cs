using System;
using System.Diagnostics;
using Timer = System.Threading.Timer;

namespace ShooterGame2
{
   
    
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer background;
        System.Media.SoundPlayer bom;
        System.Media.SoundPlayer Lose;
        System.Media.SoundPlayer laser;
        String path;
        protected override CreateParams CreateParams {
    get {
        CreateParams cp = base.CreateParams;
        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        return cp;
    }
}
        public class Players {
            String user;
            int score;
            public Players(String u , int sc) {
            user = u;
               score = sc;
            }
            public Players(String u ) { user = u;
                score = 0;
            }
            public Players() { user = "";
                score = 0;

            }

            public String getUser() {
            return user;
            }



        public int getScore() { return score; }
            public void setScore(int s) { score = s; }

        }
        List<Players> pl = new List<Players>();
        String sentUser; 

        protected virtual bool DoubleBuffered { get; set; }

        class bullet
        {

           public System.Windows.Forms.Timer timer;
            
            int i;
            private PictureBox bull;
            public bullet(int x, int y)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 40;
                timer.Tick += new EventHandler(animate);
                bull = new PictureBox
                    {
                        Size = new Size(50, 50),
                        Location = new Point(x+15, y),
                        Image = Properties.Resources.bullet_red,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent

                    };
                }
            public void bulletMove()
            {
                int x=bull.Location.X, y= bull.Location.Y;
                bull.Location = new Point(x, y - 40);

            }
            public void picDis()
            {
                bull.Dispose();
                form.Controls.Remove(bull);
            }

            public PictureBox getPictureBOx() { return bull; }

            public async void animate(object sender, EventArgs e)
            {
                bull.Size = new Size(100, 100);
                bull.Location = new Point(bull.Location.X-5, bull.Location.Y-20);
                i = (i + 1) % 6;
                if (i == 0)
                    bull.Image = Properties.Resources.b1;
                else if (i == 1)
                    bull.Image = Properties.Resources.b2;
                else if (i == 2)
                    bull.Image = Properties.Resources.b3;
                else if (i == 3)
                    bull.Image = Properties.Resources.b4;
                else if (i == 4)
                    bull.Image = Properties.Resources.b6;
                else if (i == 5)
                {
                    bull.Image = Properties.Resources.b7;
                    picDis();
                }
                   







            }


        }


    


            static int height, width;
        int score = 0;
        static Random rnd = new Random();

        public void readUsers()
        {
            String fPath = GetMaterialsDocSource();
            StreamReader sr = new StreamReader(fPath + @"\Results.txt");
            String data;

            while (sr.Peek()!=-1) {
           
                String[] te = sr.ReadLine().Split(" ");
                String u =te[0];
                int result =int.Parse(te[1]);
                pl.Add(new Players(u,result));
            
            }

        }
        class Bomb
        {
            private PictureBox bom;

            public Bomb(int x, int y)
            {
                int i = rnd.Next(1, 3);
                if (i == 1)
                {
                    bom = new PictureBox
                    {
                        Size = new Size(50, 50),
                        Location = new Point(x, y),
                        Image = Properties.Resources.bom1,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent

                    };
                }

                else
                {
                    bom = new PictureBox
                    {
                        Size = new Size(50, 50),
                        Location = new Point(x, y),
                        Image = Properties.Resources.bom2,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BackColor = Color.Transparent
                    };


                }
            }

            public async void bombMove(int step)
            {
                int x = bom.Location.X, y = bom.Location.Y;
                bom.Location = new Point(x, y + step);

            }

            public Point getLocation() { return bom.Location; }

            public PictureBox getPictureBox() { return bom; }


            public void picDis()
            {
                bom.Dispose();
                form.Controls.Remove(bom);
            }

        }

        class Enimies
        {

            PictureBox Enm;
            int i;
            public Enimies()
            {
                i = 0;
                
                Enm = new PictureBox
                {
                    Size = new Size(100, 100),
                    Location = new Point(0, 0),
                    Image = Properties.Resources.Enemy,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent


                };


            }

            public async void animate()
            {
                i = (i + 1) % 9;
                if (i == 0)
                    Enm.Image = Properties.Resources.e0;
                else if (i == 1)
                    Enm.Image = Properties.Resources.e1;
                else if (i == 2)
                    Enm.Image = Properties.Resources.e2;
                else if (i == 3)
                    Enm.Image = Properties.Resources.e3;
                else if (i == 4)
                    Enm.Image = Properties.Resources.e4;
                else if (i == 5)
                    Enm.Image = Properties.Resources.e5;
                else if (i == 6)
                    Enm.Image = Properties.Resources.e6;
                else if (i == 7)
                    Enm.Image= Properties.Resources.e7;
                else if (i == 8)
                    Enm.Image=Properties.Resources.e8;





            }

            public async void enemyMove(int step)
            {
                int x = Enm.Location.X, y = Enm.Location.Y;
                Enm.Location = new Point(x + step, y);


            }


            public PictureBox getPictureBox() { return Enm; }

            public void picDis()
            {
                Enm.Dispose();
                form.Controls.Remove(Enm);
            }


        }

        class Plane {
            PictureBox plane;
            int i;
            public Plane(int width , int height)
            {
                i = 0;

                plane = new PictureBox
                {
                    Size = new Size(100, 100),
                    Location = new Point(width, height),
                    Image = Properties.Resources.a0,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent


                };




            }

            public PictureBox getPictureBox() { return plane; }

            public void planeMoveRight()
            {
                int x = plane.Location.X, y = plane.Location.Y;
                plane.Location = new Point(x + 3, y);
                animate();

            }

            public async void planeMoveLeft()
            {
                int x = plane.Location.X, y = plane.Location.Y;
                plane.Location = new Point(x - 3, y);
                animate();

            }
            public async void animate()
            {
                i = (i + 1) % 9;
                if (i == 0)
                    plane.Image = Properties.Resources.a0;
                else if (i == 1)
                    plane.Image = Properties.Resources.a1;
                else if (i == 2)
                    plane.Image = Properties.Resources.a2;
                else if (i == 3)
                    plane.Image = Properties.Resources.a3;
                else if (i == 4)
                    plane.Image = Properties.Resources.a4;
                else if (i == 5)
                    plane.Image = Properties.Resources.a5;
                else if (i == 6)
                    plane.Image = Properties.Resources.a6;
                else if (i == 7)
                    plane.Image = Properties.Resources.a7;
                else if (i == 8)
                    plane.Image = Properties.Resources.a8;





            }

        }


        private static Form1 form = null;
        


        private string GetMaterialsDocSource()
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            int FirstIndex = str.IndexOf("ShooterGame2");
            string beginString = str.Substring(0, FirstIndex + "ShooterGame2".Length);
            string result = beginString + @"\materials";
            for (int i = 0; i < result.Length; i++)
                if (result[i] == '\\' && result[i - 1] != '\\')
                    result = result.Insert(i, "\\");
            return result;
        }

        public Form1(String u)
        {
            sentUser = u;
            readUsers();
            Players temp = new Players();
            bool found = false;
            foreach (Players p in pl)
                if (p.getUser().Equals( sentUser))
                {
                    temp = new Players(p.getUser(), p.getScore());
                    found = true;
                }

            if (found == false)
            {
                temp = new Players(sentUser, 0);
                pl.Add(temp);
            }


            InitializeComponent();
            path = GetMaterialsDocSource();
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            Image myimage = new Bitmap(Properties.Resources.background);
            this.BackgroundImage = myimage;
            form = this;
            panel1.BackgroundImage = myimage;
            panel1.Height = Screen.PrimaryScreen.WorkingArea.Height;
            //  panel1.Width = form.Width;
            pictureBox1.Height = Screen.PrimaryScreen.WorkingArea.Height;
            height = panel1.Height;
            width = panel1.Width;
            DoubleBuffered = true;
            laser = new System.Media.SoundPlayer(path+ @"\Media\laser.wav");
            background =  new System.Media.SoundPlayer(path + @"\Media\background.wav");
            bom =  new System.Media.SoundPlayer(path + @"\Media\Explosion.wav");
            Lose =  new System.Media.SoundPlayer(path + @"\Media\Lost.wav");
            label2.Location =  new Point(label2.Location.X, Screen.PrimaryScreen.WorkingArea.Height / 2);
            label1.Location = new Point(label1.Location.X,label2.Location.Y-100);
            label3.Location = new Point(label3.Location.X, label2.Location.Y + 100);

            
           

       
            label1.Text = label1.Text + " " +temp.getUser();
            label2.Text = label2.Text + " " + 0;
            label3.Text = label3.Text + " " + temp.getScore();


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            height = panel1.Height;
            width = panel1.Width;



        }
        List<Enimies> enimies = new List<Enimies>();
        List<Bomb> bombs = new List<Bomb>();
        private async void timer1_Tick(object sender, EventArgs e) // adding enimies 
        {
            
            if (rnd.Next(100) % 2 == 0)
            {

                enimies.Add(new Enimies());
                    panel1.Controls.Add(enimies[enimies.Count-1].getPictureBox());


                
            }







        }


        private async void timer2_Tick(object sender, EventArgs e)  // moving the enimes 

        {
            int step = 3;
            if (score > 1000) { // if the score > 1000 the enmies and bombs speed will be increased 
                step = 8;
                timer1.Interval = 2000;
                timer4.Interval = 2000;
            }
              

            foreach (Enimies t in enimies)
            {
                t.enemyMove(step);
                t.animate();
            }


            if (enimies.Count > 1)
            {
                x = enimies[rnd.Next(0, enimies.Count)].getPictureBox().Location.X;
                y = enimies[rnd.Next(0, enimies.Count)].getPictureBox().Location.Y;
            }


            foreach (Bomb b in bombs)
                b.bombMove(step);

            foreach (bullet b in bull)
                b.bulletMove();



        }
        bool planeAdded = false;

        private async void timer3_Tick(object sender, EventArgs e) // adding plane 
        {
            


            if (planeAdded)
                planeAdded = true;
            else
            {
                p = new Plane((panel1.Width/2)-75, panel1.Height - 150);
                panel1.Controls.Add(p.getPictureBox());
                planeAdded = true;
            }
            p.animate();


        }
        Plane p; 
        List<bullet> bull = new List<bullet>();

        int x = 0;
        int y = 0;
        private async void timer4_Tick(object sender, EventArgs e) // to add bombs 
        {
            if (enimies.Count>0)
            {
                x = enimies[rnd.Next(0, enimies.Count)].getPictureBox().Location.X;
                y = enimies[rnd.Next(0, enimies.Count)].getPictureBox().Location.Y;
            }
            else
                return;

           
            if (rnd.Next(100) % 2 == 0)
            {
                bombs.Add(new Bomb(x, y + 100));
                panel1.Controls.Add(bombs[bombs.Count-1].getPictureBox());
            }
                   
                
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (p.getPictureBox().Location.X < panel1.Width - 110 )
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                        p.planeMoveRight();
                        break;

                }
            }
            if (p.getPictureBox().Location.X > 0) {

                switch (e.KeyCode)
                {
                    case Keys.Left:
                        p.planeMoveLeft();
                        break;
                }

                }

            switch (e.KeyCode)
            {
                case Keys.Space:
                    bull.Add(new bullet(p.getPictureBox().Location.X, p.getPictureBox().Location.Y-50));
                    panel1.Controls.Add(bull[bull.Count-1].getPictureBOx());
                    laser.Play();
                    break;

            }

        }

        private void timer5_Tick_1(object sender, EventArgs e)
        {
            if (lost)
                return;
            List<int> rem = new List<int>();
            for(int i = 0; i < bull.Count; i++)
            {

                foreach (Bomb b in bombs)
                {
                    if (bull[i].getPictureBOx().Bounds.IntersectsWith(b.getPictureBox().Bounds))
                    {
                        bom.Play();
                        rem.Add(i);
                        score += 50;
                        label2.Text = "Score : " + score;

                        bull[i].timer.Start();
                        b.picDis();
                        bombs.Remove(b);

                        break;


                    }
                }

                foreach (Enimies se in enimies)
                {
                    if (bull[i].getPictureBOx().Bounds.IntersectsWith(se.getPictureBox().Bounds))
                    {
                        bom.Play();
                        score += 100;
                        label2.Text = "Score : " + score;
                        rem.Add(i);
                        bull[i].timer.Start();
                        se.picDis();
                        enimies.Remove(se);

                        break;

                    }


                }

            

        }
            foreach (int i in rem) {
            bull.RemoveAt(i);
            }
            rem.Clear();


         

        }
        bool lost = false;
        public void Lost()
        {
            String line;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
            timer6.Stop();
            lost = true;
            Lose.Play();
            StreamWriter sw = new StreamWriter(GetMaterialsDocSource()+@"\Results.txt");
            foreach (Players p in pl) {
                if (p.getScore() > score)
                    line = p.getUser() + " " + p.getScore();
                else {
                    p.setScore(score);
                    line = p.getUser() + " " + p.getScore();

                }
                    
                sw.WriteLine(line);
            }
            sw.Close();
            DialogResult res = MessageBox.Show("Want to play again?", "Lost", MessageBoxButtons.YesNo);
            if(res == DialogResult.Yes)
            {
               Application.Restart();
            }else
                Application.Exit();


        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            foreach (Bomb b in bombs)
            {
                if (p.getPictureBox().Bounds.IntersectsWith(b.getPictureBox().Bounds))
                {
                   
                    Lost();
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private async void timer5_Tick(object sender, EventArgs e)
        {
            foreach (Enimies t in enimies)
            {
                if (t.getPictureBox().Location.X > panel1.Width - 110)
                {
                    t.picDis();
                    enimies.Remove(t);
                    break;
                }




            }


            foreach (bullet t in bull)
            {
                if (t.getPictureBOx().Location.Y > 0)
                {
                    t.picDis();
                    bull.Remove(t);
                    break;
                }




            }

        }
    }
}