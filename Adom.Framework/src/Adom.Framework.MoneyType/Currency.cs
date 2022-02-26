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
        [CurrencySymbole("د.إ")]
        [CurrencyLabel("AED")]
        AED = 784,

        /// <summary>
        /// Afghani (AFGHANISTAN)
        /// </summary>
        [CurrencySymbole("؋")]
        [CurrencyLabel("AFN")]
        AFN = 971,

        /// <summary>
        /// Lek (ALBANIA)
        /// </summary>
        [CurrencySymbole("Lek")]
        [CurrencyLabel("ALL")]
        ALL = 008,

        /// <summary>
        /// Armenian Dram (ARMENIA)
        /// </summary>
        [CurrencySymbole("AMD")]
        [CurrencyLabel("AMD")]
        AMD = 051,

        /// <summary>
        /// Netherlands Antillean Guilder (CURACAO)
        /// </summary>
        [CurrencySymbole("ƒ")]
        [CurrencyLabel("ANG")]
        ANG = 532,

        /// <summary>
        /// Kwanza (ANGOLA)
        /// </summary>
        [CurrencySymbole("AOA")]
        [CurrencyLabel("AOA")]
        AOA = 973,

        /// <summary>
        /// Argentine Peso (ARGENTINA)
        /// </summary>
        [CurrencySymbole("$")]
        [CurrencyLabel("ARS")]
        ARS = 032,

        /// <summary>
        /// Australian Dollar (AUSTRALIA)
        /// </summary>
        [CurrencySymbole("$")]
        [CurrencyLabel("AUD")]
        AUD = 036,

        /// <summary>
        /// Aruban Guilder (ARUBA)
        /// </summary>
        [CurrencySymbole("ƒ")]
        [CurrencyLabel("AWG")]
        AWG = 533,

        /// <summary>
        /// Azerbaijanian Manat (AZERBAIJAN)
        /// </summary>
        [CurrencySymbole("₼")]
        [CurrencyLabel("AZN")]
        AZN = 944,

        /// <summary>
        /// Convertible Mark (BOSNIA HERZEGOVINA)
        /// </summary>
        [CurrencySymbole("KM")]
        [CurrencyLabel("BAM")]
        BAM = 977,

        /// <summary>
        /// Barbados Dollar (BARBADOS)
        /// </summary>
        [CurrencySymbole("$")]
        [CurrencyLabel("BBD")]
        BBD = 052,

        /// <summary>
        /// Taka (BANGLADESH)
        /// </summary>
        [CurrencySymbole("BDT")]
        [CurrencyLabel("BDT")]
        BDT = 050,

        /// <summary>
        /// Bulgarian Lev (BULGARIA)
        /// </summary>
        [CurrencySymbole("лв")]
        [CurrencyLabel("BGN")]
        BGN = 975,

        /// <summary>
        /// Bahraini Dinar (BAHRAIN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BHD")]
        BHD = 048,

        /// <summary>
        /// Burundi Franc (BURUNDI)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BIF")]
        BIF = 108,

        /// <summary>
        /// Bermudian Dollar (BERMUDA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BMD")]
        BMD = 060,

        /// <summary>
        /// Brunei Dollar (BRUNEI DARUSSALAM)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BND")]
        BND = 096,

        /// <summary>
        /// Boliviano (BOLIVIA, PLURINATIONAL STATE OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BOB")]
        BOB = 068,

        /// <summary>
        /// Mvdol (BOLIVIA, PLURINATIONAL STATE OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BOV")]
        BOV = 984,

        /// <summary>
        /// Brazilian Real (BRAZIL)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BRL")]
        BRL = 986,

        /// <summary>
        /// Bahamian Dollar (BAHAMAS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BSD")]
        BSD = 044,

        /// <summary>
        /// Ngultrum  (BHUTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BTN")]
        BTN = 064,

        /// <summary>
        /// Pula (BOTSWANA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BWP")]
        BWP = 072,

        /// <summary>
        /// Belarussian Ruble (BELARUS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BYR")]
        BYR = 974,

        /// <summary>
        /// Belize Dollar (BELIZE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BZD")]
        BZD = 084,

        /// <summary>
        /// Canadian Dollar (CANADA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CAD")]
        CAD = 124,

        /// <summary>
        /// Congolese Franc (CONGO, THE DEMOCRATIC REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CDF")]
        CDF = 976,

        /// <summary>
        /// WIR Euro (SWITZERLAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CHE")]
        CHE = 947,

        /// <summary>
        /// Swiss Franc (SWITZERLAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CHF")]
        CHF = 756,

        /// <summary>
        /// WIR Franc (SWITZERLAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CHW")]
        CHW = 948,

        /// <summary>
        /// Unidades de fomento  (CHILE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CLF")]
        CLF = 990,

        /// <summary>
        /// Chilean Peso (CHILE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CLP")]
        CLP = 152,

        /// <summary>
        /// Yuan Renminbi (CHINA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CNY")]
        CNY = 156,

        /// <summary>
        /// Colombian Peso (COLOMBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("COP")]
        COP = 170,

        /// <summary>
        /// Unidad de Valor Real (COLOMBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("COU")]
        COU = 970,

        /// <summary>
        /// Costa Rican Colon (COSTA RICA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CRC")]
        CRC = 188,

        /// <summary>
        /// Peso Convertible (CUBA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CUC")]
        CUC = 931,

        /// <summary>
        /// Cuban Peso (CUBA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CUP")]
        CUP = 192,

        /// <summary>
        /// Cape Verde Escudo (CAPE VERDE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CVE")]
        CVE = 132,

        /// <summary>
        /// Czech Koruna (CZECH REPUBLIC)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CZK")]
        CZK = 203,

        /// <summary>
        /// Djibouti Franc (DJIBOUTI)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DJF")]
        DJF = 262,

        /// <summary>
        /// Danish Krone (DENMARK)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DKK")]
        DKK = 208,

        /// <summary>
        /// Dominican Peso (DOMINICAN REPUBLIC)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DOP")]
        DOP = 214,

        /// <summary>
        /// Algerian Dinar (ALGERIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DZD")]
        DZD = 012,

        /// <summary>
        /// Egyptian Pound (EGYPT)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("EGP")]
        EGP = 818,

        /// <summary>
        /// Nakfa (ERITREA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ERN")]
        ERN = 232,

        /// <summary>
        /// Ethiopian Birr (ETHIOPIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ETB")]
        ETB = 230,

        /// <summary>
        /// Euro (Europe)
        /// </summary>
        [CurrencySymbole("€")]
        [CurrencyLabel("EUR")]
        EUR = 978,

        /// <summary>
        /// Fiji Dollar (FIJI)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("FJD")]
        FJD = 242,

        /// <summary>
        /// Falkland Islands Pound (FALKLAND ISLANDS (MALVINAS))
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("FKP")]
        FKP = 238,

        /// <summary>
        /// Pound Sterling
        /// </summary>
        [CurrencySymbole("£")]
        [CurrencyLabel("GBP")]
        GBP = 826,

        /// <summary>
        /// Lari (GEORGIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GEL")]
        GEL = 981,

        /// <summary>
        /// Cedi (GHANA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GHS")]
        GHS = 936,

        /// <summary>
        /// Gibraltar Pound (GIBRALTAR)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GIP")]
        GIP = 292,

        /// <summary>
        /// Dalasi (GAMBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GMD")]
        GMD = 270,

        /// <summary>
        /// Guinea Franc (GUINEA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GNF")]
        GNF = 324,

        /// <summary>
        /// Quetzal (GUATEMALA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GTQ")]
        GTQ = 320,

        /// <summary>
        /// Guyana Dollar (GUYANA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GYD")]
        GYD = 328,

        /// <summary>
        /// Hong Kong Dollar (HONG KONG)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("HKD")]
        HKD = 344,

        /// <summary>
        /// Lempira (HONDURAS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("HNL")]
        HNL = 340,

        /// <summary>
        /// Croatian Kuna (CROATIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("HRK")]
        HRK = 191,

        /// <summary>
        /// Gourde (HAITI)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("HTG")]
        HTG = 332,

        /// <summary>
        /// Forint (HUNGARY)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("HUF")]
        HUF = 348,

        /// <summary>
        /// Rupiah (INDONESIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("IDR")]
        IDR = 360,

        /// <summary>
        /// New Israeli Sheqel (ISRAEL)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ILS")]
        ILS = 376,

        /// <summary>
        /// Indian Rupee (BHUTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("INR")]
        INR = 356,

        /// <summary>
        /// Iraqi Dinar (IRAQ)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("IQD")]
        IQD = 368,

        /// <summary>
        /// Iranian Rial (IRAN, ISLAMIC REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("IRR")]
        IRR = 364,

        /// <summary>
        /// Iceland Krona (ICELAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ISK")]
        ISK = 352,

        /// <summary>
        /// Jamaican Dollar (JAMAICA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("JMD")]
        JMD = 388,

        /// <summary>
        /// Jordanian Dinar (JORDAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("JOD")]
        JOD = 400,

        /// <summary>
        /// Yen (JAPAN) (ALT 0165)
        /// </summary>
        [CurrencySymbole("¥")]
        [CurrencyLabel("JPY")]
        JPY = 392,

        /// <summary>
        /// Kenyan Shilling (KENYA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KES")]
        KES = 404,

        /// <summary>
        /// Som (KYRGYZSTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KGS")]
        KGS = 417,

        /// <summary>
        /// Riel (CAMBODIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KHR")]
        KHR = 116,

        /// <summary>
        /// Comoro Franc (COMOROS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KMF")]
        KMF = 174,

        /// <summary>
        /// North Korean Won (KOREA, DEMOCRATIC PEOPLE’S REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KPW")]
        KPW = 408,

        /// <summary>
        /// Won (KOREA, REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KRW")]
        KRW = 410,

        /// <summary>
        /// Kuwaiti Dinar (KUWAIT)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KWD")]
        KWD = 414,

        /// <summary>
        /// Cayman Islands Dollar (CAYMAN ISLANDS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KYD")]
        KYD = 136,

        /// <summary>
        /// Tenge (KAZAKHSTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("KZT")]
        KZT = 398,

        /// <summary>
        /// Kip (LAO PEOPLE’S DEMOCRATIC REPUBLIC)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LAK")]
        LAK = 418,

        /// <summary>
        /// Lebanese Pound (LEBANON)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LBP")]
        LBP = 422,

        /// <summary>
        /// Sri Lanka Rupee (SRI LANKA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LKR")]
        LKR = 144,

        /// <summary>
        /// Liberian Dollar (LIBERIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LRD")]
        LRD = 430,

        /// <summary>
        /// Loti (LESOTHO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LSL")]
        LSL = 426,

        /// <summary>
        /// Lithuanian Litas (LITHUANIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LTL")]
        LTL = 440,

        /// <summary>
        /// Latvian Lats (LATVIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LVL")]
        LVL = 428,

        /// <summary>
        /// Libyan Dinar (LIBYAN ARAB JAMAHIRIYA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LYD")]
        LYD = 434,

        /// <summary>
        /// Moroccan Dirham (MOROCCO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MAD")]
        MAD = 504,

        /// <summary>
        /// Moldovan Leu (MOLDOVA, REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MDL")]
        MDL = 498,

        /// <summary>
        /// Malagasy Ariary (MADAGASCAR)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MGA")]
        MGA = 969,

        /// <summary>
        /// Denar (MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MKD")]
        MKD = 807,

        /// <summary>
        /// Kyat (MYANMAR)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MMK")]
        MMK = 104,

        /// <summary>
        /// Tugrik (MONGOLIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MNT")]
        MNT = 496,

        /// <summary>
        /// Pataca (MACAO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MOP")]
        MOP = 446,

        /// <summary>
        /// Ouguiya (MAURITANIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MRO")]
        MRO = 478,

        /// <summary>
        /// Mauritius Rupee (MAURITIUS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MUR")]
        MUR = 480,

        /// <summary>
        /// Rufiyaa (MALDIVES)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MVR")]
        MVR = 462,

        /// <summary>
        /// Kwacha (MALAWI)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MWK")]
        MWK = 454,

        /// <summary>
        /// Mexican Peso (MEXICO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MXN")]
        MXN = 484,

        /// <summary>
        /// Mexican Unidad de Inversion (UDI) (MEXICO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MXV")]
        MXV = 979,

        /// <summary>
        /// Malaysian Ringgit (MALAYSIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MYR")]
        MYR = 458,

        /// <summary>
        /// Metical (MOZAMBIQUE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MZN")]
        MZN = 943,

        /// <summary>
        /// Namibia Dollar (NAMIBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NAD")]
        NAD = 516,

        /// <summary>
        /// Naira (NIGERIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NGN")]
        NGN = 566,

        /// <summary>
        /// Cordoba Oro (NICARAGUA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NIO")]
        NIO = 558,

        /// <summary>
        /// Norwegian Krone
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NOK")]
        NOK = 578,

        /// <summary>
        /// Nepalese Rupee (NEPAL)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NPR")]
        NPR = 524,

        /// <summary>
        /// New Zealand Dollar
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NZD")]
        NZD = 554,

        /// <summary>
        /// Rial Omani (OMAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("OMR")]
        OMR = 512,

        /// <summary>
        /// Balboa (PANAMA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PAB")]
        PAB = 590,

        /// <summary>
        /// Nuevo Sol (PERU)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PEN")]
        PEN = 604,

        /// <summary>
        /// Kina (PAPUA NEW GUINEA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PGK")]
        PGK = 598,

        /// <summary>
        /// Philippine Peso (PHILIPPINES)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PHP")]
        PHP = 608,

        /// <summary>
        /// Pakistan Rupee (PAKISTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PKR")]
        PKR = 586,

        /// <summary>
        /// Zloty (POLAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PLN")]
        PLN = 985,

        /// <summary>
        /// Guarani (PARAGUAY)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PYG")]
        PYG = 600,

        /// <summary>
        /// Qatari Rial (QATAR)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("QAR")]
        QAR = 634,

        /// <summary>
        /// Leu (ROMANIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("RON")]
        RON = 946,

        /// <summary>
        /// Serbian Dinar (SERBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("RSD")]
        RSD = 941,

        /// <summary>
        /// Russian Ruble (RUSSIAN FEDERATION)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("RUB")]
        RUB = 643,

        /// <summary>
        /// Rwanda Franc (RWANDA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("RWF")]
        RWF = 646,

        /// <summary>
        /// Saudi Riyal (SAUDI ARABIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SAR")]
        SAR = 682,

        /// <summary>
        /// Solomon Islands Dollar (SOLOMON ISLANDS)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SBD")]
        SBD = 090,

        /// <summary>
        /// Seychelles Rupee (SEYCHELLES)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SCR")]
        SCR = 690,

        /// <summary>
        /// Sudanese Pound (SUDAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SDG")]
        SDG = 938,

        /// <summary>
        /// Swedish Krona (SWEDEN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SEK")]
        SEK = 752,

        /// <summary>
        /// Singapore Dollar (SINGAPORE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SGD")]
        SGD = 702,

        /// <summary>
        /// Saint Helena Pound (SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SHP")]
        SHP = 654,

        /// <summary>
        /// Leone (SIERRA LEONE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SLL")]
        SLL = 694,

        /// <summary>
        /// Somali Shilling (SOMALIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SOS")]
        SOS = 706,

        /// <summary>
        /// Surinam Dollar (SURINAME)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SRD")]
        SRD = 968,

        /// <summary>
        /// Dobra (SÃO TOME AND PRINCIPE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("STD")]
        STD = 678,

        /// <summary>
        /// Syrian Pound (SYRIAN ARAB REPUBLIC)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SYP")]
        SYP = 760,

        /// <summary>
        /// Lilangeni (SWAZILAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SZL")]
        SZL = 748,

        /// <summary>
        /// Baht (THAILAND)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("THB")]
        THB = 764,

        /// <summary>
        /// Somoni (TAJIKISTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TJS")]
        TJS = 972,

        /// <summary>
        /// New Manat (TURKMENISTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TMT")]
        TMT = 934,

        /// <summary>
        /// Tunisian Dinar (TUNISIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TND")]
        TND = 788,

        /// <summary>
        /// Pa’anga (TONGA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TOP")]
        TOP = 776,

        /// <summary>
        /// Turkish Lira (TURKEY)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TRY")]
        TRY = 949,

        /// <summary>
        /// Trinidad and Tobago Dollar (TRINIDAD AND TOBAGO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TTD")]
        TTD = 780,

        /// <summary>
        /// New Taiwan Dollar (TAIWAN, PROVINCE OF CHINA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TWD")]
        TWD = 901,

        /// <summary>
        /// Tanzanian Shilling (TANZANIA, UNITED REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TZS")]
        TZS = 834,

        /// <summary>
        /// Hryvnia (UKRAINE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UAH")]
        UAH = 980,

        /// <summary>
        /// Uganda Shilling (UGANDA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UGX")]
        UGX = 800,

        /// <summary>
        /// US Dollar
        /// </summary>
        [CurrencySymbole("$")]
        [CurrencyLabel("USD")]
        USD = 840,

        /// <summary>
        /// US Dollar (Next day)  (UNITED STATES)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("USN")]
        USN = 997,

        /// <summary>
        /// US Dollar (Same day)  (UNITED STATES)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("USS")]
        USS = 998,

        /// <summary>
        /// Uruguay Peso en Unidades Indexadas (URUIURUI) (URUGUAY)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UYI")]
        UYI = 940,

        /// <summary>
        /// Peso Uruguayo (URUGUAY)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UYU")]
        UYU = 858,

        /// <summary>
        /// Uzbekistan Sum (UZBEKISTAN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UZS")]
        UZS = 860,

        /// <summary>
        /// Bolivar Fuerte (VENEZUELA, BOLIVARIAN REPUBLIC OF)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("VEF")]
        VEF = 937,

        /// <summary>
        /// Dong (VIET NAM)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("VND")]
        VND = 704,

        /// <summary>
        /// Vatu (VANUATU)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("VUV")]
        VUV = 548,

        /// <summary>
        /// Tala (SAMOA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("WST")]
        WST = 882,

        /// <summary>
        /// CFA Franc BEAC  (CAMEROON)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XAF")]
        XAF = 950,

        /// <summary>
        /// Silver (ZZ11_Silver)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XAG")]
        XAG = 961,

        /// <summary>
        /// Gold (ZZ08_Gold)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XAU")]
        XAU = 959,

        /// <summary>
        /// Bond Markets Unit European Composite Unit (EURCO) (ZZ01_Bond Markets Unit European_EURCO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XBA")]
        XBA = 955,

        /// <summary>
        /// Bond Markets Unit European Monetary Unit (E.M.U.-6)  (ZZ02_Bond Markets Unit European_EMU-6)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XBB")]
        XBB = 956,

        /// <summary>
        /// Bond Markets Unit European Unit of Account 9 (E.U.A.-9) (ZZ03_Bond Markets Unit European_EUA-9)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XBC")]
        XBC = 957,

        /// <summary>
        /// Bond Markets Unit European Unit of Account 17 (E.U.A.-17) (ZZ04_Bond Markets Unit European_EUA-17)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XBD")]
        XBD = 958,

        /// <summary>
        /// East Caribbean Dollar (ANGUILLA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XCD")]
        XCD = 951,

        /// <summary>
        /// SDR (Special Drawing Right) (INTERNATIONAL MONETARY FUND (IMF) )
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XDR")]
        XDR = 960,

        /// <summary>
        /// CFA Franc BCEAO  (BENIN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XOF")]
        XOF = 952,

        /// <summary>
        /// Palladium (ZZ09_Palladium)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XPD")]
        XPD = 964,

        /// <summary>
        /// CFP Franc (FRENCH POLYNESIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XPF")]
        XPF = 953,

        /// <summary>
        /// Platinum (ZZ10_Platinum)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XPT")]
        XPT = 962,

        /// <summary>
        /// Sucre (SISTEMA UNITARIO DE COMPENSACION REGIONAL DE PAGOS SUCRE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XSU")]
        XSU = 994,

        /// <summary>
        /// Codes specifically reserved for testing purposes (ZZ06_Testing_Code)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XTS")]
        XTS = 963,

        /// <summary>
        /// ADB Unit of Account (MEMBER COUNTRIES OF THE AFRICAN DEVELOPMENT BANK GROUP)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XUA")]
        XUA = 965,

        /// <summary>
        /// The codes assigned for transactions where no currency is involved (ZZ07_No_Currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XXX")]
        XXX = 999,

        /// <summary>
        /// Yemeni Rial (YEMEN)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("YER")]
        YER = 886,

        /// <summary>
        /// Rand (LESOTHO)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZAR")]
        ZAR = 710,

        /// <summary>
        /// Zambian Kwacha (ZAMBIA)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZMK")]
        ZMK = 894,

        /// <summary>
        /// Zimbabwe Dollar (ZIMBABWE)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZWL")]
        ZWL = 932,

        #region Historical Unit

        /// <summary>
        /// Andorran peseta (1:1 peg to the Spanish peseta) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ADP")]
        ADP = 20,

        /// <summary>
        /// Austrian schilling (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ATS")]
        ATS = 40,

        /// <summary>
        /// Belgian franc (currency union with LUF) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BEF")]
        BEF = 56,

        /// <summary>
        /// Cypriot pound (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CYP")]
        CYP = 196,

        /// <summary>
        /// German mark (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DEM")]
        DEM = 276,

        /// <summary>
        /// Estonian kroon (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("EEK")]
        EEK = 233,

        /// <summary>
        /// Spanish peseta (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ESP")]
        ESP = 724,

        /// <summary>
        /// Finnish markka (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("FIM")]
        FIM = 246,

        /// <summary>
        /// French franc (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("FRF")]
        FRF = 250,

        /// <summary>
        /// Greek drachma (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GRD")]
        GRD = 300,

        /// <summary>
        /// Irish pound (punt in Irish language) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("IEP")]
        IEP = 372,

        /// <summary>
        /// Italian lira (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ITL")]
        ITL = 380,

        /// <summary>
        /// Luxembourg franc (currency union with BEF) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("LUF")]
        LUF = 442,

        /// <summary>
        /// Maltese lira (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MTL")]
        MTL = 470,

        /// <summary>
        /// Netherlands guilder (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("NLG")]
        NLG = 528,

        /// <summary>
        /// Portuguese escudo (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PTE")]
        PTE = 620,

        /// <summary>
        /// Slovenian tolar (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SIT")]
        SIT = 705,

        /// <summary>
        /// Slovak koruna (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SKK")]
        SKK = 703,

        /// <summary>
        /// European Currency Unit (1 XEU = 1 EUR) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("XEU")]
        XEU = 954,

        /// <summary>
        /// Afghan afghani (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("AFA")]
        AFA = 4,

        /// <summary>
        /// Angolan new kwanza (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("AON")]
        AON = 24,

        /// <summary>
        /// Angolan kwanza readjustado (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("AOR")]
        AOR = 982,

        /// <summary>
        /// Azerbaijani manat (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("AZM")]
        AZM = 31,

        /// <summary>
        /// Bulgarian lev A/99 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("BGL")]
        BGL = 100,

        /// <summary>
        /// Serbian dinar (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CSD")]
        CSD = 891,

        /// <summary>
        /// Czechoslovak koruna (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("CSK")]
        CSK = 200,

        /// <summary>
        /// East German Mark of the GDR (East Germany) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("DDM")]
        DDM = 278,

        /// <summary>
        /// Ecuadorian sucre (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ECS")]
        ECS = 218,

        /// <summary>
        /// Ecuador Unidad de Valor Constante (funds code) (discontinued) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ECV")]
        ECV = 983,

        /// <summary>
        /// Spanish peseta (account A) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ESA")]
        ESA = 996,

        /// <summary>
        /// Spanish peseta (account B) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ESB")]
        ESB = 995,

        /// <summary>
        /// Ghanaian cedi (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GHC")]
        GHC = 288,

        /// <summary>
        /// Guinea-Bissau peso (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("GWP")]
        GWP = 624,

        /// <summary>
        /// Malagasy franc (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MGF")]
        MGF = 450,

        /// <summary>
        /// Mozambican metical (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("MZM")]
        MZM = 508,

        /// <summary>
        /// Polish zloty A/94 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("PLZ")]
        PLZ = 616,

        /// <summary>
        /// Romanian leu A/05 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ROL")]
        ROL = 642,

        /// <summary>
        /// Russian rouble A/97 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("RUR")]
        RUR = 810,

        /// <summary>
        /// Sudanese dinar (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SDD")]
        SDD = 736,

        //// <summary>
        //// Sudanese old pound (historical currency)
        //// </summary>
        //// [CurrencySymbole("")]
        //// SDP = 736, // Duplicate numeric code with 'Sudanese dinar' (the initial code is duplicate see http://en.wikipedia.org/wiki/ISO_4217)

        /// <summary>
        /// Suriname guilder (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SRG")]
        SRG = 740,

        /// <summary>
        /// Salvadoran colón (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("SVC")]
        SVC = 222,

        /// <summary>
        /// Tajikistani ruble (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TJR")]
        TJR = 762,

        /// <summary>
        /// Turkmenistani manat (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TMM")]
        TMM = 795,

        /// <summary>
        /// Turkish lira A/05 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("TRL")]
        TRL = 792,

        /// <summary>
        /// Ukrainian karbovanets (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("UAK")]
        UAK = 804,

        /// <summary>
        /// Venezuelan bolívar (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("VEB")]
        VEB = 862,

        /// <summary>
        /// South Yemeni dinar (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("YDD")]
        YDD = 720,

        //// <summary>
        //// Yugoslav dinar (historical currency)
        //// </summary>
        //// [CurrencySymbole("")]
        //// YUM = 891, // Duplicate numeric code with 'Serbian dinar' (the initial code is duplicate see http://en.wikipedia.org/wiki/ISO_4217)

        /// <summary>
        /// South African financial rand (funds code) (discontinued) (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZAL")]
        ZAL = 991,

        /// <summary>
        /// Zaïrean new zaïre (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZRN")]
        ZRN = 180,

        /// <summary>
        /// Zimbabwean dollar A/06 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZWD")]
        ZWD = 716,

        /// <summary>
        /// Zimbabwean dollar A/08 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZWN")]
        ZWN = 942,

        /// <summary>
        /// Zimbabwean dollar A/09 (historical currency)
        /// </summary>
        [CurrencySymbole("")]
        [CurrencyLabel("ZWR")]
        ZWR = 935,

        #endregion
    }
}
