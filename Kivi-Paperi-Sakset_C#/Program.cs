using System.Runtime.InteropServices;

//tää toimii, mutta tuntuu vähän kaaottiselta noitten tiedon tallentamisten osalta. 
//lista pelaajista tulostetaan jos pelaaja ei halua enää pelata tai lisätä pelaajia
namespace OLIO_OHJELMOINTI
{
    public class Program
    {
        static void Main()
        {   //tehdään lista pelaajille
            List<Object> objectList = new List<object>();
        
            Console.WriteLine("Tervetuloa pelaamaan kivi-paperi-sakset peliä! Paras kolmesta voittaa\nPaina q lopettaaksesi ohjelman");
            
            bool newGame = true;
            while(newGame)
            {   //luodaan kierros ja voittolaskurit
                int round =1;
                int totalRound = 1;
                int totalWin = 0;
                int totalRoundWin =0;
                //luodaan pelissä tarvittavat objektit
                PC pC = new PC(0,0,0,0);
                Game game = new Game(0,0,0);
                Player player = new Player(0,0,0,0,"",totalRound,totalWin,totalRoundWin);
                //luodaan boolean itse pelisilmukkaa varten
                bool play = true;
                //Kysytään käyttäjältä nimi
                Console.WriteLine("\nAnna nimesi:");
                string? name = Console.ReadLine();
                if(name!= string.Empty && name!=null)
                {   //tallenetaan nimi, lisätään pelaaja listaan
                    player.Name = name;
                    objectList.Add(player);
                }
                else
                {
                    Console.WriteLine("Syötit tyhjän!");
                }            
                while(play)
                {   //tulostetaan kierros ja erä
                    Console.WriteLine($"\nKierros {totalRound}");
                    Console.WriteLine($"Erä {round}");
                    Console.WriteLine("Valitse joko kivi(1), sakset(2), paperi(3)");
                    string? answer = Console.ReadLine();
                    if(answer!=null && answer!=string.Empty)
                    {
                        if(int.TryParse(answer, out int newAnser))
                        {   //viedään valinta tiedot voittolaskimeen
                            if(newAnser>=1 &&newAnser<=3)
                            {   int enemy = pC.SetChoise();
                                if(newAnser == 1 || enemy ==1)
                                {   
                                    game.Rock = 1;
                                }
                                if(newAnser == 2 || enemy ==2)
                                {
                                    game.Scissors = 2;
                                }
                                if(newAnser ==3 || enemy==3)
                                {
                                    game.Paper = 3;

                                }
                                //tulostetaan tiedot mittelöstä
                                Console.WriteLine($"Kivi, Paperi, Sakset!");
                                Console.WriteLine($"{player.Name} valitsi {game.Translator(newAnser)}");
                                Console.WriteLine($"PC valitsi {game.Translator(enemy)}");
                                int result = game.Winner();
                                Console.WriteLine($"Ja erän voittaa {game.Translator(result)}");
                                //jos voittaja oli käyttäjän syöte, niin kasvatetaan voittolaskureita
                                if(result == newAnser)
                                {
                                    Console.WriteLine($"{player.Name} voitti erän!");
                                    player.Win++;
                                    player.TotalWins++;
                                    Console.WriteLine($"Pelaajalla on {player.Win} voitto");
                                }
                                else if(result == enemy)
                                {   //kasvatetaan tietokoneen voittoja
                                    Console.WriteLine("Tietokone voitti erän!");
                                    pC.Win++;
                                    Console.WriteLine($"Tietokoneella on {pC.Win} voitto");
                                }
                                //nollataan arvot
                                game.Rock = 0;
                                game.Scissors = 0;
                                game.Paper = 0;
                
                                round++;
                                //jos jommalla kummalla on enemmän kuin kolme voittoa, julistetaan voittaja
                                if(pC.Win >= 3 || player.Win >=3)
                                {
                                    if(pC.Win>player.Win)
                                    {
                                        Console.WriteLine($"Parempi onni ensi kerralla {player.Name}");
                                        player.TotalRounds = totalRound;
                                   
                                        totalRound++;   
                                       
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Onneksi olkoon, {player.Name}!");
                                       
                                        player.TotalRounds = totalRound;
                                        //kasvatetaan kierrosvoittoja
                                        player.TotalRoundWins = ++totalRoundWin;
                                    
                                        totalRound++;
                                    }
                                    Console.WriteLine("Haluatko pelata uuden kierroksen?k/e");
                                    ConsoleKeyInfo keyInfo2 = Console.ReadKey();
 
                                    if(keyInfo2.Key == ConsoleKey.E)
                                    {
                                        play = false;
                                    }
                                    else
                                    {   //nollataan tiedot, jotta uusi erä voi alkaa tyhjältä pöydältä
                                        pC.Win = 0;
                                        player.Win = 0;
                                        round = 1;
                                    }

                                }

                            }
                            else
                            {
                                Console.WriteLine("numeron täytyy olla väliltä 1-3");
                            }
                        }
                        else if(answer.Equals("q"))
                        {
                            play= false;
                        }
                        else
                        {
                            Console.WriteLine("Et syöttänyt numeroa");
                        }    
                    }
                }
                Console.WriteLine("\nHaluatko luoda uuden pelaajan?k/e");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
 
                if(keyInfo.Key == ConsoleKey.E)
                {
                    newGame = false;
                }
                else
                {
                    play = true;
                }

            } //tulostetaan lista pelaajista
            foreach(Object person in objectList)
            {
                Console.WriteLine($"\n{person}");
            }
        }
    }
}