using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Enums
{
    public enum  MedicineType
    {
        OTC = 0,        // Over-the-counter (Thuốc không kê đơn)
        Prescription = 1 // Thuốc cần kê đơn (Rx)
    }
}
