using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task4.Application.CQs.User.Queries.Login;

public class LoginQuery : IRequest<ModelStateDictionary>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public ModelStateDictionary ModelState { get; set; }
}