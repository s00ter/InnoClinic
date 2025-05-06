namespace InnoClinic.Prof.DataAccess.Entities;

public class Specialization
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public List<Doctor> Doctors { get; set; }
}