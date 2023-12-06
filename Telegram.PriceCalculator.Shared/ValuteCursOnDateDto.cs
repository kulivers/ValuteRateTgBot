namespace Telegram.PriceCalculator.Shared;

public sealed record ValuteCursOnDateDto
{
    /// <summary>Название валюты.</summary>
    public string Vname { get; init; }

    /// <summary>Номинал.</summary>
    public int Vnom { get; init; }

    /// <summary>Курс.</summary>
    public Decimal Vcurs { get; init; }

    /// <summary>ISO Цифровой код валюты.</summary>
    public string Vcode { get; init; }

    /// <summary>ISO Символьный код валюты.</summary>
    public string VchCode { get; init; }

    public ValuteCursOnDateDto()
    {
    }

    public ValuteCursOnDateDto(string vname, int vnom, decimal vcurs, string vcode, string vchCode)
    {
        Vname = vname;
        Vnom = vnom;
        Vcurs = vcurs;
        Vcode = vcode;
        VchCode = vchCode;
    }
}
