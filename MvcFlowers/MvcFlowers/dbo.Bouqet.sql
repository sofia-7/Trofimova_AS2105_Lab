CREATE TABLE [dbo].[Bouqet] (
    [BouqetId]                INT            IDENTITY (1, 1) NOT NULL,
    [SelectedFlowerIds] NVARCHAR (MAX) DEFAULT (N'[]') NOT NULL,
    CONSTRAINT [PK_Bouqet] PRIMARY KEY CLUSTERED ([BouqetId] ASC)
);

