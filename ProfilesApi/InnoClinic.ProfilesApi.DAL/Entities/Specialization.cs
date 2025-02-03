namespace InnoClinic.Prof.DataAccess.Entities;

public class Specialization
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public List<Doctor> Doctors { get; set; }
}