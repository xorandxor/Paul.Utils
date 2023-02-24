namespace Paul.Utils
{
    #region enums plurbus

    /// <summary>
    /// Self explanatory x
    /// </summary>
    public enum BuyOrSellType
    {
        NotSet = 0,
        Buy = 1,
        Sell = 2
    }

    /// <summary>
    /// conditional close order
    /// default is notset and if not set==true then this is
    /// excluded from what is submitted to the api
    /// </summary>
    public enum KrakenCloseOrderType
    {
        NotSet = 0,
        Limit = 1,
        StopLoss = 2,
        TakeProfit = 3,
        StopLossLimit = 4,
        TakeProfitLimit = 5
    }

    /// <summary>
    /// OrderType is mandatory and if notset==true then
    /// an exception is thrown
    /// </summary>
    public enum KrakenOrderType
    {
        NotSet = 0,
        Market = 1,
        Limit = 2,
        StopLoss = 3,
        TakeProfit = 4,
        StopLossLimit = 5,
        TakeProfitLimit = 6,
        SettlePosition = 7
    }

    /// <summary>
    /// Margin level requested
    /// note: must be a multimillionaire to use this feature
    /// (cocksuckers)
    /// </summary>
    public enum LeverageLevel
    {
        None = 0,
        TwoToOne = 1,
        ThreeToOne = 2,
        FourToOne = 3,
        FiveToOne = 4
    }

    #endregion enums plurbus
}