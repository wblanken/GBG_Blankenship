﻿CREATE TABLE [dbo].[Status]
(
	[Id] INT NOT NULL IDENTITY(0,1), 
    [Status] NVARCHAR(50) NOT NULL
    CONSTRAINT [PK_Status] PRIMARY KEY ([Id])
)
