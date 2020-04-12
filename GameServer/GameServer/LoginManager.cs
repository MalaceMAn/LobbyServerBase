using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
//MY Name space. Don't forget to match yours
namespace LobbyServer
{
    class LoginManager
    {
/*        REQUIRES NuGet Package
 *        
 *        A Crude script to simulate serverside login quickly with data persistence.
        The script will check for an existing text file, and create one if none exists.
        From there anytime a username and password are passed through the check method.
        All entries will be parsed. If no username match is made. A new entry is made in the dictionary, with whatever password was given.
        Any time this happens the dictionary will be re-written to the text file in JSON.
        If a username already exists the password is checked against the matching dictionary entry. 
        Method returns true for first time users and matched password user combo.*/
        
        //Define a quick struct class for handling username and password.
        public class Credentials
        {
            public string username;
            public string password;
            public Credentials(string _username, string _password)
            {
                username = _username;
                password = _password;
            }
        }

        //Dictionary to hold previous and add new username+password Combos.
        public static Dictionary<int, Credentials> userHistory = new Dictionary<int, Credentials>();
        public void TestFunction1()
        {
            int currentUsers = userHistory.Count;
            for(int i = userHistory.Count+1; i <= currentUsers+140000; i++)
            {
                Credentials newCred = new Credentials($"test{i}", $"test{i}");
                userHistory.Add(userHistory.Count+1, newCred);
            }

            SaveDictionary();
        }
        public LoginManager()
        {
            
            int dateTimeStart = DateTime.Now.Millisecond;//Keeping an eye on load times
            if (!File.Exists("userHistory.txt"))//Check for file.
            {
                File.WriteAllText("userHistory.Txt", JsonConvert.SerializeObject(userHistory));
                Console.WriteLine("No user login history to load. Creating new userHistory.txt file.");
                return;//To prevent loading the file we just created which will be empty.
            }
            userHistory = JsonConvert.DeserializeObject<Dictionary<int, Credentials>>(File.ReadAllText("userHistory.txt"));//File existed so we load it into the dictionary.
            int dateTimeEnd = DateTime.Now.Millisecond;//Keeping an eye on load times
            Console.WriteLine($"Loaded login history: {userHistory.Count} entries loaded. in {dateTimeStart -dateTimeEnd} ms.");//Keeping an eye on load times
           //TestFunction1();
        }

        public static void SaveDictionary()
        {
            File.WriteAllText("userHistory.Txt", JsonConvert.SerializeObject(userHistory));
            Console.WriteLine("Saved login history");
        }
        public bool CheckForValidCredentials(string _username, string _password)
        {
            foreach (KeyValuePair<int, Credentials> entry in userHistory)
            {
                if (entry.Value.username == _username)
                {
                    //user name exists check for password match
                    if (entry.Value.password == _password)
                    {
                        //login exists and passwords match.
                        return true;
                    }
                    else
                    {
                        //password did not match existing user. Return login failed.
                        return false;
                    }
                }
            }
            //we reached the end of the loop and did not find a match.
            Credentials newUser = new Credentials(_username, _password);
            userHistory.Add(userHistory.Count + 1, newUser);
            SaveDictionary();
            return true;
        }
    }
}
