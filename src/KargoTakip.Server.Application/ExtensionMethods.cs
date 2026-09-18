using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KargoTakip.Server.Application;
public static class ExtensionMethods
{
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field!.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
        return attribute?.Name ?? value.ToString();
    }
}
