using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.MoneyType
{
    /// <summary>
    ///  ISO 4217 currencies code list
    ///  <para>The string enum representation is the 4217_alphacode</para>
    ///  <para>The Int enum representation is the 4217_numericcode</para>
    ///  <para>See http://www.iso.org/iso/fr/support/faqs/faqs_widely_used_standards/widely_used_standards_other/currency_codes/currency_codes_list-1.htm</para>
    /// </summary>
    public enum Currency
    {
        None,

        /// <summary>
        /// UAE Dirham (UNITED ARAB EMIRATES)
        /// </summary>
        [CurrencySymbol("د.إ")]
        [CurrencyLabel("AED")]
        AED = 784,

        /// <summary>
        /// Afghani (AFGHANISTAN)
        /// </summary>
        [CurrencySymbol("؋")]
        [CurrencyLabel("AFN")]
        AFN = 971,

        /// <summary>
        /// Lek (ALBANIA)
        /// </summary>
        [CurrencySymbol("Lek")]
        [CurrencyLabel("ALL")]
        ALL = 008,

        /// <summary>
        /// Armenian Dram (ARMENIA)
        /// </summary>
        [CurrencySymbol("AMD")]
        [CurrencyLabel("AMD")]
        AMD = 051,

        /// <summary>
        /// Netherlands Antillean Guilder (CURACAO)
        /// </summary>
        [CurrencySymbol("ƒ")]
        [CurrencyLabel("ANG")]
        ANG = 532,

        /// <summary>
        /// Kwanza (ANGOLA)
        /// </summary>
        [CurrencySymbol("AOA")]
        [CurrencyLabel("AOA")]
        AOA = 973,

        /// <summary>
        /// Argentine Peso (ARGENTINA)
        /// </summary>
        [CurrencySymbol("$")]
        [CurrencyLabel("ARS")]
        ARS = 032,

        /// <summary>
        /// Australian Dollar (AUSTRALIA)
        /// </summary>
        [CurrencySymbol("$")]
        [CurrencyLabel("AUD")]
        AUD = 036,

        /// <summary>
        /// Aruban Guilder (ARUBA)
        /// </summary>
        [CurrencySymbol("ƒ")]
        [CurrencyLabel("AWG")]
        AWG = 533,

        /// <summary>
        /// Azerbaijanian Manat (AZERBAIJAN)
        /// </summary>
        [CurrencySymbol("₼")]
        [CurrencyLabel("AZN")]
        AZN = 944,

        /// <summary>
        /// Convertible Mark (BOSNIA HERZEGOVINA)
        /// </summary>
        [CurrencySymbol("KM")]
        [CurrencyLabel("BAM")]
        BAM = 977,

        /// <summary>
        /// Barbados Dollar (BARBADOS)
        /// </summary>
        [CurrencySymbol("$")]
        [CurrencyLabel("BBD")]
        BBD = 052,

        /// <summary>
        /// Taka (BANGLADESH)
        /// </summary>
        [CurrencySymbol("BDT")]
        [CurrencyLabel("BDT")]
        BDT = 050,

        /// <summary>
        /// Bulgarian Lev (BULGARIA)
        /// </summary>
        [CurrencySymbol("лв")]
        [CurrencyLabel("BGN")]
        BGN = 975,

        /// <summary>
        /// Bahraini Dinar (BAHRAIN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BHD")]
        BHD = 048,

        /// <summary>
        /// Burundi Franc (BURUNDI)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BIF")]
        BIF = 108,

        /// <summary>
        /// Bermudian Dollar (BERMUDA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BMD")]
        BMD = 060,

        /// <summary>
        /// Brunei Dollar (BRUNEI DARUSSALAM)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BND")]
        BND = 096,

        /// <summary>
        /// Boliviano (BOLIVIA, PLURINATIONAL STATE OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BOB")]
        BOB = 068,

        /// <summary>
        /// Mvdol (BOLIVIA, PLURINATIONAL STATE OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BOV")]
        BOV = 984,

        /// <summary>
        /// Brazilian Real (BRAZIL)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BRL")]
        BRL = 986,

        /// <summary>
        /// Bahamian Dollar (BAHAMAS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BSD")]
        BSD = 044,

        /// <summary>
        /// Ngultrum  (BHUTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BTN")]
        BTN = 064,

        /// <summary>
        /// Pula (BOTSWANA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BWP")]
        BWP = 072,

        /// <summary>
        /// Belarussian Ruble (BELARUS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BYR")]
        BYR = 974,

        /// <summary>
        /// Belize Dollar (BELIZE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BZD")]
        BZD = 084,

        /// <summary>
        /// Canadian Dollar (CANADA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CAD")]
        CAD = 124,

        /// <summary>
        /// Congolese Franc (CONGO, THE DEMOCRATIC REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CDF")]
        CDF = 976,

        /// <summary>
        /// WIR Euro (SWITZERLAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CHE")]
        CHE = 947,

        /// <summary>
        /// Swiss Franc (SWITZERLAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CHF")]
        CHF = 756,

        /// <summary>
        /// WIR Franc (SWITZERLAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CHW")]
        CHW = 948,

        /// <summary>
        /// Unidades de fomento  (CHILE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CLF")]
        CLF = 990,

        /// <summary>
        /// Chilean Peso (CHILE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CLP")]
        CLP = 152,

        /// <summary>
        /// Yuan Renminbi (CHINA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CNY")]
        CNY = 156,

        /// <summary>
        /// Colombian Peso (COLOMBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("COP")]
        COP = 170,

        /// <summary>
        /// Unidad de Valor Real (COLOMBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("COU")]
        COU = 970,

        /// <summary>
        /// Costa Rican Colon (COSTA RICA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CRC")]
        CRC = 188,

        /// <summary>
        /// Peso Convertible (CUBA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CUC")]
        CUC = 931,

        /// <summary>
        /// Cuban Peso (CUBA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CUP")]
        CUP = 192,

        /// <summary>
        /// Cape Verde Escudo (CAPE VERDE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CVE")]
        CVE = 132,

        /// <summary>
        /// Czech Koruna (CZECH REPUBLIC)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CZK")]
        CZK = 203,

        /// <summary>
        /// Djibouti Franc (DJIBOUTI)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DJF")]
        DJF = 262,

        /// <summary>
        /// Danish Krone (DENMARK)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DKK")]
        DKK = 208,

        /// <summary>
        /// Dominican Peso (DOMINICAN REPUBLIC)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DOP")]
        DOP = 214,

        /// <summary>
        /// Algerian Dinar (ALGERIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DZD")]
        DZD = 012,

        /// <summary>
        /// Egyptian Pound (EGYPT)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("EGP")]
        EGP = 818,

        /// <summary>
        /// Nakfa (ERITREA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ERN")]
        ERN = 232,

        /// <summary>
        /// Ethiopian Birr (ETHIOPIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ETB")]
        ETB = 230,

        /// <summary>
        /// Euro (Europe)
        /// </summary>
        [CurrencySymbol("€")]
        [CurrencyLabel("EUR")]
        EUR = 978,

        /// <summary>
        /// Fiji Dollar (FIJI)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("FJD")]
        FJD = 242,

        /// <summary>
        /// Falkland Islands Pound (FALKLAND ISLANDS (MALVINAS))
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("FKP")]
        FKP = 238,

        /// <summary>
        /// Pound Sterling
        /// </summary>
        [CurrencySymbol("£")]
        [CurrencyLabel("GBP")]
        GBP = 826,

        /// <summary>
        /// Lari (GEORGIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GEL")]
        GEL = 981,

        /// <summary>
        /// Cedi (GHANA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GHS")]
        GHS = 936,

        /// <summary>
        /// Gibraltar Pound (GIBRALTAR)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GIP")]
        GIP = 292,

        /// <summary>
        /// Dalasi (GAMBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GMD")]
        GMD = 270,

        /// <summary>
        /// Guinea Franc (GUINEA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GNF")]
        GNF = 324,

        /// <summary>
        /// Quetzal (GUATEMALA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GTQ")]
        GTQ = 320,

        /// <summary>
        /// Guyana Dollar (GUYANA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GYD")]
        GYD = 328,

        /// <summary>
        /// Hong Kong Dollar (HONG KONG)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("HKD")]
        HKD = 344,

        /// <summary>
        /// Lempira (HONDURAS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("HNL")]
        HNL = 340,

        /// <summary>
        /// Croatian Kuna (CROATIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("HRK")]
        HRK = 191,

        /// <summary>
        /// Gourde (HAITI)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("HTG")]
        HTG = 332,

        /// <summary>
        /// Forint (HUNGARY)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("HUF")]
        HUF = 348,

        /// <summary>
        /// Rupiah (INDONESIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("IDR")]
        IDR = 360,

        /// <summary>
        /// New Israeli Sheqel (ISRAEL)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ILS")]
        ILS = 376,

        /// <summary>
        /// Indian Rupee (BHUTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("INR")]
        INR = 356,

        /// <summary>
        /// Iraqi Dinar (IRAQ)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("IQD")]
        IQD = 368,

        /// <summary>
        /// Iranian Rial (IRAN, ISLAMIC REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("IRR")]
        IRR = 364,

        /// <summary>
        /// Iceland Krona (ICELAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ISK")]
        ISK = 352,

        /// <summary>
        /// Jamaican Dollar (JAMAICA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("JMD")]
        JMD = 388,

        /// <summary>
        /// Jordanian Dinar (JORDAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("JOD")]
        JOD = 400,

        /// <summary>
        /// Yen (JAPAN) (ALT 0165)
        /// </summary>
        [CurrencySymbol("¥")]
        [CurrencyLabel("JPY")]
        JPY = 392,

        /// <summary>
        /// Kenyan Shilling (KENYA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KES")]
        KES = 404,

        /// <summary>
        /// Som (KYRGYZSTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KGS")]
        KGS = 417,

        /// <summary>
        /// Riel (CAMBODIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KHR")]
        KHR = 116,

        /// <summary>
        /// Comoro Franc (COMOROS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KMF")]
        KMF = 174,

        /// <summary>
        /// North Korean Won (KOREA, DEMOCRATIC PEOPLE’S REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KPW")]
        KPW = 408,

        /// <summary>
        /// Won (KOREA, REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KRW")]
        KRW = 410,

        /// <summary>
        /// Kuwaiti Dinar (KUWAIT)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KWD")]
        KWD = 414,

        /// <summary>
        /// Cayman Islands Dollar (CAYMAN ISLANDS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KYD")]
        KYD = 136,

        /// <summary>
        /// Tenge (KAZAKHSTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("KZT")]
        KZT = 398,

        /// <summary>
        /// Kip (LAO PEOPLE’S DEMOCRATIC REPUBLIC)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LAK")]
        LAK = 418,

        /// <summary>
        /// Lebanese Pound (LEBANON)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LBP")]
        LBP = 422,

        /// <summary>
        /// Sri Lanka Rupee (SRI LANKA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LKR")]
        LKR = 144,

        /// <summary>
        /// Liberian Dollar (LIBERIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LRD")]
        LRD = 430,

        /// <summary>
        /// Loti (LESOTHO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LSL")]
        LSL = 426,

        /// <summary>
        /// Lithuanian Litas (LITHUANIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LTL")]
        LTL = 440,

        /// <summary>
        /// Latvian Lats (LATVIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LVL")]
        LVL = 428,

        /// <summary>
        /// Libyan Dinar (LIBYAN ARAB JAMAHIRIYA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LYD")]
        LYD = 434,

        /// <summary>
        /// Moroccan Dirham (MOROCCO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MAD")]
        MAD = 504,

        /// <summary>
        /// Moldovan Leu (MOLDOVA, REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MDL")]
        MDL = 498,

        /// <summary>
        /// Malagasy Ariary (MADAGASCAR)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MGA")]
        MGA = 969,

        /// <summary>
        /// Denar (MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MKD")]
        MKD = 807,

        /// <summary>
        /// Kyat (MYANMAR)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MMK")]
        MMK = 104,

        /// <summary>
        /// Tugrik (MONGOLIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MNT")]
        MNT = 496,

        /// <summary>
        /// Pataca (MACAO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MOP")]
        MOP = 446,

        /// <summary>
        /// Ouguiya (MAURITANIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MRO")]
        MRO = 478,

        /// <summary>
        /// Mauritius Rupee (MAURITIUS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MUR")]
        MUR = 480,

        /// <summary>
        /// Rufiyaa (MALDIVES)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MVR")]
        MVR = 462,

        /// <summary>
        /// Kwacha (MALAWI)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MWK")]
        MWK = 454,

        /// <summary>
        /// Mexican Peso (MEXICO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MXN")]
        MXN = 484,

        /// <summary>
        /// Mexican Unidad de Inversion (UDI) (MEXICO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MXV")]
        MXV = 979,

        /// <summary>
        /// Malaysian Ringgit (MALAYSIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MYR")]
        MYR = 458,

        /// <summary>
        /// Metical (MOZAMBIQUE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MZN")]
        MZN = 943,

        /// <summary>
        /// Namibia Dollar (NAMIBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NAD")]
        NAD = 516,

        /// <summary>
        /// Naira (NIGERIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NGN")]
        NGN = 566,

        /// <summary>
        /// Cordoba Oro (NICARAGUA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NIO")]
        NIO = 558,

        /// <summary>
        /// Norwegian Krone
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NOK")]
        NOK = 578,

        /// <summary>
        /// Nepalese Rupee (NEPAL)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NPR")]
        NPR = 524,

        /// <summary>
        /// New Zealand Dollar
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NZD")]
        NZD = 554,

        /// <summary>
        /// Rial Omani (OMAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("OMR")]
        OMR = 512,

        /// <summary>
        /// Balboa (PANAMA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PAB")]
        PAB = 590,

        /// <summary>
        /// Nuevo Sol (PERU)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PEN")]
        PEN = 604,

        /// <summary>
        /// Kina (PAPUA NEW GUINEA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PGK")]
        PGK = 598,

        /// <summary>
        /// Philippine Peso (PHILIPPINES)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PHP")]
        PHP = 608,

        /// <summary>
        /// Pakistan Rupee (PAKISTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PKR")]
        PKR = 586,

        /// <summary>
        /// Zloty (POLAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PLN")]
        PLN = 985,

        /// <summary>
        /// Guarani (PARAGUAY)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PYG")]
        PYG = 600,

        /// <summary>
        /// Qatari Rial (QATAR)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("QAR")]
        QAR = 634,

        /// <summary>
        /// Leu (ROMANIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("RON")]
        RON = 946,

        /// <summary>
        /// Serbian Dinar (SERBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("RSD")]
        RSD = 941,

        /// <summary>
        /// Russian Ruble (RUSSIAN FEDERATION)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("RUB")]
        RUB = 643,

        /// <summary>
        /// Rwanda Franc (RWANDA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("RWF")]
        RWF = 646,

        /// <summary>
        /// Saudi Riyal (SAUDI ARABIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SAR")]
        SAR = 682,

        /// <summary>
        /// Solomon Islands Dollar (SOLOMON ISLANDS)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SBD")]
        SBD = 090,

        /// <summary>
        /// Seychelles Rupee (SEYCHELLES)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SCR")]
        SCR = 690,

        /// <summary>
        /// Sudanese Pound (SUDAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SDG")]
        SDG = 938,

        /// <summary>
        /// Swedish Krona (SWEDEN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SEK")]
        SEK = 752,

        /// <summary>
        /// Singapore Dollar (SINGAPORE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SGD")]
        SGD = 702,

        /// <summary>
        /// Saint Helena Pound (SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SHP")]
        SHP = 654,

        /// <summary>
        /// Leone (SIERRA LEONE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SLL")]
        SLL = 694,

        /// <summary>
        /// Somali Shilling (SOMALIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SOS")]
        SOS = 706,

        /// <summary>
        /// Surinam Dollar (SURINAME)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SRD")]
        SRD = 968,

        /// <summary>
        /// Dobra (SÃO TOME AND PRINCIPE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("STD")]
        STD = 678,

        /// <summary>
        /// Syrian Pound (SYRIAN ARAB REPUBLIC)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SYP")]
        SYP = 760,

        /// <summary>
        /// Lilangeni (SWAZILAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SZL")]
        SZL = 748,

        /// <summary>
        /// Baht (THAILAND)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("THB")]
        THB = 764,

        /// <summary>
        /// Somoni (TAJIKISTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TJS")]
        TJS = 972,

        /// <summary>
        /// New Manat (TURKMENISTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TMT")]
        TMT = 934,

        /// <summary>
        /// Tunisian Dinar (TUNISIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TND")]
        TND = 788,

        /// <summary>
        /// Pa’anga (TONGA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TOP")]
        TOP = 776,

        /// <summary>
        /// Turkish Lira (TURKEY)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TRY")]
        TRY = 949,

        /// <summary>
        /// Trinidad and Tobago Dollar (TRINIDAD AND TOBAGO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TTD")]
        TTD = 780,

        /// <summary>
        /// New Taiwan Dollar (TAIWAN, PROVINCE OF CHINA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TWD")]
        TWD = 901,

        /// <summary>
        /// Tanzanian Shilling (TANZANIA, UNITED REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TZS")]
        TZS = 834,

        /// <summary>
        /// Hryvnia (UKRAINE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UAH")]
        UAH = 980,

        /// <summary>
        /// Uganda Shilling (UGANDA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UGX")]
        UGX = 800,

        /// <summary>
        /// US Dollar
        /// </summary>
        [CurrencySymbol("$")]
        [CurrencyLabel("USD")]
        USD = 840,

        /// <summary>
        /// US Dollar (Next day)  (UNITED STATES)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("USN")]
        USN = 997,

        /// <summary>
        /// US Dollar (Same day)  (UNITED STATES)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("USS")]
        USS = 998,

        /// <summary>
        /// Uruguay Peso en Unidades Indexadas (URUIURUI) (URUGUAY)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UYI")]
        UYI = 940,

        /// <summary>
        /// Peso Uruguayo (URUGUAY)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UYU")]
        UYU = 858,

        /// <summary>
        /// Uzbekistan Sum (UZBEKISTAN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UZS")]
        UZS = 860,

        /// <summary>
        /// Bolivar Fuerte (VENEZUELA, BOLIVARIAN REPUBLIC OF)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("VEF")]
        VEF = 937,

        /// <summary>
        /// Dong (VIET NAM)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("VND")]
        VND = 704,

        /// <summary>
        /// Vatu (VANUATU)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("VUV")]
        VUV = 548,

        /// <summary>
        /// Tala (SAMOA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("WST")]
        WST = 882,

        /// <summary>
        /// CFA Franc BEAC  (CAMEROON)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XAF")]
        XAF = 950,

        /// <summary>
        /// Silver (ZZ11_Silver)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XAG")]
        XAG = 961,

        /// <summary>
        /// Gold (ZZ08_Gold)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XAU")]
        XAU = 959,

        /// <summary>
        /// Bond Markets Unit European Composite Unit (EURCO) (ZZ01_Bond Markets Unit European_EURCO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XBA")]
        XBA = 955,

        /// <summary>
        /// Bond Markets Unit European Monetary Unit (E.M.U.-6)  (ZZ02_Bond Markets Unit European_EMU-6)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XBB")]
        XBB = 956,

        /// <summary>
        /// Bond Markets Unit European Unit of Account 9 (E.U.A.-9) (ZZ03_Bond Markets Unit European_EUA-9)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XBC")]
        XBC = 957,

        /// <summary>
        /// Bond Markets Unit European Unit of Account 17 (E.U.A.-17) (ZZ04_Bond Markets Unit European_EUA-17)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XBD")]
        XBD = 958,

        /// <summary>
        /// East Caribbean Dollar (ANGUILLA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XCD")]
        XCD = 951,

        /// <summary>
        /// SDR (Special Drawing Right) (INTERNATIONAL MONETARY FUND (IMF) )
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XDR")]
        XDR = 960,

        /// <summary>
        /// CFA Franc BCEAO  (BENIN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XOF")]
        XOF = 952,

        /// <summary>
        /// Palladium (ZZ09_Palladium)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XPD")]
        XPD = 964,

        /// <summary>
        /// CFP Franc (FRENCH POLYNESIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XPF")]
        XPF = 953,

        /// <summary>
        /// Platinum (ZZ10_Platinum)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XPT")]
        XPT = 962,

        /// <summary>
        /// Sucre (SISTEMA UNITARIO DE COMPENSACION REGIONAL DE PAGOS SUCRE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XSU")]
        XSU = 994,

        /// <summary>
        /// Codes specifically reserved for testing purposes (ZZ06_Testing_Code)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XTS")]
        XTS = 963,

        /// <summary>
        /// ADB Unit of Account (MEMBER COUNTRIES OF THE AFRICAN DEVELOPMENT BANK GROUP)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XUA")]
        XUA = 965,

        /// <summary>
        /// The codes assigned for transactions where no currency is involved (ZZ07_No_Currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XXX")]
        XXX = 999,

        /// <summary>
        /// Yemeni Rial (YEMEN)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("YER")]
        YER = 886,

        /// <summary>
        /// Rand (LESOTHO)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZAR")]
        ZAR = 710,

        /// <summary>
        /// Zambian Kwacha (ZAMBIA)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZMK")]
        ZMK = 894,

        /// <summary>
        /// Zimbabwe Dollar (ZIMBABWE)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZWL")]
        ZWL = 932,

        #region Historical Unit

        /// <summary>
        /// Andorran peseta (1:1 peg to the Spanish peseta) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ADP")]
        ADP = 20,

        /// <summary>
        /// Austrian schilling (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ATS")]
        ATS = 40,

        /// <summary>
        /// Belgian franc (currency union with LUF) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BEF")]
        BEF = 56,

        /// <summary>
        /// Cypriot pound (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CYP")]
        CYP = 196,

        /// <summary>
        /// German mark (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DEM")]
        DEM = 276,

        /// <summary>
        /// Estonian kroon (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("EEK")]
        EEK = 233,

        /// <summary>
        /// Spanish peseta (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ESP")]
        ESP = 724,

        /// <summary>
        /// Finnish markka (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("FIM")]
        FIM = 246,

        /// <summary>
        /// French franc (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("FRF")]
        FRF = 250,

        /// <summary>
        /// Greek drachma (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GRD")]
        GRD = 300,

        /// <summary>
        /// Irish pound (punt in Irish language) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("IEP")]
        IEP = 372,

        /// <summary>
        /// Italian lira (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ITL")]
        ITL = 380,

        /// <summary>
        /// Luxembourg franc (currency union with BEF) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("LUF")]
        LUF = 442,

        /// <summary>
        /// Maltese lira (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MTL")]
        MTL = 470,

        /// <summary>
        /// Netherlands guilder (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("NLG")]
        NLG = 528,

        /// <summary>
        /// Portuguese escudo (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PTE")]
        PTE = 620,

        /// <summary>
        /// Slovenian tolar (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SIT")]
        SIT = 705,

        /// <summary>
        /// Slovak koruna (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SKK")]
        SKK = 703,

        /// <summary>
        /// European Currency Unit (1 XEU = 1 EUR) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("XEU")]
        XEU = 954,

        /// <summary>
        /// Afghan afghani (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("AFA")]
        AFA = 4,

        /// <summary>
        /// Angolan new kwanza (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("AON")]
        AON = 24,

        /// <summary>
        /// Angolan kwanza readjustado (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("AOR")]
        AOR = 982,

        /// <summary>
        /// Azerbaijani manat (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("AZM")]
        AZM = 31,

        /// <summary>
        /// Bulgarian lev A/99 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("BGL")]
        BGL = 100,

        /// <summary>
        /// Serbian dinar (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CSD")]
        CSD = 891,

        /// <summary>
        /// Czechoslovak koruna (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("CSK")]
        CSK = 200,

        /// <summary>
        /// East German Mark of the GDR (East Germany) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("DDM")]
        DDM = 278,

        /// <summary>
        /// Ecuadorian sucre (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ECS")]
        ECS = 218,

        /// <summary>
        /// Ecuador Unidad de Valor Constante (funds code) (discontinued) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ECV")]
        ECV = 983,

        /// <summary>
        /// Spanish peseta (account A) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ESA")]
        ESA = 996,

        /// <summary>
        /// Spanish peseta (account B) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ESB")]
        ESB = 995,

        /// <summary>
        /// Ghanaian cedi (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GHC")]
        GHC = 288,

        /// <summary>
        /// Guinea-Bissau peso (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("GWP")]
        GWP = 624,

        /// <summary>
        /// Malagasy franc (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MGF")]
        MGF = 450,

        /// <summary>
        /// Mozambican metical (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("MZM")]
        MZM = 508,

        /// <summary>
        /// Polish zloty A/94 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("PLZ")]
        PLZ = 616,

        /// <summary>
        /// Romanian leu A/05 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ROL")]
        ROL = 642,

        /// <summary>
        /// Russian rouble A/97 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("RUR")]
        RUR = 810,

        /// <summary>
        /// Sudanese dinar (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SDD")]
        SDD = 736,

        //// <summary>
        //// Sudanese old pound (historical currency)
        //// </summary>
        //// [CurrencySymbol("")]
        //// SDP = 736, // Duplicate numeric code with 'Sudanese dinar' (the initial code is duplicate see http://en.wikipedia.org/wiki/ISO_4217)

        /// <summary>
        /// Suriname guilder (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SRG")]
        SRG = 740,

        /// <summary>
        /// Salvadoran colón (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("SVC")]
        SVC = 222,

        /// <summary>
        /// Tajikistani ruble (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TJR")]
        TJR = 762,

        /// <summary>
        /// Turkmenistani manat (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TMM")]
        TMM = 795,

        /// <summary>
        /// Turkish lira A/05 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("TRL")]
        TRL = 792,

        /// <summary>
        /// Ukrainian karbovanets (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("UAK")]
        UAK = 804,

        /// <summary>
        /// Venezuelan bolívar (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("VEB")]
        VEB = 862,

        /// <summary>
        /// South Yemeni dinar (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("YDD")]
        YDD = 720,

        //// <summary>
        //// Yugoslav dinar (historical currency)
        //// </summary>
        //// [CurrencySymbol("")]
        //// YUM = 891, // Duplicate numeric code with 'Serbian dinar' (the initial code is duplicate see http://en.wikipedia.org/wiki/ISO_4217)

        /// <summary>
        /// South African financial rand (funds code) (discontinued) (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZAL")]
        ZAL = 991,

        /// <summary>
        /// Zaïrean new zaïre (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZRN")]
        ZRN = 180,

        /// <summary>
        /// Zimbabwean dollar A/06 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZWD")]
        ZWD = 716,

        /// <summary>
        /// Zimbabwean dollar A/08 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZWN")]
        ZWN = 942,

        /// <summary>
        /// Zimbabwean dollar A/09 (historical currency)
        /// </summary>
        [CurrencySymbol("")]
        [CurrencyLabel("ZWR")]
        ZWR = 935,

        #endregion
    }
}
