IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'jobClouddb_dev')
	BEGIN
		CREATE DATABASE [jobClouddb_dev]
	END