using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDoctorRepo
    {
        Result<IEnumerable<Doctor>> Get();
        Result<Doctor> Get(int id);
        Result RemoveLicence(int id);
        Result Remove(int id);
        Result Edit(Doctor doctor);
        Result Add(Doctor doctor);
    }
}
