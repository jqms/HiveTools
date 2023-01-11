using Console = Colorful.Console;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json.Nodes;
using System.Net.NetworkInformation;

namespace hiveTools
{
    class StatChecker
    {
        public static void print(string str) // lets not talk about this
        {
            Console.WriteLine(str);
        }
        public static void createOption(string option, string description)
        {
            Console.Write("[");
            Console.Write(option, Color.MediumBlue);
            Console.Write("] ");
            Console.WriteLine(description, Color.White);
        }
        public static void getStats(string username, string gamemode)
        {
            // Fix gamemode and username for URL
            username = username.Replace(" ", "%20").ToLower().Trim();
            gamemode = gamemode.ToLower().Trim();
            // Get the stats from the API
            string url = "https://api.playhive.com/v0/game/all/" + gamemode + "/" + username;
            WebClient client = new WebClient();
            string data = client.DownloadString(url);

            Console.WriteLine("Stats for " + username + " in " + gamemode + ":");
            // Parse the JSON
            var json = JObject.Parse(data);
            Console.WriteLine(json);
        }
        // public static void getLeaderboard(string gamemode, bool monthly)
        // {
        //     // Fix gamemode for URL
        //     gamemode = gamemode.ToLower().Trim();
        //     // Get the stats from the API
        //     string url = "";
        //     if (monthly == true)
        //     {
        //         url = "https://api.playhive.com/v0/game/monthly/" + gamemode;
        //         Console.WriteLine(url);
        //     }
        //     else
        //     {
        //         url = "https://api.playhive.com/v0/game/all/" + gamemode;
        //         Console.WriteLine(url);
        //     }
        //     WebClient client = new WebClient();
        //     string data = client.DownloadString(url);
        //     Console.WriteLine(data);

        //     string json = (string)JArray.Parse(data);
        //     print(json);
        // }
        public static void br()
        {
            Console.WriteLine(" ");
        }
        public static void Uptime(string address)
        {
            try
            {
                Ping susPing = new Ping();
                PingReply response = susPing.Send(address, 1000);
                if (response != null)
                {
                    br();
                    Console.Write("Ping: ", Color.Blue);
                    Console.Write(response.RoundtripTime, Color.Green);
                    Console.WriteLine("ms", Color.Green);
                    br();
                    Console.Write("Status: ", Color.Blue); 
                    Console.WriteLine(response.Status, Color.Green);
                    Console.Write("Address: ", Color.Blue); 
                    Console.WriteLine(response.Address, Color.Green);
                }
            }
            catch
            {
                Console.WriteLine("\nUnable to ping!", Color.Red);
            }
            print("\nPress any key to go back.");
            Console.ReadKey();
        }
        public static string selectRegion(string logo)
        {
            Console.Clear();
            Console.WriteLine(logo, Color.DarkRed);
            print("What region are you on? (NA/EU/AS)");
            string region = Console.ReadLine();
            if (region == "NA")
            {
                return "ca.hivebedrock.network";
            }
            else if (region == "EU")
            {
                return "fr.hivebedrock.network";
            }
            else if (region == "AS")
            {
                return "sg.hivebedrock.network";
            }
            else if (region == "AU")
            {
                return "au.hivebedrock.network";
            }
            else
            {
                return "Invalid Region! (NA/EU/AS)";
            }
        }
        static void Main()
        {
        start:
            Console.Title = "Hive Tools";
            Console.Clear();
            string logo = (@" 
 █░█ █ █░█ █▀▀ ▀█▀ █▀█ █▀█ █░░ █▀
 █▀█ █ ▀▄▀ ██▄ ░█░ █▄█ █▄█ █▄▄ ▄█
");
            Console.WriteLine(logo, Color.Maroon);
            createOption("1", "Check Player Stats");
            createOption("2", "Check Leaderboard");
            createOption("3", "Check Ping");
            br();
            createOption("10", "Exit");

            string input = Console.ReadLine();
            if (input == "1")
            {
                // Get the username and gamemode
                Console.Write("Enter a username: ");
                string username = Console.ReadLine();
                if (username == "")
                {
                    Console.WriteLine("Please enter a username: ");
                    Console.ReadLine();
                    return;
                }
                Console.Write("Enter a gamemode: ");
                string gamemode = Console.ReadLine();
                if (gamemode == "")
                {
                    Console.WriteLine("Please enter a gamemode: ");
                    Console.ReadLine();
                    return;
                }
                // Get the stats
                getStats(username, gamemode);
                Console.WriteLine("Press any key to go back.");
                Console.ReadKey();
                goto start;
            }
            if (input == "2")
            {
                print("Leaderboards are currently unsupported due to stupid json errors");
                Console.WriteLine("Press any key to go back.");
                Console.ReadKey();
                goto start;
            }
            if (input == "3")
            {
                Uptime(selectRegion(logo));
                goto start;
            }
            if (input == "10")
            {
                Environment.Exit(0);
            }
            else
            {
                print("Invalid key!");
                Thread.Sleep(1000);
                goto start;
            }
        }
    }
}