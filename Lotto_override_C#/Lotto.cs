public class Lotto
{   //päälotto, muut lotot ovat samanlaisia, mutta sisältävät eri logiikat arvonnalle
    //Tämä lotto toimii numeroilla (1-40), lisänumeron pitää olla eri kuin jo valitut numerot, 5 + 2
    //metodi lottonumeroiden arvonnalle
    public virtual List<int> GenerateLotteryNumbers()
    {
        Random random = new Random();
        List<int> lotteryNumbers = new List<int>();

        while (lotteryNumbers.Count < 5)
        {  
            int randomNumber = random.Next(1, 41);
            //lisätään numero listaan, jos se ei ole jo listassa
            if (!lotteryNumbers.Contains(randomNumber))
            {
                lotteryNumbers.Add(randomNumber);
            }
        }

        return lotteryNumbers;
    }
    //metodi lisänumeroiden arvonnalle
    public virtual List<int> GenerateAdditionalLotteryNumbers(List<int>lotteryNumbers)
    {
        Random random = new Random();
        List<int> additionalLotteryNumbers = new List<int>();

        while (additionalLotteryNumbers.Count < 2)
        {
            int randomNumber = random.Next(1, 41);
            if (!additionalLotteryNumbers.Contains(randomNumber)&&!lotteryNumbers.Contains(randomNumber))
            {   //lisätään numero listaan, jos se ei ole jo listassa
                additionalLotteryNumbers.Add(randomNumber);
            }
        }

        return additionalLotteryNumbers;
    }
    //metodi käyttäjän lottonumeroiden saamiselle
    public virtual List<int> GetUserNumbers()
    {   
        List<int> userNumbers = new List<int>();
        int counter = 0;
        Console.Write("\nValitse viisi numeroa väliltä (1-40)");
        while(counter<5)
        {   
            if(counter>0)
            {
                Console.WriteLine($"Olet valinnut {counter} numeroa.");
            }
            string? userInput = Console.ReadLine();

            if(userInput != null && int.TryParse(userInput, out int luckynumber))
            {   //tarkistetaan onko numero jo listassa ja oikealta väliltiä,jos on niin lisätään numero listaan
                if(luckynumber > 0 && luckynumber<40 && !userNumbers.Contains(luckynumber))
                {
                    userNumbers.Add(luckynumber);
                    counter++;
                }
                else if(luckynumber > 0 && luckynumber<40)
                {   //jos numero oli listassa ja oikealta väliltä muistutetaan käyttäjää valituista numeroista
                    Console.WriteLine($"Olet valinnut jo numeron {luckynumber}");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers));
                }
                else
                {
                    Console.WriteLine("Numeron täytyy olla väliltä 1-40");
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
    public virtual List<int> GetAdditionalUserNumbers(List<int> userNumbers)
    {   
        List<int> additionalUserNumbers = new List<int>();
        int counter = 0;
        Console.Write("\nValitse kaksi lisänumeroa väliltä (1-40)");
        while(counter<2)
        {   
            if(counter>0)
            {
                Console.WriteLine($"Olet valinnut {counter} numeroa.");
            }
            string? userInput = Console.ReadLine();

            if(userInput != null && int.TryParse(userInput, out int luckynumber))
            {
                if(luckynumber > 0 && luckynumber<40 && !additionalUserNumbers.Contains(luckynumber)&& !userNumbers.Contains(luckynumber))
                {
                    additionalUserNumbers.Add(luckynumber);
                    counter++;
                }
                else if(luckynumber > 0 && luckynumber<40)
                {
                    Console.WriteLine($"Olet valinnut jo numeron {luckynumber}");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers) + " Lisänumerosi: "+ string.Join(", ", additionalUserNumbers));
                }
                else
                {
                    Console.WriteLine("Numeron täytyy olla väliltä (1-40)");
                    Console.WriteLine("\nLotto numerosi: " + string.Join(", ", userNumbers) + " Lisänumerosi: "+ string.Join(", ", additionalUserNumbers));
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