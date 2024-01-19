using System;
using System.Collections.Generic;
using System.Linq;
namespace OLIO_OHJELMOINTI
{  
class Program
{
    static void Main()
    {   
        bool letsPlay = true;
        while(letsPlay)
        {   //esitellään listat muuttujille
            List<int> lotteryNumbers = new List<int>();
            List<int> userNumbers = new List<int>();
            List<int> additionalUserNumbers = new List<int>();
            List<int> additionalLotteryNumbers = new List<int>();

            Console.WriteLine("Tervetuloa lottoon!");
            Console.WriteLine("Valitse lotto: Lotto(1), Eurojackpot(2) vai Vikinglotto(3)?");
            string? answer = Console.ReadLine();
            //tarkistetaan onko vastaus integer tai tyhjä 
            if (int.TryParse(answer, out int newAnswer)&& answer != null && answer != string.Empty )
            {
                if(newAnswer== 1)
                {   //funktiot päälotolle: luodaan ensin random numerot listaan, sitten luodaan käyttäjäsyötteille lista
                    Lotto lotto = new Lotto();
                    lotteryNumbers = lotto.GenerateLotteryNumbers();
                    userNumbers = lotto.GetUserNumbers();
                    additionalUserNumbers = lotto.GetAdditionalUserNumbers(userNumbers);
                    additionalLotteryNumbers = lotto.GenerateAdditionalLotteryNumbers(lotteryNumbers);

                }
                else if(newAnswer == 2)
                {   //funktiot eurojackpotille: luodaan ensin random numerot listaan, sitten luodaan käyttäjäsyötteille lista
                    EuroJackPot euroJackPot = new EuroJackPot();
                    lotteryNumbers = euroJackPot.GenerateLotteryNumbers();
                    userNumbers = euroJackPot.GetUserNumbers();
                    additionalLotteryNumbers = euroJackPot.GenerateAdditionalLotteryNumbers(lotteryNumbers);
                    additionalUserNumbers = euroJackPot.GetAdditionalUserNumbers(userNumbers);
   
                }
                else if(newAnswer == 3)
                {   //funktiot vikinglotolle: luodaan ensin random numerot listaan, sitten luodaan käyttäjäsyötteille lista
                    VikingLotto vikingLotto = new VikingLotto();
                    lotteryNumbers = vikingLotto.GenerateLotteryNumbers();
                    additionalLotteryNumbers = vikingLotto.GenerateAdditionalLotteryNumbers(lotteryNumbers);
                    userNumbers = vikingLotto.GetUserNumbers();
                    additionalUserNumbers = vikingLotto.GetAdditionalUserNumbers(userNumbers);
   

                }
                else
                {
                    Console.WriteLine("Valitse 1, 2 tai 3");
                }
            }//jos listat ei ole tyhjiä voidaan arpoa numerot, varmaan vähän turha tarkistus kyllä, koska syötteet on while loopin sisässä
            if(lotteryNumbers.Count>0 && userNumbers.Count>0 && additionalLotteryNumbers.Count>0 && additionalUserNumbers.Count>0)
            {   //tulostetaan arvotut numerot ja omat valinnat. Kutsutaan vertailu metodeja numeroille sekä lisänumeroille
                Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers) + " Lisänumerosi: "+ string.Join(", ", additionalUserNumbers));
                Console.WriteLine("\nArvotut numerot: " + string.Join(", ", lotteryNumbers)+ " Lisänumerot: "+ string.Join(", ", additionalLotteryNumbers));
                int matchedNumbers = MatchingNumbers(lotteryNumbers, userNumbers);
                int matchedAdditionalNumbers = MatchingAdditionalNumbers(additionalLotteryNumbers,additionalUserNumbers);
                //jos numeroiden määrä on 5 ja lisänumeroiden määrä 2 tai numeroiden määrä on kuusi ja lisänumeroiden määrä 1 mennään katsomaan voittoja
                if(matchedNumbers == 5 && additionalLotteryNumbers.Count == 2 || matchedNumbers == 6 && matchedAdditionalNumbers == 1)
                {   //jos lisänumeroita on enemmän kuin yksi onnitellaan voittajaa ja tulostetaan kuinka monta hän sai oikein
                    if(matchedAdditionalNumbers>1)
                    {
                        Console.WriteLine($"Onneksi olkoon! Voitit {matchedNumbers} + {matchedAdditionalNumbers}");
                    }//tarkistetaan oliko jossain lotossa pääpotti
                    else if(matchedAdditionalNumbers + matchedNumbers == 7 && lotteryNumbers.Count == 5 || matchedAdditionalNumbers + matchedNumbers == 9 && lotteryNumbers.Count == 6)
                    {
                        Console.WriteLine("Onneksi olkoon! Voitit pääpotin!");
                    }    
                }
                else
                {   //jos käyttäjä ei voittanut mitään, tulostetaan monta nunmeroa meni oikein
                    Console.WriteLine($"Sait {matchedNumbers} + {matchedAdditionalNumbers} oikein. Parempi onni ensi kerralla!");
                }

            }//kysytään käyttäjältä haluaako hän pelata uuden loton
            Console.WriteLine("Haluatko pelata uuden loton? (k/e)");
            string ?userInput = Console.ReadLine();
            Console.WriteLine(userInput);
            if(userInput!=null && userInput!= string.Empty)
            {
                userInput = userInput.ToString().ToLower();
                if(userInput.Equals("k"))
                {
                    Console.WriteLine("Jatketaan pelailua!");
                }
                else if(userInput.Equals("e"))
                {
                    Console.WriteLine("Suljetaan ohjelma!");
                    letsPlay = false;
                }
                else
                {
                    Console.WriteLine("Jatketaan pelailua!");
                }
            }

        }
          
    }
    //metodi perusnumeroiden tarkistamiselle
    static int MatchingNumbers(List<int> lotteryNumbers, List<int> userNumbers)
    {
        int matchedNumbers = lotteryNumbers.Intersect(userNumbers).Count();
        return matchedNumbers;
    }
    //metodi lisänumeroiden tarkistamiselle
    static int MatchingAdditionalNumbers(List<int> additionalLotteryNumbers, List<int> additionalUserNumbers)
    {
        int matchedAdditionalNumbers = additionalLotteryNumbers.Intersect(additionalUserNumbers).Count();
        return matchedAdditionalNumbers;
    }
}
}