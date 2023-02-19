using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace clownAnimal
{
    public class DataClass
    {
        /*[12:35 PM]
        TRIANTAFYLLOS XYDIS*/


        //Η παρακατω μεθοδος φορτωνει τις πληροφοριες ολων των παικτων απο την βαση σε λιστα και την επιστρεφει με το ονομα της μεθοδου
         public static List<myPlayer> LoadPlayers() 
         { 
             using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
             { 
                 var output = cnn.Query<myPlayer>("SELECT * FROM Players", new DynamicParameters());
                 return output.ToList(); 
             } 
         }

        //Η παρακατω μεθοδος προσθετει τις πληροφοριες του παικτη, που μπαινει ως ορισμα στη μεθοδο, στη βαση
         public static void AddPlayer(myPlayer player)
         { 
             using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) 
            { 
                cnn.Execute("INSERT INTO Players(name, difficulty, score) values (@Name,@Difficulty,@Score)", player); 
            } 
         }

        //Η παρακατω μεθοδος παιρνει ως ορισμα το connection string που εχουμε ορισει στο App.configuration και χρησιμοποιειται στις
        //παραπανω μεθοδους ωστε να παρουμε τα δεδομενα απο τη βαση
         private static string LoadConnectionString(string id = "Default" )
         {
             return ConfigurationManager.ConnectionStrings[id].ConnectionString; //gets the connection string        
         }
    }
}
