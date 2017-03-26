-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StockQuotes_GetBySymbol]
       @Symbol          char(6)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT 
        StockId,
        AdjNonAdjFlag,
        AnnualDividend,
        Ask,
        AskExchange,
        AskSize,
        AskTime,
        Beta,
        Bid,
        BidExchange,
        BidSize,
        BidTime,
        ChgClose,
        ChgClosePrcn,
        CompanyName,
        DaysToExpiration,
        DirLast,
        Dividend,
        Eps,
        EstEarnings,
        ExDivDate,
        ExchgLastTrade,
        Fsi,
        High,
        High52,
        HighAsk,
        HighBid,
        LastTrade,
        Low,
        Low52,
        LowAsk,
        LowBid,
        NumTrades,
        [Open],
        OpenInterest,
        OptionStyle,
        OptionUnderlier,
        PrevClose,
        PrevDayVolume,
        PrimaryExchange,
        SymbolDesc,
        TodayClosed,
        TotalVolume,
        Upc,
        Volume10Day,
        DateTime,
        Symbol,
        Type,
        Exchange
     FROM [StockPicker].[dbo].[StockQuotes]
  WHERE Symbol = @Symbol

END
