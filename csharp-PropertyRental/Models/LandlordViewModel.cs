using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_PropertyRental.Models
{
    public class LandlordViewModel
    {
        public int LandlordId { get; set; }
        public string? LandlordFirstName { get; set; }
        public string? LandlordLastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}