IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OtoparkDB')
BEGIN
    CREATE DATABASE OtoparkDB;
END
GO

USE OtoparkDB;
GO 