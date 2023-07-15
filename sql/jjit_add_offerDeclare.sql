CREATE PROCEDURE jjit_add_offer
    @title NVARCHAR(100),
    @company_name NVARCHAR(100),
    @company_size INT,
    @company_link NVARCHAR(200),
    @job_level INT,
    @company_location NVARCHAR(100),
    @link NVARCHAR(200),
    @salary_uop_min DECIMAL(18, 2),
    @salary_uop_max DECIMAL(18, 2),
    @salary_b2b_min DECIMAL(18, 2),
    @salary_b2b_max DECIMAL(18, 2),
    @technology_ids NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRANSACTION;

	BEGIN TRY
        INSERT INTO jjit_job_offers (title, company_name, company_size, company_link, job_level, company_location, link, salary_uop_min, salary_uop_max, salary_b2b_min, salary_b2b_max)
        VALUES (@title, @company_name, @company_size, @company_link, @job_level, @company_location, @link, @salary_uop_min, @salary_uop_max, @salary_b2b_min, @salary_b2b_max)

        DECLARE @current_offer_id INT = SCOPE_IDENTITY();

        DECLARE @technology_id INT;
        DECLARE @delimiter CHAR(1) = ',';
        DECLARE @start_position INT = 1;
        DECLARE @end_position INT;
        SET @technology_ids = @technology_ids + @delimiter;

        WHILE @start_position < LEN(@technology_ids)
        BEGIN
            SET @end_position = CHARINDEX(@delimiter, @technology_ids, @start_position);

            IF @end_position = 0
                SET @end_position = LEN(@technology_id);

            SET @technology_id = SUBSTRING(@technology_ids, @start_position, @end_position - @start_position);

            INSERT INTO jjit_jobOffers_technologies (job_offer_id, technology_id)
            VALUES (@current_offer_id, @technology_id);

            SET @start_position = @end_position + 1;
        END;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH;
END;