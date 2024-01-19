public class PC: Game
{
    public int Choise
    {
        get;set;
    }
    public int Win
    {
        get;set;
    }
    
    public PC(int scissors,int rock, int paper,int win): base(scissors,rock,paper)
    {
        Win = win;
    }
    //arpajaiskone metodi numeroille 1-3
    public int SetChoise()
    {
        Random rand = new Random();
        int randomChoise = rand.Next(1,4);
        Choise = randomChoise;
        return Choise;
    }
}