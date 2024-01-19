public class Teacher : Person
{
    public Double? Wage
    {
        get;set;
    }
    public Teacher(string name, int age, double wage) : base(name,age)
    {
        Wage = wage;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Palkka = {Wage}";
    }
}