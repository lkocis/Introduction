using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Model
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public bool IsActive { get; set; }
        public int? NumberOfStars { get; set; }
        public decimal? AverageScore { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
    }
}
