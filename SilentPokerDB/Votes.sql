﻿CREATE TABLE [dbo].[Votes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StoryId] VARCHAR(32) NOT NULL, 
    [UserId] VARCHAR(32) NOT NULL, 
    [VoteValue] INT NOT NULL
)
