using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minimal_api.Domain.Entities
{
    public class Admnistrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;

        [StringLength(50)]
        public string Password { get; set; } = default!;

        [StringLength(15)]
        public string Profile { get; set; } = default!;
    }
}
