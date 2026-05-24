TRUNCATE persons, cycles, flow_levels, physical_symptom, daily_logs, daily_log_symptoms RESTART IDENTITY CASCADE;

INSERT INTO persons (name, gender, birthdate)
VALUES ('Test user', 'Female', '2000-01-15');

INSERT INTO flow_levels (level_name)
VALUES ('Light'), ('Medium'), ('Heavy');

INSERT INTO physical_symptom (physical_symptom_name)
VALUES ('Cramping'), ('Fatigue'), ('Tender breasts'), ('Bloated'), ('Mood swings');

-- Today is 2026-05-24 = day 4 of cycle 4. ~28 day cycles, counted back from there.
-- Cycle 1: 2026-02-26 to 2026-03-25
-- Cycle 2: 2026-03-26 to 2026-04-22
-- Cycle 3: 2026-04-23 to 2026-05-20
-- Cycle 4: 2026-05-21 to present (open)
INSERT INTO cycles (start_date, end_date, person_id) VALUES
    ('2026-02-26', '2026-03-25', 1),
    ('2026-03-26', '2026-04-22', 1),
    ('2026-04-23', '2026-05-20', 1),
    ('2026-05-21', NULL, 1);

-- Period logs (days 1-5) for each cycle. Flow: Light(1), Medium(2), Heavy(3).
-- Cycle 1 → daily_log_id 1-5
INSERT INTO daily_logs (date, cycle_day, cycle_id, flow_level_id) VALUES
    ('2026-02-26', 1, 1, 1),
    ('2026-02-27', 2, 1, 3),
    ('2026-02-28', 3, 1, 3),
    ('2026-03-01', 4, 1, 2),
    ('2026-03-02', 5, 1, 1);

-- Cycle 2 → daily_log_id 6-10
INSERT INTO daily_logs (date, cycle_day, cycle_id, flow_level_id) VALUES
    ('2026-03-26', 1, 2, 1),
    ('2026-03-27', 2, 2, 3),
    ('2026-03-28', 3, 2, 2),
    ('2026-03-29', 4, 2, 2),
    ('2026-03-30', 5, 2, 1);

-- Cycle 3 → daily_log_id 11-15
INSERT INTO daily_logs (date, cycle_day, cycle_id, flow_level_id) VALUES
    ('2026-04-23', 1, 3, 1),
    ('2026-04-24', 2, 3, 3),
    ('2026-04-25', 3, 3, 3),
    ('2026-04-26', 4, 3, 2),
    ('2026-04-27', 5, 3, 1);

-- Cycle 4 (current) → daily_log_id 16-18. Today (day 4) intentionally left blank.
INSERT INTO daily_logs (date, cycle_day, cycle_id, flow_level_id) VALUES
    ('2026-05-21', 1, 4, 1),
    ('2026-05-22', 2, 4, 3),
    ('2026-05-23', 3, 4, 2);

-- A few symptoms scattered across period days. Symptom IDs: 1=Cramping, 2=Fatigue, 3=Tender breasts, 4=Bloated, 5=Mood swings.
INSERT INTO daily_log_symptoms (daily_log_id, physical_symptom_id) VALUES
    (1, 1),
    (2, 1), (2, 2),
    (3, 2), (3, 4),
    (6, 1),
    (7, 1), (7, 2),
    (8, 4),
    (11, 1),
    (12, 1), (12, 2),
    (13, 2), (13, 4),
    (16, 1),
    (17, 1), (17, 2);
