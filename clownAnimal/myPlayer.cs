using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clownAnimal
{
    //Η κλαση myPlayer παιρνει και θετει το ονομα, τη δυσκολια και το score καθε παικτη αντικειμενου

    public class myPlayer
    {
        public string Name { get; set; }

        public string Difficulty { get; set; }
        public int Score { get; set; }

        //default constructor
        public myPlayer() { }  



    }
}
