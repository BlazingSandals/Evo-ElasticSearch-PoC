namespace elastic_web.Models;

public class Provider
{
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string State { get; set; }
    public IEnumerable<Patient> Patients { get; set; }
}