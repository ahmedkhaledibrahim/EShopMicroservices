using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Users.Application.Features.Commands.RegisterUser
{
    public class RegisterUserRequest : IRequest<Unit>
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
