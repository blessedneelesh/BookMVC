using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMVC.Models.DataLayer
{
    [Table("MMABook")]
    public partial class Mmabook
    {
        [Key]
        [StringLength(50)]
        [Unicode(false)]
        public string ProductCode { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        public double UnitPrice { get; set; }
    }
}
