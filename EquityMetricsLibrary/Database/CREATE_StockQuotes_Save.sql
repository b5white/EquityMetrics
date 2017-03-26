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
CREATE PROCEDURE StockQuote_Save
           @aStockId int,
           @aAdjNonAdjFlag bit,
           @aAnnualDividend float,
           @aAsk float,
           @aAskExchange char(5),
           @aAskSize bigint,
           @aAskTime datetime,
           @aBeta float,
           @aBid float,
           @aBidExchange char(5),
           @aBidSize bigint,
           @aBidTime datetime,
           @aChgClose float,
           @aChgClosePrcn float,
           @aCompanyName char(40),
           @aDaysToExpiration bigint,
           @aDirLast char(10),
           @aDividend float,
           @aEps float,
           @aEstEarnings float,
           @aExDivDate char(15),
           @aExchgLastTrade char(5),
           @aFsi char(1),
           @aHigh float,
           @aHigh52 float,
           @aHighAsk float,
           @aHighBid float,
           @aLastTrade float,
           @aLow float,
           @aLow52 float,
           @aLowAsk float,
           @aLowBid float,
           @aNumTrades bigint,
           @aOpen float,
           @aOpenInterest bigint,
           @aOptionStyle char(10),
           @aOptionUnderlier char(8),
           @aPrevClose float,
           @aPrevDayVolume bigint,
           @aPrimaryExchange char(5),
           @aSymbolDesc char(25),
           @aTodayClosed float,
           @aTotalVolume bigint,
           @aUpc bigint,
           @aVolume10Day bigint,
           @aDateTime datetime,
           @aSymbol char(8),
           @aType char(5),
           @aExchange char(5)

AS
BEGIN
	-- SET NOCOUNT ON @added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [StockPicker].[dbo].[StockQuotes]
           ([StockId],
            [AdjNonAdjFlag],
            [AnnualDividend],
            [Ask],
            [AskExchange],
            [AskSize],
            [AskTime],
            [Beta],
            [Bid],
            [BidExchange],
            [BidSize],
            [BidTime],
            [ChgClose],
            [ChgClosePrcn],
            [CompanyName],
            [DaysToExpiration],
            [DirLast],
            [Dividend],
            [Eps],
            [EstEarnings],
            [ExDivDate],
            [ExchgLastTrade],
            [Fsi],
            [High],
            [High52],
            [HighAsk],
            [HighBid],
            [LastTrade],
            [Low],
            [Low52],
            [LowAsk],
            [LowBid],
            [NumTrades],
            [Open],
            [OpenInterest],
            [OptionStyle],
            [OptionUnderlier],
            [PrevClose],
            [PrevDayVolume],
            [PrimaryExchange],
            [SymbolDesc],
            [TodayClosed],
            [TotalVolume],
            [Upc],
            [Volume10Day],
            [DateTime],
            [Symbol],
            [Type],
            [Exchange])
     VALUES
           (@aStockId,
             @aAdjNonAdjFlag,
             @aAnnualDividend,
             @aAsk,
             @aAskExchange,
             @aAskSize,
             @aAskTime,
             @aBeta,
             @aBid,
             @aBidExchange,
             @aBidSize,
             @aBidTime,
             @aChgClose,
             @aChgClosePrcn,
             @aCompanyName,
             @aDaysToExpiration,
             @aDirLast,
             @aDividend,
             @aEps,
             @aEstEarnings,
             @aExDivDate,
             @aExchgLastTrade,
             @aFsi,
             @aHigh,
             @aHigh52,
             @aHighAsk,
             @aHighBid,
             @aLastTrade,
             @aLow,
             @aLow52,
             @aLowAsk,
             @aLowBid,
             @aNumTrades,
             @aOpen,
             @aOpenInterest,
             @aOptionStyle,
             @aOptionUnderlier,
             @aPrevClose,
             @aPrevDayVolume,
             @aPrimaryExchange,
             @aSymbolDesc,
             @aTodayClosed,
             @aTotalVolume,
             @aUpc,
             @aVolume10Day,
             @aDateTime,
             @aSymbol,
             @aType,
             @aExchange);
END
GO
