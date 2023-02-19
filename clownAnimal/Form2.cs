using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Form2 : Form
    {
        //Οριζω την χρηση του αντικειμενου player απο την κλαση myPlayer και το δημιουργω
        myPlayer player = new myPlayer();

        //Δημιουργω μια λιστα με στοιχεια απο την κλαση myPlayer με ονομα players
        //και φορτωνω τα στοιχεια που παιρνω απο την μεθοδο της DataClass κλασης
        List<myPlayer> players = DataClass.LoadPlayers();



        //Αρχικοποιω τις μεταβλητες που καθοριζουν την αποσταση που θα διανυσει ο στοχος στο χρονικο διαστημα που αλλαζει θεση
        //οριζοντια και καθετα (στο πλατος και στο μηκος)
        int pokemon_x, pokemon_y;


        //Οριζω και αρχικοποιω:
        int secondsToStart = 3;     //Τα δευτερολεπτα για να αρχισει το παιχνιδι
        int secondsToEnd = 25;      //Τα δευτερολεπτα για να τελειωσει


        int num;        //Μεταβλτητη που θα βοηθησει στον υπολογισμο του score

        //Αρχικοποιω τις μεταβλητες που καθοριζουν την αποσταση που θα διανυσει η φορμα στο χρονικο διαστημα που αλλαζει θεση
        //οριζοντια και καθετα (στο πλατος και στο μηκος)
        int formx, formy;


        public string Names;         //Αρχικοποιω τη μεταβλητη του ονοματος

        public Form2(string Name)
        {
            InitializeComponent();

            //Μεταφερω το ονομα που εδωσε ο χρηστης στο home στη μεταβλητη του ονοματος
            Names = Name;

            //Χρησιμοποιουνται για να αλλαζουν πιο ομαλα τα frames ωστε να ειναι περισσοτερο smooth
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
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

        //Οταν φορτωσει η φορμα
        private void Form2_Load(object sender, EventArgs e)
        {
            //Βαζω στο ονομα του αντικειμενου το ονομα του χρηστη και την δυσκολια που διαλεξε
            player.Name = Names;
            player.Difficulty = "Hard";

            //Εμφανιζω στην οθονη το ονομα του παικτη
            textBox2.Text = player.Name;

            //Βαζω σε μια αλλη λιστα "topPlayersByScore" ταξινομημενα απο το μεγαλυτερο στο μικροτερο ολα τα στοιχεια που βαλαμε στην αρχικη λιστα "players"
            List<myPlayer> topPlayersByScore = players.OrderByDescending(p => p.Score).ToList();

            //Για καθε παικτη στην ταξινομημενη λιστα...
            foreach (myPlayer player in topPlayersByScore)
            {
                //Αν η δυσκολια του παικτη ειναι "Hard"..
                if (player.Difficulty == "Hard")
                {
                    //τοτε μεταφερουμε το πρωτο στοιχειο που θα βρουμε στην ετικετα που θα εμφανιζεται στην οθονη με το υψηλοτερο score
                    label4.Text = player.Score.ToString();
                    break;
                }
            }

            //Αρχικοποιω τις ορισμενες μεταβλητες
            pokemon_x = 11;
            pokemon_y = 12;
            
            formx = 3;
            formy = 3;
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
                button3.Visible= true;

                player.Score = num;     //μεταφερουμε το score στο score του παικτη

                DataClass.AddPlayer(player);        //Καλουμε την μεθοδο της DataClass η οποια προσθετει τα στοιχεια του παικτη που μολις επαιξε, δηλαδη το score, το ονομα και την δυσκολια
            }
        }

        //Οταν κανει click τον στοχο (το pictureBox) υπολογιζει τους ποντους και τους μεταφερει στο αντιστοιχο label
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) > 0 && timer2.Enabled == true)
            {
                num = Convert.ToInt32(textBox1.Text) + 10;      //Υπολογισμος των ποντων
                textBox1.Text = num.ToString();
            }
        }


        //Timer που σε καθε tick ο στοχος κινειται στα ορια του panel της φορμας
        //και καθοριζει το ποσο γρηγορα θα αλλαζει θεση το pictureBox με την χρηση
        //των μεταβλητων pokemon_x και pokemon_y
        //Σε αντιθεση με την form1 εδω κινειται και το panel στα ορια της οθονης του υπολογιστη
        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + pokemon_x, pictureBox1.Location.Y + pokemon_y);       //αλλαζει σε καθε tick η θεση του pictureBox
            if (pictureBox1.Location.X < 0 || pictureBox1.Location.X + pictureBox1.Width > panel1.Width)
            {
                pokemon_x = -pokemon_x;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της φορμας

            }
            if (pictureBox1.Location.Y < 0 || pictureBox1.Location.Y + pictureBox1.Height + 0 > panel1.Height)
            {
                pokemon_y = -pokemon_y;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της φορμας
            }

            this.Location = new Point(this.Location.X + formx, this.Location.Y + formy);        //αλλαζει σε καθε tick η θεση του panel
            if (this.Location.X < 0 || this.Location.X + this.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                formx = -formx;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της οθονης του υπολογιστη
            }
            if (this.Location.Y < 0 || this.Location.Y + this.Height + 0 > Screen.PrimaryScreen.Bounds.Height)
            {
                formy = -formy;     //αλλαγη κατευθυνσης αν πετυχει στα ορια της οθονης του υπολογιστη
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();       //Κλεινει την form2 και επιστρεφει στην home
        }
    }
}
