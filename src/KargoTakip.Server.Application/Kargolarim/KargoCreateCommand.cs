using FluentValidation;
using GenericRepository;
using KargoTakip.Server.Domain.Kargolarim;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace KargoTakip.Server.Application.Kargolarim;
public sealed record KargoCreateCommand(
    Person Gonderen,
    Person Alıcı,
    Address TeslimAdresi,
    KargoInformationDto KargoInformation) : IRequest<Result<string>>;

// Validatör yapılanmasında direkt enumu kullanamıyoruz bu sebeple bir dto oluşturduk bizim enum değerlerimiz bir value dir o yüzden kargotipivalue değişkeni oluşturduk ve bu dto' yu da yukarıdaki kargoınformation değişkenine tip olarak yazdık kargoınformationdto yerine kargoınformation verseydik validatörde enumları yapılandıramazdık.
public sealed record KargoInformationDto(
    int KargoTipiValue,
    int Agirlik);

public sealed class KargoCreateCommandValidator : AbstractValidator<KargoCreateCommand>
{
    public KargoCreateCommandValidator()
    {
        RuleFor(p => p.Gonderen.FirstName).NotEmpty().WithMessage("Geçerli bir gönderen adı girin");
        RuleFor(p => p.Gonderen.LastName).NotEmpty().WithMessage("Geçerli bir gönderen soyadı girin");
        RuleFor(p => p.Alıcı.FirstName).NotEmpty().WithMessage("Geçerli bir alıcı adı girin");
        RuleFor(p => p.Alıcı.LastName).NotEmpty().WithMessage("Geçerli bir alıcı soyadı girin");
        RuleFor(p => p.TeslimAdresi.FullAddress).NotEmpty().WithMessage("Geçerli bir tam adres girin");
        RuleFor(p => p.TeslimAdresi.Town).NotEmpty().WithMessage("Geçerli bir ilçe adı girin");
        RuleFor(p => p.TeslimAdresi.City).NotEmpty().WithMessage("Geçerli bir şehir adı girin");
        RuleFor(p => p.TeslimAdresi.Mahalle).NotEmpty().WithMessage("Geçerli bir mahalle adı girin");
        RuleFor(p => p.KargoInformation.KargoTipiValue)// smart enum yapısı kullanımı bu şeklide
            .GreaterThanOrEqualTo(0).WithMessage("Geçerli bir kargo tipi seçin")
            .LessThan(KargoTipiEnum.List.Count()).WithMessage("Geçerli bir kargo tipi seçin");
    }
}
internal sealed class KargoCreateCommandHandler(
    IKargoRepository kargoRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<KargoCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(KargoCreateCommand request, CancellationToken cancellationToken)
    {
        Kargo kargo = request.Adapt<Kargo>();

        KargoInformation kargoInformation = new(
            KargoTipiEnum.FromValue(request.KargoInformation.KargoTipiValue), request.KargoInformation.Agirlik);
        kargo.KargoDurum = KargoDurumEnum.Bekliyor;
        kargo.KargoInformation = kargoInformation;
        //Tc numarasında problem olduğu için manel aktarım yaptık.
        kargo.Alici = request.Alıcı;
        kargo.Gonderen = request.Gonderen;
        kargoRepository.Add(kargo);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        //to do : burada mail veya sms gönderme işlemleri yapılacak.
        //to do : ileride notification içinde domain event kullanabiliriz.

        return "Kargo başarıyla kaydedildi";
    }
}
