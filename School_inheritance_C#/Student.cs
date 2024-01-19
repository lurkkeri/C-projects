public class Student:Person
{
    public string? FieldOfStudy { get; set; }
    public Student(string? name, int? age, string? fieldOfStudy): base(name,age)
    {
        FieldOfStudy = fieldOfStudy;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Tutkinto = {FieldOfStudy}";
    }

}