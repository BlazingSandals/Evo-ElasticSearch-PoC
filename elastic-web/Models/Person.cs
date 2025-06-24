public class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? State { get; set; }
    public string ProperName => $"{FirstName} {LastName}";
    public string PersonType => "Person";
}