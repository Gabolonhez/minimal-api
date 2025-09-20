using minimal_api.Domain.Enuns;

namespace minimal_api.Domain.ModelsViews
{
    public record AdministratorModelView
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;

        public Profile Profile { get; set; } = default!;
 
    }
}
