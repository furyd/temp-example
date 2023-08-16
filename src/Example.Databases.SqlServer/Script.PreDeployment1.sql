/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF SUSER_ID (N'Example') IS NULL
BEGIN
    CREATE LOGIN [Example] WITH PASSWORD=N'Example', DEFAULT_DATABASE=[Example], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END

IF DATABASE_PRINCIPAL_ID('db_executor') IS NULL
BEGIN
    CREATE ROLE db_executor
END

GRANT EXECUTE TO db_executor

EXEC sp_addrolemember 'db_executor','Example'