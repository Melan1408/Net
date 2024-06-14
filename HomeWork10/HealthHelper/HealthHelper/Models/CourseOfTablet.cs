using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHelper.Models
{
    public class CourseOfTablet
    {
        public int Id { get; set; }
        public int TabletId { get; set; }
        public string Time { get; set; }
        public virtual Tablet Tablet { get; set; }
        public int FamilyMemberId { get; set; } 
        public FamilyMember FamilyMember { get; set; } = null!;
    }
}
