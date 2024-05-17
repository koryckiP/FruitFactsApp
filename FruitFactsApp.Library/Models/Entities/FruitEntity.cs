using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitFactsApp.Library.Models.Entities
{
    public class FruitEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
        [StringLength(250, MinimumLength = 5)]
        public string? Description { get; set; }
        public string? Type { get; set; }
    }
}
