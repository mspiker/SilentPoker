CREATE TABLE [dbo].[Rooms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(40) NOT NULL, 
    [Sprint] VARCHAR(32) NOT NULL, 
    [Filter] VARCHAR(256) NULL, 
    [AllowPass] BIT NULL
)
