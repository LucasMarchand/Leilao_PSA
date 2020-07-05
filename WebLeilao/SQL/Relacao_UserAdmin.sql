USE [DBLeilao]
GO

INSERT INTO AspNetRoles (Id,Name) VALUES (1, 'Admin');

INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('f6098e9b-6a2b-43b9-93b3-20a07127c1bd', 1)