using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class PrescriptionItemNotFoundException : BaseNotFoundException
    {
        public PrescriptionItemNotFoundException(int prescriptionId, int itemId) : base($"pozycji o ID {itemId} na recepcie", prescriptionId){}

        public PrescriptionItemNotFoundException(int itemId) : base($"pozycji na recepcie", itemId) { }
    }
}
