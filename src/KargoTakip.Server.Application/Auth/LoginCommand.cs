using KargoTakip.Server.Application.Services;
using KargoTakip.Server.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace KargoTakip.Server.Application.Auth;
public sealed record LoginCommand(
       string UserNameOrEmail,
       string Password) : IRequest<Result<LoginCommmandResponse>>;

public sealed record LoginCommmandResponse
{
    public string AccessToken { get; set; } = default!;
}
internal sealed class LoginCommandHandler(
    //user manager girişin doğru olup olmadığını kontrol eder kullanıcı arka arkaya yanlış şifre denemelerini engelleme denetimş yapmak için signin manager kullanacağız
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommmandResponse>>
{
    public async Task<Result<LoginCommmandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail || p.Email == request.UserNameOrEmail, cancellationToken);

        if (user is null)
        {
            return Result<LoginCommmandResponse>.Failure("Kullanıcı bulunamadı");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
                return (500, $"Şifrenizi 5 defa yanlış girdiniz için kullanıcı {Math.Ceiling(timeSpan.Value.TotalMinutes)} dakika süreyle bloke edilmiştir");
            else
                return (500, "Kuallnıcı 5 kez yanlış şifre girdiği için 5 dakika süreyle bloke edilmiştir");
        }
        if (signInResult.IsNotAllowed)
        {
            return (500, "Mail adresiniz onaylı değil");
        }
        if (!signInResult.Succeeded)
        {
            return (500, "Şifreniz yanlış");
        }

        // token üret

        var token = await jwtProvider.CreateTokenAsync(user, cancellationToken);

        var response = new LoginCommmandResponse()
        {
            AccessToken = token
        };

        return response;
    }
}
