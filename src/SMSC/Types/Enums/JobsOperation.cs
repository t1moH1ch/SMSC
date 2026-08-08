namespace SMSC.Types.Enums;

public enum JobsOperation
{
    Add,
    Delete,
    Cancel,
    Get,
    GetAll
}

public enum JobRepeat
{
    None = 0,
    Daily = 1,
    Weekly = 2,
    Monthly = 3,
    Quarterly = 4,
    Yearly = 5,
    Hourly = 6,
    EveryMinute = 7
}
