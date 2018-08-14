CREATE TABLE [dbo].[Members] (
    [Group]    NVARCHAR (25) NOT NULL,
    [Username] NVARCHAR (25) NOT NULL,
    PRIMARY KEY CLUSTERED ([Group] ASC, [Username] ASC)
);