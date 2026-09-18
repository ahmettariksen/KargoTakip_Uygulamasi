using KargoTakip.Server.Application.Kargolarim;
using KargoTakip.Server.Domain.Kargolarim;
using MediatR;
using System.Threading;
using TS.Result;

namespace KargoTakip.Server.WebAPI.Modules;

public static class KargoModule
{
    public static void RegisterKargoRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group =
            app.MapGroup("/kargolarim").WithTags("Kargolarim").RequireAuthorization();

        group.MapPost(string.Empty,
            async (ISender sender, KargoCreateCommand request, CancellationToken cancellatioNToken) =>
            {
                var response = await sender.Send(request, cancellatioNToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("KargoCreate");

        group.MapGet("{id}",
           async (Guid id, ISender sender, CancellationToken cancellatioNToken) =>
           {
               var response = await sender.Send(new KargoGetQuery(id), cancellatioNToken);
               return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
           })
           .Produces<Result<Kargo>>()
           .WithName("KargoGet");

        group.MapPut(string.Empty,
            async (ISender sender, KargoUpdateCommand request, CancellationToken cancellatioNToken) =>
            {
                var response = await sender.Send(request, cancellatioNToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("KargoUpdate");

        group.MapPut("update-status",
           async (ISender sender, KargoDurumUpdateCommand request, CancellationToken cancellatioNToken) =>
           {
               var response = await sender.Send(request, cancellatioNToken);
               return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
           })
           .Produces<Result<string>>()
           .WithName("KargoDurumUpdate");

        group.MapDelete("{id}",
            async (Guid id, ISender sender, CancellationToken cancellatioNToken) =>
            {
                var response = await sender.Send(new KargoDeleteCommand(id), cancellatioNToken);
                return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
            })
            .Produces<Result<string>>()
            .WithName("KargoDelete");



    }
}
