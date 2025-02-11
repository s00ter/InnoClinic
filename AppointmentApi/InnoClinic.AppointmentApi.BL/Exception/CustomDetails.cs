using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.AppointmentApi.BL.Exception;

public class CustomDetails : ProblemDetails
{
    public string? AdditionalInfo { get; set; }
}