using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minimal_api.Domain.DTOs
{
    public record class VehicleDTO
    {
        public string Name { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public int Year { get; set; } = default!;
    }
}
