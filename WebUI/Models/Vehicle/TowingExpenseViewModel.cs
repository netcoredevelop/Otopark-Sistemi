using System;

namespace WebUI.Models.Vehicle
{
    public class TowingExpenseViewModel
    {
        public int VehicleId { get; set; }
        public DateTime TowingDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; }

    }
} 