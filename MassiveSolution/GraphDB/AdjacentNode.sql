CREATE TABLE [dbo].[AdjacentNode]
(
	[NodeId] INT NOT NULL, 
    [AdjacentNodeId] INT NOT NULL,
	PRIMARY KEY CLUSTERED ([NodeId], [AdjacentNodeId]),
	CONSTRAINT [FK_AdjacentNode_ToTable_NodeId] FOREIGN KEY ([NodeId]) REFERENCES [Node]([NodeId]) ON DELETE CASCADE
)

