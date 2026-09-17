using KargoTakip.Server.Domain.Abstractions;
using KargoTakip.Server.Domain.Kargolarim;
using KargoTakip.Server.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KargoTakip.Server.Application.Kargolarim;
public sealed record KargoGetAllQuery() : IRequest<IQueryable<KargoGetAllQueryResponse>>;

public sealed class KargoInformationGetAllDto
{
    public string KargoTipiName { get; set; } = default!;
    public int Agirlik { get; set; }
}
public sealed class KargoDurumGetAllDto
{
    public string KargoDurumName { get; set; } = default!;
}
public sealed class KargoGetAllQueryResponse : EntityDto
{
    public Person Gonderen { get; set; } = default!;
    public Person Alici { get; set; } = default!;
    public Address TeslimAdresi { get; set; } = default!;

    //direk kargoınformation tipi yazarsak sonuç gösteremeyebiliyor o yüzden özelleştirip(sebebi odata enumları okuyamıyor o yüzden verileri ayıkladık dto classında) o sınıfı tip olarak yazacağım.
    public KargoInformationGetAllDto KargoInformation { get; set; } = default!;
    public KargoDurumGetAllDto KargoDurumu { get; set; } = default!;
}
internal sealed class KargoGetAllQueryHandler(
    IKargoRepository kargoRepository,
    UserManager<AppUser> userManager) : IRequestHandler<KargoGetAllQuery, IQueryable<KargoGetAllQueryResponse>>
{
    public Task<IQueryable<KargoGetAllQueryResponse>> Handle(KargoGetAllQuery request, CancellationToken cancellationToken)
    {
        var response = (from entity in kargoRepository.GetAll()
                        join create_user in userManager.Users.AsQueryable() on entity.CreateUserId equals create_user.Id
                        join update_user in userManager.Users.AsQueryable() on entity.UpdateUserId equals update_user.Id into update_user
                        from update_users in update_user.DefaultIfEmpty()
                        select new KargoGetAllQueryResponse
                        {
                            Alici = entity.Alici,
                            Gonderen = entity.Gonderen,
                            TeslimAdresi = entity.TeslimAdresi,
                            KargoInformation = new KargoInformationGetAllDto
                            {
                                Agirlik = entity.KargoInformation.Agirlik,
                                KargoTipiName = entity.KargoInformation.KargoTipi.Name,
                            },
                            KargoDurumu = new KargoDurumGetAllDto
                            {
                                KargoDurumName = entity.KargoDurum.Name
                            },
                            CreateAt = entity.CreateAt,
                            UpdateAt = entity.UpdateAt,
                            IsDeleted = entity.IsDeleted,
                            DeleteAt = entity.DeleteAt,
                            CreateUserId = entity.CreateUserId,
                            CreateUserName = create_user.FirstName + " " + create_user.LastName + "(" + create_user.Email + ")",
                            UpdateUserId = entity.UpdateUserId,
                            UpdateUserName = entity.UpdateUserId == null ? "" : update_users.FirstName + " " + update_users.LastName + "(" + update_users.Email + ")",
                        });
        return Task.FromResult(response);
    }
}
