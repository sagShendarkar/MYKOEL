import { Validators } from "@angular/forms";
import { HttpHeaders } from "@angular/common/http";

export class GlobalConstants {


//This header options is required to get data and headers from the http post.
public static HttpPostOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    observe: "response" as "body",
    responseType: 'json' as "json"
};

public static languages = [
  {
    id: 'ktl',
    name: 'KloudQ',
    // flag: './assets/media/flags/india.svg',
    additional: {
      image: './assets/media/flags/india.svg',
      winner: '1'
    }
  },
  {
    id: 'hi',
    name: 'Hindi',
    // flag: './assets/media/flags/india.svg',
    additional: {
      image: './assets/media/flags/france.svg',
      winner: '2'
    }
  },
  {
    id: 'ktltest',
    name: 'KTLTest',
    // flag: './assets/media/flags/india.svg',
    additional: {
      image: './assets/media/flags/united-states.svg',
      winner: '3'
    }
  },
];
  public static mapApiKey: string='AIzaSyCBxL92SJXtQdjI3RVCCoxuZp1J-3-NGOg';
  public static MonacoHowerLoaded: boolean = false;
  public static MonacoCompletionItemProviderLoaded: boolean = false;
  public static SOCReportFleetFilterDuration: number = 7;
  // public static parameterService: ParameterService;
  public parameterString: string;
  public static paginationDefParams={
    searchPagination:'',
    location:'',
    flag:'',
    date:'',
    searchPaginationFota:'',
    pageNumber: 1,
    pageSize:10
  }

  public static COMPANYLOGOFOLDER ="companyLogos/";
  public static MACHINEIMGFOLDER ="machineImages/";
  public static ATTACHMENTSFOLDER ="attachments/";
  public static LOGOPATH ="./assets/media/new_assets/logos/";
  public static DEFAULT_BG_IMG ="Loginpage_BG.png";
  public static DEFAULT_LOGO_IMG ="TorLoader.png";

  public static iconBaseUrl="assets/media/new_assets/icons/log9_icons/Icons-live-map-log9/";
  public static iconBaseUrl1="assets/media/new_assets/icons/log9_icons/";
  public static DEBOUNCETIME = 400;
  // constructor(private parameterService: ParameterService)
  // {
  //   console.log("Heyyy");

  //     this.parameterService.getparameterName();
  //     GlobalConstants.codeForValidation= GlobalConstants.codeForValidation+this.parameterString;
  //     console.log(this.parameterService.getparameterName());
  // }

public static codeForValidation: string = `using System;
using iot_Domain.Repositories;
using Infrastructure.Caching;
using iot_Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;
public class MyClass
{
	Globals1 data = new Globals1();
	public void MyMethod(int value)
	{
    {{CODEREPLACE}}
	}
	public int Get(int position){return 0;}
	public int GetInt(int position){return 0;}
	public decimal getHextoDecimal(int position1, int position2, int position3, int position4){return 0;}
	public ICacheBase cache;
	public IAlertDataRepository alert;
	public string[] ArryPostString;
  public Dictionary<uint, UInt64> keyValuePairs;
  public string GetString(int position){return "";}
  public string GetUTCDate(){return "";}
  public static string HexToBin(string hexString){return "";}
  public decimal HextToDec(int position1, int position2, int position3, int position4){return 0;}
  public decimal HexToDecimal(int position1){return 0;}
  public decimal HexToDecimal(int position1, int position2){return 0;}
  public decimal HextToDec(int position1, int position2){return 0;}
  public decimal HextToDec(int position1){return 0;}
  public string byteHextoBinary(int position){return "";}
  public string byteHextoBinary(int position,int getPosition){return "";}
  public int binaryTODecimal(string binary){return 0;}
  public int binaryTODecimal(string binary,int startIndex, int length){return 0;}
  public double GetFt(int high, int low){return 0;}
  public double getFloatFromTwo16(int position_1, int position_2){return 0;}
  public double GetDistance(Location pos1, Location pos2){return 0;}
  public double GetOdometerByLatLong(string Latitude, string Longitude){return 0;}
  public double RxSignalUnpack(UInt64 RxMsg64, Signal signal){return 0;}
}
public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public Location(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
    }
    public class Signal
    {


        public uint ID { get; set; }
        public string Name { get; set; }
        public ushort StartBit { get; set; }
        public ushort Length { get; set; }
        public byte ByteOrder { get; set; }
        public byte IsSigned { get; set; } = 1;

        public double InitialValue { get; set; }
        public double Factor { get; set; }
        public bool IsInteger { get; set; }
        public double Offset { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public string Unit { get; set; }
        public string[] Receiver { get; set; }
        public string ValueTable { get; private set; }
        public string Comment { get; set; }
        public string Multiplexing { get; set; }


        public Signal(uint _ID,
        string _Name,
        ushort _StartBit,
        ushort _Length,
        byte _ByteOrder,
        byte _IsSigned,
        double _InitialValue,
        double _Factor,
        bool _IsInteger,
        double _Offset,
        double _Minimum,
        double _Maximum)
        {

            ID = _ID;
            Name = _Name;
            StartBit = _StartBit;
            Length = _Length;
            ByteOrder = _ByteOrder;
            IsSigned = _IsSigned; ;
            InitialValue = _InitialValue;
            Factor = _Factor;
            IsInteger = _IsInteger;
            Offset = _Offset;
            Minimum = _Minimum;
            Maximum = _Maximum;


        }
    }
`;


}



export const FIELD_VALIDATIONS = {
  NUMERIC_ONLY: Validators.pattern((/^[0-9]*$/)),
  ALPHABETS_ONLY: Validators.pattern((/^[a-zA-Z ]*$/)),
  ALPHANUMERIC_ONLY: Validators.pattern((/^([a-zA-Z0-9 ]+)$/)),
  EMAIL_ONLY: Validators.pattern(('^[A-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$')),
  NO_BLANK: Validators.pattern(/^(\s+\S+\s*)*(?!\s).*$/),
  AMOUNT: Validators.pattern(('^\d+(\.\d{1,2})?$')),
  PASSWORD: Validators.pattern(( /(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*#?&^_-]).{6,}/)),
 CONTACT_NUMBER:Validators.pattern((/^[0-9]{10}$/)),
 NAME:Validators.pattern((/^(\s+\S+\s*)*(?!\s).*$/))
};

export const BLANKPROFILEIMAGE ="./assets/media/svg/avatars/blank.svg";
export const PROFILEPATH ="profile_images/";
export const DEFAULTPROFILEIMAGE='./assets/media/new_assets/avatars/profile.png';

export const LOG9INCEPTIONDATE = '2023-05-01';
