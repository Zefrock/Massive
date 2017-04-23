CREATE TABLE [dbo].[AdjacentNodes]
(
	[IdNode1] INT NOT NULL , 
    [IdNode2] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([IdNode1], [IdNode2]),
	CONSTRAINT [FK_AdjacentNodes_ToTable_IdNode1] FOREIGN KEY ([IdNode1]) REFERENCES [Nodes]([Id]),
    CONSTRAINT [FK_AdjacentNodes_ToTable_IdNode2] FOREIGN KEY ([IdNode2]) REFERENCES [Nodes]([Id]),
)

