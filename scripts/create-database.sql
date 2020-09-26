USE [master]
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DomainHistoryTest')
BEGIN
CREATE DATABASE [DomainHistoryTest]
	ON
	( NAME = DGBar_dat,
		FILENAME = 'C:\Bancos\DomainHistoryTestdat.mdf',
		SIZE = 10,
		MAXSIZE = 50,
		FILEGROWTH = 5 )
	LOG ON
	( NAME = Sales_log,
		FILENAME = 'C:\Bancos\DomainHistoryTestlog.ldf',
		SIZE = 5MB,
		MAXSIZE = 25MB,
		FILEGROWTH = 5MB )
END
GO

USE [DomainHistoryTest]
GO