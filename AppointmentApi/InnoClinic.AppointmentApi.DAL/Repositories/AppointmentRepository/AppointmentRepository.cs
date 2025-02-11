using InnoClinic.AppointmentApi.DataAccess.Entity;
using InnoClinic.AppointmentApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.AppointmentApi.DataAccess.Repositories.AppointmentRepository;

public class AppointmentRepository(InnoClinicAppointmentContext context) : IAppointmentRepository
{
    public async Task<Appointment> Add(Appointment doctor)
    {
        await context.Appointments.AddAsync(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Appointment> Update(Appointment doctor)
    {
        context.Appointments.Update(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Appointment?> Delete(string id)
    {
        var res = await context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        if (res == null)
        {
            return null;
        }
        context.Appointments.Remove(res);
        await context.SaveChangesAsync();
        return res;
    }
    
    public async Task<Appointment?> GetByIdAsync(string id)
    {
        var res =  await context.Appointments
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
    
    public async Task<List<Appointment>> GetAllAsync(QueryObject query)
    {
        var doctors = context.Appointments.AsQueryable();
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await doctors.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }
}