using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Home : Form
    {
        //Οριζω τη μεταβλτητη s που θα χρησιμοποιω για τον ηχο
        private SoundPlayer s;

        //Οριζω τη μεταβλητη για το ονομα που θα δωσει ο χρηστης
        private string pName;


        public Home()
        {
            InitializeComponent();
        }

        //Αρχικοποιω την μεταβλητη του ηχου σε αυτη την μεθοδο
        private void InitializeSound2()
        {
            s = new SoundPlayer("Pokemon Black⧸White Music - Pokemon Center.wav");
        }

        //Aρχη του intro του παιχνιδιου
       private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Visible = false;
            label2.Visible = true;
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = false;
            label3.Visible = true;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Visible = false;
            label4.Visible = true;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Visible = false;
            label5.Visible = true;
            toolTip1.Active = true;
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = true;
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = true;
        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            label7.Visible=false;
            textBox1.Visible = true;
            toolTip1.Active = false;
        }
        //Τελος του intro


        /* Οταν ο χρηστης παταει πανω στο textbox για να βαλει το ονομα 
         * θα σβηνεται το τι υπαρχει ηδη μεσα ωστε να μην
         * χρειαζεται να σβησει ο ιδιος την οδηγια: "(type your name)"
         */
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }


        //Χρησιμοποιω αυτο το event ωστε να μπορει να μην μπορει να δωσει ως ονομα
        //το "(type your name)". Για να λειτουργησει το event θα πρεπει να πατησει πανω στο textBox
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Δεχεται το ονομα οταν πατησει enter 
            if(e.KeyCode == Keys.Enter)
            {
                
                pName = textBox1.Text;      //Βαζει στην μεταβλητη του ονοματoς το περιεχομενο του textBox
                textBox1.Visible=false;     //To textBox δεν θελουμε να φαινεται στη συνεχεια, οποτε το κανουμε μη ορατο
                label8.Text = "Welcome " + pName + "!";     //Καλωσοριζουμε τον χρηστη
                label8.Visible=true;
                toolTip1.Active=true;
            }
        }

        //Αφου παταει δυο φορες στο label μεταφερεται στο κεντρικο μενου
        private void label8_DoubleClick(object sender, EventArgs e)
        {
            toolTip1.Active = false;
            label8.Visible=false;
            InitializeSound2();     //Χρησιμοποιω τη συναρτηση για τον ηχο
            s.PlayLooping();        //Για να μην σταματησει ο ηχος κατα την διαρκεια του παιχνιδιου

            //Κανω ορατα ολα τα labels του κεντρικου μενου

            startButton.Visible = true;     
            infoButton.Visible = true;
            personalButton.Visible = true;
            leaveButton.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
        }

        //Κανει check την αντιστοιχη δυσκολια που θελει
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Οταν κανει check το ενα κανει για σιγουρια uncheck το αλλο ωστε
            //να μην ειναι και τα δυο checked ταυτοχρονα
            checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        //Παταει το κουμπι για να παιξει
        private void startButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)      //Αν εχει επιλεξει την easy δυσκολια
            {
                s.Stop();       //σταματαει η μουσικη
                this.Hide();        //κρυβει το home
                Form1 f1 = new Form1(pName);        //Ανοιγει το form1 και περναει ως παραμετρο το ονομα του χρηστη
                f1.ShowDialog();
                this.Show();        //Αφου κλεσει το form1 εμφανιζει παλι το home
                s.Play();       //παιζει ο ηχος παλι
            }
            else if (checkBox2.Checked)     //Αν εχει επιλεξει την hard δυσκολια γινονται τα αντιστοιχα πραγματα
            {
                s.Stop();
                this.Hide();
                Form2 f2 = new Form2(pName);
                f2.ShowDialog();
                this.Show();
                s.Play();
            }
            else
            {       //Αν δεν εχει επιλεξει καποιο checkBox τοτε εμφανιζει σχετικο μηνυμα κρυβοντας τα υπολοιπα
                label9.Visible = true;
                startButton.Visible = false;
                infoButton.Visible = false;
                personalButton.Visible = false;
                leaveButton.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                toolTip2.Active = true;
                toolTip1.Active = true;
            }
        }

        //Αφου παταει δυο φορες στο label μεταφερεται στο κεντρικο μενου
        private void label9_DoubleClick(object sender, EventArgs e)
        {
            label9.Visible = false;
            startButton.Visible = true;
            infoButton.Visible = true;
            personalButton.Visible = true;
            leaveButton.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            toolTip2.Active = false;
            toolTip1.Active = false;
        }

        //Παταει το κουμπι για να δει τα καλυτερα scores του παιχινδιου
        private void infoButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
            this.Show();
        }

        //Παταει το κουμπι για να δει τα καλυτερα scores που εχει κανει ο ιδιος
        private void personalButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4(pName);        //Χρειαζεται η παραμετρος με το ονομα γιατι θα χρησιμοποιηθει στην form4
            f4.ShowDialog();
            this.Show();
        }

        //Οταν φορτωσει η φορμα
        private void Home_Load(object sender, EventArgs e)
        {
            //Βαζω ως parent των ετικετων το pictureBox1
            //γιατι to pictureBox ειναι background και οχι η ιδια η φορμα...
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;

            //... αυτο το κανω ωστε το color των ετικετων να ειναι transparent με
            //βαση το pictureBox ωστε να φαινονται μονο τα γραμματα των ετικετων
            //και οχι το πλαισιο (Για να φαινεται πιο ωραιο)

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;

            //Το ιδιο
            checkBox1.Parent = pictureBox1;
            checkBox2.Parent = pictureBox1;

            checkBox1.BackColor = Color.Transparent;
            checkBox2.BackColor = Color.Transparent;
        }

        //Παταει το κουμπι για να το κλεισει
        private void leaveButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
