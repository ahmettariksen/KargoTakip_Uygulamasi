using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KargoTakip.Server.Domain.Dtos;
public sealed class ErrorResponseDto
{
    [JsonPropertyName("error")]
    public string Field { get; set; } = default!;

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; } = default!;
}
