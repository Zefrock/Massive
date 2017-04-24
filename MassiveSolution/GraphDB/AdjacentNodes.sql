CREATE TABLE [dbo].[AdjacentNodes]
(
	[NodeId] INT NOT NULL , 
    [AdjacentNodeId] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([NodeId], [AdjacentNodeId]),
	CONSTRAINT [FK_AdjacentNodes_ToTable_NodeId] FOREIGN KEY ([NodeId]) REFERENCES [Nodes]([NodeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdjacentNodes_ToTable_AdjacentNodeId] FOREIGN KEY ([AdjacentNodeId]) REFERENCES [Nodes]([NodeId]),
)

