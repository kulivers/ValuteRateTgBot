using CentralBankSDK.Model.CursOnDateResponse;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Services;

internal class Mapper
{
    public ValuteCursOnDateDto? Map(ValuteCursOnDate? libCurs)
    {
        return libCurs == null ? new ValuteCursOnDateDto() : new ValuteCursOnDateDto(libCurs.Vname, libCurs.Vnom, libCurs.Vcurs, libCurs.Vcode, libCurs.VchCode);
    }

    public ValuteCursOnDate Map(ValuteCursOnDateDto? dtoCurs)
    {
        if (dtoCurs == null)
        {
            return new ValuteCursOnDate();
        }

        return new ValuteCursOnDate()
        {
            Vname = dtoCurs.Vname,
            Vnom = dtoCurs.Vnom,
            Vcurs = dtoCurs.Vcurs,
            Vcode = dtoCurs.Vcode,
            VchCode = dtoCurs.VchCode
        };
    }
}
