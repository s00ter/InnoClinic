using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace InnoClinic.OfficesApi.DataAccess.Entities;

[Collection("offices")]
public class Office
{
    public ObjectId Id { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhotoId { get; set; }
    
    public string? RegistryPhoneNumber { get; set; }
    
    public bool IsActive { get; set; }
}