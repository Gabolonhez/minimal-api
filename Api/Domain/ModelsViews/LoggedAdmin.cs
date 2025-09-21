namespace minimal_api.Domain.ModelsViews
{
    public record LoggedAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;

        public string Profile { get; set; } = default!; 
        public string Token { get; set; } = default!;
    }
}
