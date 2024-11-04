CREATE TABLE [dbo].[MonoFlowers] (
    [MonoFlowerId]              INT             IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX)  NOT NULL,
    [RecievementDate] DATETIME2 (7)   NOT NULL,
    [Price]           DECIMAL (18, 2) NOT NULL,
    [Colour]          NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_MonoFlowers] PRIMARY KEY CLUSTERED ([MonoFlowerId] ASC)
);

