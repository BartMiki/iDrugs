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
        Result RemoveLicence(Doctor doctor);
        Result Remove(Doctor doctor);
        Result Edit(Doctor doctor);
    }
}
