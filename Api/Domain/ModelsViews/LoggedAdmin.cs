using minimal_api.Domain.Enums;

namespace minimal_api.Domain.ModelsViews
{
    public record LoggedAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public Profile Profile { get; set; } = default!; // Changed from string to Profile enum
        public string Token { get; set; } = default!;
    }
}
