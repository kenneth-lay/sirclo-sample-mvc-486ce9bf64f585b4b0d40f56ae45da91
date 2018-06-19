CREATE TABLE weight_list (
  weight_list_id smallint IDENTITY(1,1) PRIMARY KEY,
  record_date datetime2 NOT NULL,
  max_weight int NOT NULL,
  min_weight int NOT NULL
);