public class ExchangeStudent : Student
{
    public string? Origin { get; set; }
    public ExchangeStudent(string? name, int? age, string? fieldOfStudy,string? origin): base(name,age,fieldOfStudy)
    {
        Origin = origin;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Kansalaisuus = {Origin}";
    }
}