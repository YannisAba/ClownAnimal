using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace clownAnimal
{
    public partial class Form1 : Form
    {
        //Οριζω την χρηση του αντικειμενου player απο την κλαση myPlayer
        myPlayer player;

        //Δημιουργω μια λιστα με στοιχεια απο την κλαση myPlayer με ονομα players
        //και φορτωνω τα στοιχεια που παιρνω απο την μεθοδο της DataClass κλασης
        List<myPlayer> players = DataClass.LoadPlayers();


        //Μεθοδος που δινει extra 15 ποντους αν πετυχει 5 φορες συνεχομενες χωρις να αστοχησει τον στοχο
        int times;
        public int combo_play()
        {
            if (times == 5)
            {
                if (timer2.Enabled)
                {
                    label6.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    pictureBox2.Visible = true;
                    times = 0;
                    return 15;
                }
                else return 0;
            }
            else
            {
                label6.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                pictureBox2.Visible = false;
                return 0;
            }
        }
        //Τελος μεθοδου


        //Αρχικοποιω τις μεταβλητες που καθοριζουν την αποσταση που θα διανυσει ο στοχος στο χρονικο διαστημα που αλλαζει θεση
        //οριζοντια και καθετα (στο πλατος και στο μηκος)
        int pokemon_x, pokemon_y;

        //Οριζω και αρχικοποιω:
        int secondsToStart = 3;     //Τα δευτερολεπτα για να αρχισει το παιχνιδι
        int secondsToEnd = 25;      //Τα δευτερολεπτα για να τελειωσει

        int num;        //Μεταβλτητη που θα βοηθησει στον υπολογισμο του score
        
        
        public string Names;        //Αρχικοποιω τη μεταβλητη του ονοματος

        public Form1(string Name)
        {
            InitializeComponent();

            //Μεταφερω το ονομα που εδωσε ο χρηστης στο home στη μεταβλητη του ονοματος
            Names = Name;
            
            //Χρησιμοποιουνται για να αλλαζουν πιο ομαλα τα frames ωστε να ειναι περισσοτερο smooth
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        //Οταν φορτωσει η φορμα
        private void Form1_Load(object sender, EventArgs e)
        {
            //Δημιουργω το αντικειμενο player
            player = new myPlayer();

            //Βαζω στο ονομα του αντικειμενου το ονομα του χρηστη και την δυσκολια που διαλεξε
            player.Name = Names;
            player.Difficulty = "Easy";

            //Εμφανιζω στην οθονη το ονομα του παικτη
            textBox2.Text = player.Name;

            //Βαζω σε μια αλλη λιστα "topPlayersByScore" ταξινομημενα απο το μεγαλυτερο στο μικροτερο ολα τα στοιχεια που βαλαμε στην αρχικη λιστα "players"
            List<myPlayer> topPlayersByScore = players.OrderByDescending(p => p.Score).ToList();

            //Για καθε παικτη στην ταξινομημενη λιστα...
            foreach (myPlayer player in topPlayersByScore)
            {
                //Αν η δυσκολια του παικτη ειναι "Easy"...
                if (player.Difficulty == "Easy")
                {
                    //τοτε μεταφερουμε το πρωτο στοιχειο που θα βρουμε στην ετικετα που θα εμφανιζεται στην οθονη με το υψηλοτερο score
                    label4.Text = player.Score.ToString();
                    break;
                }
            }

            //Αρχικοποιω τις ορισμενες μεταβλητες
            pokemon_x = 5;
            pokemon_y = 7;
        }

        //Timer αντιστροφης μετρησης εναρξης παιχνιδιου
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = secondsToStart--.ToString();
            if (Convert.ToInt32(label2.Text) < 1)
            {
                timer1.Stop();
                //Οταν τελειωσει η αντιστροφη μετρηση τα δυο επομενα timers ενεργοποιουνται
                timer2.Enabled = true;
                timer3.Enabled = true;
            }
        }

        //Timer αντιστροφης μετρησης τελος παιχνιδιου
        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = "Game is over in:";
            label2.Text = secondsToEnd--.ToString();
            if (Convert.ToInt32(label2.Text) < 1)
            {
                timer2.Stop();
                timer3.Stop();
                //Οταν τελειωσει η αντιστροφη μετρηση
                label7.Visible = true;
                button3.Visible = true;

                player.Score = num;     //μεταφερουμε το score στο score του παικτη

                DataClass.AddPlayer(player);        //Καλουμε την μεθοδο της DataClass η οποια προσθετει τα στοιχεια του παικτη που μολις επαιξε, δηλαδη το score, το ονομα και την δυσκολια
            }
        }

        //Οταν κανει click τον στοχο (το pictureBox) υπολογιζει τους ποντους και τους μεταφερει στο αντιστοιχο label
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) > 0 && timer2.Enabled == true)
            {
                times += 1;
                num = Convert.ToInt32(textBox1.Text) + 10 + combo_play();       //Υπολογισμος των ποντων και χρηση της μεθοδου combo_play()
                textBox1.Text = num.ToString();
            }
        }

        //Timer που σε καθε tick ο στοχος κινειται στα ορια του panel της φορμας
        //και καθοριζει το ποσο γρηγορα θα αλλαζει θεση το pictureBox με την χρηση
        //των μεταβλητων pokemon_x και pokemon_y
        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + pokemon_x, pictureBox1.Location.Y + pokemon_y);       //αλλαζει σε καθε tick η θεση του pictureBox
            if (pictureBox1.Location.X < 0 || pictureBox1.Location.X + pictureBox1.Width > panel1.Width)
            {
                pokemon_x = -pokemon_x;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της φορμας
            }
            if (pictureBox1.Location.Y < 0 || pictureBox1.Location.Y + pictureBox1.Height + 2 > panel1.Height)
            {
                pokemon_y = -pokemon_y;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της φορμας
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            times = 0;      //Κατα την διαρκεια της πρωτης αντιστροφης μετρησης δεν θελουμε να αλλαξει η μεταβλητη times 
                            //γιατι δεν θα λειτουργει οπως θελουμε η μεθοδος combo_play()
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();       //Κλεινει την form1 και επιστρεφει στην home
        }
    }
}
