public class Provider : Person
{
    public new string ProperName => $"Dr {LastName}";
    public new string PersonType = "Provider";
    public List<Patient> Patients { get; set; } = new List<Patient>();
}