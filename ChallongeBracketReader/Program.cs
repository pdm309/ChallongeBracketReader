using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;
namespace ChallongeBracketReader
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Write the challonge bracket url in full: \n>");
            //string url = Console.ReadLine();
            //check url for errors such as not having challonge or .com or maybe even http(s) and www
            Console.WriteLine("Enter challonge tournament apikey: \n");
            string api = Console.ReadLine();

            ChallongePortal challongePortal = new ChallongePortal(api);
            IEnumerable<Tournament> tournaments = challongePortal.GetTournaments();
            foreach (Tournament tournament in tournaments)
            {
                Console.WriteLine();
                Console.WriteLine(tournament.Name);
            }
            Console.WriteLine("Which tournament would you like to see match reports of?");
            string tourneyChoice = Console.ReadLine();

            Console.WriteLine("Is there a specific player you'd like results of? Please write a name/tag or NO");
            string playerChoice = Console.ReadLine();

            Tournament tourney = ValidateTournament(tourneyChoice, tournaments);
            IEnumerable<Match> matches;
            if (tourney!=null)
            {
                matches = challongePortal.GetMatches(tourney.Id);
                IEnumerable<Participant> participants = challongePortal.GetParticipants(tourney.Id);
                foreach (Match match in matches)
                {
                    int[] score = new int[2];

                    string player1 = "";
                    string player2 = "";
                    Participant part1 = new Participant();
                    Participant part2 = new Participant();
                    bool player1Won = false;
                    foreach (Participant participant in participants)
                    {
                        if (participant.Id == match.Player1Id)
                        {
                            player1 = participant.NameOrUsername;
                            part1 = participant;
                            if (match.WinnerId == match.Player1Id)
                            {
                                player1Won = true;
                            }
                        }
                        else if (participant.Id == match.Player2Id)
                        {
                            part2 = participant;
                            player2 = participant.NameOrUsername;
//                            if (player2 == null || player2 == "")
//                            {
//                                player2 = participant.ChallongeUsername;
//                            }

                        }
                    }

                    if (player1Won)
                    {
                        challongePortal.ReportMatchWinner(tourney.Id, match.Id, part1.Id, score);
                        if (player1.Contains(playerChoice) || player2.Contains(playerChoice) || playerChoice == "" || playerChoice.ToUpper() == "NO")
                        {
                            Console.WriteLine("\nRound " + match.Round + ": " + player1 + " defeated " + player2);// + " in a score " + score.ToString());
                        }
                    }
                    else
                    {
                        challongePortal.ReportMatchWinner(tourney.Id, match.Id, part2.Id, score);
                        if (player1.Contains(playerChoice) || player2.Contains(playerChoice) || playerChoice == "" || playerChoice.ToUpper() == "NO")
                        {
                            Console.WriteLine("\nRound " + match.Round + ": " + player2 + " defeated " + player1);
                                // + " in a score " + score.ToString());
                        }
                    }
                    
                }
                //Console.ReadLine();
            }
            else
            {
                Console.WriteLine("INVALID TOURNAMENT NAME");
                Console.ReadLine();
                return;
            }
            
            
        }

        static Tournament ValidateTournament(string tourneyChoice, IEnumerable<Tournament> tournaments)
        {
            foreach (Tournament tournament in tournaments)
            {
                if (tourneyChoice == tournament.Name)
                {
                    return tournament;
                }
            }
            return null;
        }
        
        static async Task RunAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.challonge.com/v1/");
                //"https://PaulDSSB:FVkjpMlzZ3NUcPtc8WKLnRkBfa0PAdhdkmsAAb5c@api.challonge.com/v1/"
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/challonge");
                if (response.IsSuccessStatusCode)
                {
                    Product product = await response.Content.ReadAsAsync<Product>();
                    Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
                }
                else
                {
                    Console.WriteLine("\noops, something went wrong");
                    Console.ReadLine();
                }
                // HTTP POST
                var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
                response = await client.PostAsJsonAsync("api/products", gizmo);
                if (response.IsSuccessStatusCode)
                {
                    Uri gizmoUrl = response.Headers.Location;

                    // HTTP PUT
                    gizmo.Price = 80;   // Update price
                    response = await client.PutAsJsonAsync(gizmoUrl, gizmo);

                    // HTTP DELETE
                    response = await client.DeleteAsync(gizmoUrl);
                }
            }


        }
    }
}
