IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DocumentManagerDB')
BEGIN
  CREATE DATABASE DocumentManagerDB;
END