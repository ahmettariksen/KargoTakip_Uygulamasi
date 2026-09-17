namespace KargoTakip.Server.Domain.Kargolarim;

public sealed record KargoInformation(
    KargoTipiEnum KargoTipi, // rastgele birşey yazılmasın diye smart enum ekledim      
    int Agirlik
    );

