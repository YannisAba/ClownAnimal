using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace clownAnimal
{
    public partial class Form3 : Form
    {
        List<myPlayer> players = DataClass.LoadPlayers();

        int var_hard;
        int var_easy;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            List<myPlayer> topPlayersByScore = players.OrderByDescending(p => p.Score).ToList();

            foreach (myPlayer player in topPlayersByScore)
            {
                if (player.Difficulty == "Hard")
                {
                    var_hard += 1;
                    listBox4.Items.Add(player.Score);
                    listBox3.Items.Add(player.Name);
                    if (var_hard == 10) break;
                }
            }
            foreach (myPlayer player in topPlayersByScore)
            {
                if (player.Difficulty == "Easy")
                {
                    var_easy += 1;
                    listBox2.Items.Add(player.Score);
                    listBox1.Items.Add(player.Name);
                    if (var_easy == 10) break;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
