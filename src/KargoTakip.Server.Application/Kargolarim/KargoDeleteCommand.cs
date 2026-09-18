using GenericRepository;
using KargoTakip.Server.Domain.Kargolarim;
using MediatR;
using TS.Result;

namespace KargoTakip.Server.Application.Kargolarim;
public sealed record KargoDeleteCommand(
    Guid Id) : IRequest<Result<string>>;

internal sealed record KargoDeleteCommandHandler(
    IKargoRepository KargoRepository,
    IUnitOfWork UnitOfWork) : IRequestHandler<KargoDeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(KargoDeleteCommand request, CancellationToken cancellationToken)
    {
        Kargo? kargo = await KargoRepository.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (kargo is null)
        {
            return Result<string>.Failure("Kargo bulunamadı");
        }
        if (kargo.KargoDurum != KargoDurumEnum.Bekliyor)
        {
            return Result<string>.Failure("Sadece bekleyen kargoları silebilirsiniz");
        }

        kargo.IsDeleted = true;
        KargoRepository.Update(kargo);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return "Kargo başarıyla silindi";
    }
}
