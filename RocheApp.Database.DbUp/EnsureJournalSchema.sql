﻿IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'DbUp')
   EXEC('CREATE SCHEMA [DbUp]')
GO