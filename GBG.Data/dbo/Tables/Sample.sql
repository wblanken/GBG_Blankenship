CREATE TABLE [dbo].[Sample]
(
	[Id] INT NOT NULL IDENTITY(0,1), 
    [Barcode] NVARCHAR(50) NOT NULL , 
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[CreatedBy] INT NOT NULL,
	[StatusId] INT NOT NULL, 
    CONSTRAINT [PK_Sample] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sample_User] FOREIGN KEY ([CreatedBy]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Sample_Status] FOREIGN KEY ([StatusId]) REFERENCES [Status]([Id]), 
)
