SELECT [name] AS Name
      ,[company] AS Company
      ,[salary_uop] AS SalaryUOP
      ,[salary_b2b] AS SalaryB2B
      ,[base_tech] AS BaseTech
      ,[techstack] AS TechStack
FROM [jobClouddb_dev].[jjit].[offers]
WHERE [status] = 1