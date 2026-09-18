using Ardalis.SmartEnum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KargoTakip.Server.Domain.Kargolarim;

//smart enum o data da problem çıkarttığı için normal enuma çevirdim

//public sealed class KargoDurumEnum : SmartEnum<KargoDurumEnum>
//{
//    public static KargoDurumEnum Bekliyor = new("Bekliyor", 0);
//    public static KargoDurumEnum AracaTeslimEdildi = new("Araca Teslim Edildi", 1);
//    public static KargoDurumEnum YolaCikti = new("Yola Çıktı", 2);
//    public static KargoDurumEnum TeslimSubesineUlasti = new("Teslim Şubesine Ulaştı", 3);
//    public static KargoDurumEnum TeslimIcinYolaCikti = new("Teslim için Yola Çıktı", 4);
//    public static KargoDurumEnum TeslimEdildi = new("Teslim Edildi", 5);
//    public static KargoDurumEnum AdresteKimseBulunamadı = new("Adreste Kimse Bulunamadı", 6);
//    public static KargoDurumEnum IptalEdildi = new("İptal Edildi", 0);

//    public KargoDurumEnum(string name, int value) : base(name, value)
//    {
//    }
//}

public enum KargoDurumEnum
{
    [Display(Name = "Bekliyor")]
    Bekliyor = 0,

    [Display(Name = "Araca Teslim Edildi")]
    AracaTeslimEdildi = 1,

    [Display(Name = "Yola Çıktı")]
    YolaCikti = 2,

    [Display(Name = "Teslim Şubesine Ulaştı")]
    TeslimSubesineUlasti = 3,

    [Display(Name = "Teslim için Yola Çıktı")]
    TeslimIcinYolaCikti = 4,

    [Display(Name = "Teslim Edildi")]
    TeslimEdildi = 5,

    [Display(Name = "Adreste Kimse Bulunamadı")]
    AdresteKimseBulunamadı = 6,

    [Display(Name = "İptal Edildi")]
    IptalEdildi = 7
}