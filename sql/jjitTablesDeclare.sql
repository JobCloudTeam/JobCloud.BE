CREATE TABLE jjit_job_offers (
	id INT IDENTITY(1,1) PRIMARY KEY,
	title NVARCHAR(100),
	company_name NVARCHAR(100),
	company_size INT,
	company_link NVARCHAR(200),
	job_level INT,
	company_location NVARCHAR(100),
	link NVARCHAR(200),
	salary_uop_min DECIMAL(18, 2),
	salary_uop_max DECIMAL(18, 2),
	salary_b2b_min DECIMAL(18, 2),
	salary_b2b_max DECIMAL(18, 2),
);
CREATE TABLE technologies (
	id INT IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(50),
	tech_level int
);
CREATE TABLE jjit_jobOffers_technologies (
	job_offer_id int,
	technology_id int,
	PRIMARY KEY(job_offer_id, technology_id),
	FOREIGN KEY (job_offer_id) REFERENCES jjit_job_offers(id),
	FOREIGN KEY (technology_id) REFERENCES technologies(id)
);

