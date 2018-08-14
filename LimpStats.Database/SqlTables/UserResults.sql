CREATE TABLE [dbo].[UserResults] (
    [Username]           NVARCHAR (25) NOT NULL,
    [SolvedProblemCount] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Username] ASC)
);