﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY(0,1), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
)