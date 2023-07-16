CREATE PROCEDURE jjit_get_offers
AS
BEGIN
    SELECT
        j.id,
        j.title,
        j.company_name,
        j.company_size,
        j.company_link,
        j.job_level,
        j.company_location,
        j.link,
        j.salary_uop_min,
        j.salary_uop_max,
        j.salary_b2b_min,
        j.salary_b2b_max,
        STRING_AGG(CONVERT(NVARCHAR(MAX), t.id), ',') AS technologies
    FROM
        jjit_job_offers j
        INNER JOIN jjit_jobOffers_technologies jot ON j.id = jot.job_offer_id
        INNER JOIN technologies t ON jot.technology_id = t.id
    GROUP BY
        j.id,
        j.title,
        j.company_name,
        j.company_size,
        j.company_link,
        j.job_level,
        j.company_location,
        j.link,
        j.salary_uop_min,
        j.salary_uop_max,
        j.salary_b2b_min,
        j.salary_b2b_max
END;