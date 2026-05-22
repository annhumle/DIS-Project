INSERT INTO persons (name)
VALUES ('Test user');

INSERT INTO cycles (start_date, end_date, person_id)
VALUES ('2026-05-01', NULL, 1);

INSERT INTO flow_levels (level_name)
VALUES ('Light'), ('Medium'), ('Heavy');

INSERT INTO physical_symptom (physical_symptom_name)
VALUES ('Cramping'), ('Fatigue'), ('Tender breasts'), ('Bloated'), ('Mood swings');