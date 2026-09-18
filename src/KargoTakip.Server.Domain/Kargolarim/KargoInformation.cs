namespace KargoTakip.Server.Domain.Kargolarim;

public sealed class KargoInformation
{
    public KargoTipiEnum KargoTipi { get; set; } = default!;
    public int KargoTipiValue => KargoTipi.Value;
    public int Agirlik { get; set; }
}
