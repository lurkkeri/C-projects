using System.Reflection.Metadata;

public class Game
{
    public int Scissors
    {
        get;set;
    }
    public int Rock
    {
        get;set;
    }
    public int Paper
    {
        get;set;
    }
    public Game(int scissors,int rock,int paper)
    {
        Scissors = scissors;
        Rock = rock;
        Paper = paper;
        
    }
    //metodi pelin logiikalle
    public int Winner()
    {
        if(Scissors == 2 && Rock == 0 && Paper == 3)
        {   //sakset voittaa paperin
            return Scissors;
        }
        else if(Scissors == 2 && Rock == 1 && Paper == 0)
        {   //kivi voittaa sakset
            return Rock;
        }
        else if(Scissors == 0 && Rock == 1 && Paper == 3)
        {   //paperi voittaa kiven
            return Paper;
        }
        else
        {   //tasapeli
            return 0;
        }     
    }
    //metodi tulosten kansankielistämiseksi
    public string Translator(int answer)
    {
       if(answer == 2)
       {
        return "Sakset";
       }
       else if(answer == 1)
       {
        return "Kivi";
       }
       else if(answer == 3)
       {
        return "Paperi";
       }
       else
       {
        return "...Se on tasapeli!";
       }
    }
    
    
}