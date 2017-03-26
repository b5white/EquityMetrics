USE [StockPicker]
GO

/****** Object:  Table [dbo].[StockQuotes]    Script Date: 1/28/2017 11:24:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StockQuotes](
	[StockId] [int] NOT NULL,
	[AdjNonAdjFlag] [bit] NULL,
	[AnnualDividend] [float] NULL,
	[Ask] [float] NULL,
	[AskExchange] [char](5) NULL,
	[AskSize] [bigint] NULL,
	[AskTime] [datetime] NULL,
	[Beta] [float] NULL,
	[Bid] [float] NULL,
	[BidExchange] [char](5) NULL,
	[BidSize] [bigint] NULL,
	[BidTime] [datetime] NULL,
	[ChgClose] [float] NULL,
	[ChgClosePrcn] [float] NULL,
	[CompanyName] [char](40) NULL,
	[DaysToExpiration] [bigint] NULL,
	[DirLast] [char](10) NULL,
	[Dividend] [float] NULL,
	[Eps] [float] NULL,
	[EstEarnings] [float] NULL,
	[ExDivDate] [char](15) NULL,
	[ExchgLastTrade] [char](5) NULL,
	[Fsi] [char](1) NULL,
	[High] [float] NULL,
	[High52] [float] NULL,
	[HighAsk] [float] NULL,
	[HighBid] [float] NULL,
	[LastTrade] [float] NULL,
	[Low] [float] NULL,
	[Low52] [float] NULL,
	[LowAsk] [float] NULL,
	[LowBid] [float] NULL,
	[NumTrades] [bigint] NULL,
	[Open] [float] NULL,
	[OpenInterest] [bigint] NULL,
	[OptionStyle] [char](10) NULL,
	[OptionUnderlier] [char](8) NULL,
	[PrevClose] [float] NULL,
	[PrevDayVolume] [bigint] NULL,
	[PrimaryExchange] [char](5) NULL,
	[SymbolDesc] [char](25) NULL,
	[TodayClosed] [float] NULL,
	[TotalVolume] [bigint] NULL,
	[Upc] [bigint] NULL,
	[Volume10Day] [bigint] NULL,
	[DateTime] [datetime] NOT NULL,
	[Symbol] [char](8) NOT NULL,
	[Type] [char](5) NULL,
	[Exchange] [char](5) NULL
) ON [PRIMARY]

GO


