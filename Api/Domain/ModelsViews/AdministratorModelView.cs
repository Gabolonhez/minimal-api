using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Enums; // Changed from Enuns to Enums
using minimal_api.Domain.Interfaces;
using minimal_api.Domain.ModelsViews;
using minimal_api.Domain.Services;
using minimal_api.Infraestructure.Db;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace minimal_api.Domain.ModelsViews
{
    public record AdministratorModelView
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public Profile Profile { get; set; } = default!; 
    }
}
