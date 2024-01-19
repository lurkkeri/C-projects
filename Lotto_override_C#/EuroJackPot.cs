public class EuroJackPot:Lotto
{   //tämä lotto arvotaan väliltä (1-49), 5 + 2, numero ja lisänumero saavat olla samoja 
    //metodi lottonumeroiden arpomiselle
    public override List<int> GenerateLotteryNumbers()
    {
        Random random = new Random();
        List<int> lotteryNumbers = new List<int>();

        while (lotteryNumbers.Count < 5)
        {
            int randomNumber = random.Next(1, 50);
            if (!lotteryNumbers.Contains(randomNumber))
            {
                lotteryNumbers.Add(randomNumber);
            }
        }

        return lotteryNumbers;
    }
    //metodi lisänumeroiden arpomiselle
    public override List<int> GenerateAdditionalLotteryNumbers(List<int>lotteryNumbers)
    {
        Random random = new Random();
        List<int> additionalLotteryNumbers = new List<int>();

        while (additionalLotteryNumbers.Count < 2)
        {
            int randomNumber = random.Next(1, 50);
            if (!additionalLotteryNumbers.Contains(randomNumber))
            {
                additionalLotteryNumbers.Add(randomNumber);
            }
        }

        return additionalLotteryNumbers;
    }
    //metodi käyttäjän numeroiden saamiselle
    public override List<int> GetUserNumbers()
    {   
        List<int> userNumbers = new List<int>();
        int counter = 0;
        Console.Write("\nValitse viisi numeroa väliltä (1-49)");
        while(counter<5)
        {   
            if(counter>0)
            {
                Console.WriteLine($"Olet valinnut {counter} numeroa.");
            }
            string? userInput = Console.ReadLine();

            if(userInput != null && int.TryParse(userInput, out int luckynumber))
            {
                if(luckynumber > 0 && luckynumber<49 && !userNumbers.Contains(luckynumber))
                {
                    userNumbers.Add(luckynumber);
                    counter++;
                }
                else if(luckynumber > 0 && luckynumber<49)
                {
                    Console.WriteLine($"Olet valinnut jo numeron {luckynumber}");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers));
                }
                else
                {
                    Console.WriteLine("Numeron täytyy olla väliltä 1-49");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers));
                }

            }
            else
            {
                Console.WriteLine("Syötteesi on väärin!");
            }
        
        }

        return userNumbers;
    }
    //metodi käyttäjän lisänumeroiden saamiselle
    public override List<int> GetAdditionalUserNumbers(List<int> userNumbers)
    {   
        List<int> additionalUserNumbers = new List<int>();
        int counter = 0;
        Console.Write("\nValitse kaksi lisänumeroa väliltä (1-49)");
        while(counter<2)
        {   
            if(counter>0)
            {
                Console.WriteLine($"Olet valinnut {counter} numeroa.");
            }
            string? userInput = Console.ReadLine();

            if(userInput != null && int.TryParse(userInput, out int luckynumber))
            {
                if(luckynumber > 0 && luckynumber<49 && !additionalUserNumbers.Contains(luckynumber))
                {
                    additionalUserNumbers.Add(luckynumber);
                    counter++;
                }
                else if(luckynumber > 0 && luckynumber<49)
                {
                    Console.WriteLine($"Olet valinnut jo numeron {luckynumber}");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers) +" Lisänumerosi: " + string.Join(", ", additionalUserNumbers));
                }
                else
                {
                    Console.WriteLine("Numeron täytyy olla väliltä 1-49");
                }

            }
            else
            {
                Console.WriteLine("Syötteesi on väärin!");
            }
        
        }

        return additionalUserNumbers;
    }

}