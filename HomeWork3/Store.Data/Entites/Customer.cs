using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entites
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

    }
}
