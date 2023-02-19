using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Form4 : Form
    {
        List<myPlayer> players = DataClass.LoadPlayers();

        int var_hard;
        int var_easy;

        public string Names;
        public Form4(string Name)
        {
            Names = Name;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            label5.Text = Names;

            List<myPlayer> topPlayersByScore = players.OrderByDescending(p => p.Score).ToList();

            foreach (myPlayer player in topPlayersByScore)
            {
                if (player.Name == Names && player.Difficulty == "Easy")
                {
                    var_easy += 1;
                    listBox1.Items.Add(player.Score);
                    if(var_easy == 4) break;
                }
            }
            foreach (myPlayer player in topPlayersByScore)
            {
                if (player.Name == Names && player.Difficulty == "Hard")
                {
                    var_hard += 1;
                    listBox2.Items.Add(player.Score);
                    if (var_hard == 4) break;
                }
            }
        }
    }
}
