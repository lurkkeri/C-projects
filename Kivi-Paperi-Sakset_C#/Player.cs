public class Player : Game
{
    public string Name { get; set; }
    public int Win { get; set; }
    public int TotalRounds { get; set; }
    public int TotalWins { get; set; }
    public int TotalRoundWins { get; set; }

    public Player(int scissors, int rock, int paper, int win, string name, int totalWins, int totalRounds, int totalRoundWins)
        : base(scissors, rock, paper)
    {   
        //täällä pidetään kirjaa jokaikisestä rundista
        TotalRounds = totalRounds;
        TotalWins = totalWins;
        Win = win;
        Name = name;
        TotalRoundWins = totalRoundWins;
    }

    public override string ToString()
    {
        return $"Pelaaja: {Name}, yksittäiset pelivoitot: {TotalWins}, Kierrosvoitot: {TotalRoundWins}, Kaikki kierrokset: {TotalRounds}";
    }
}