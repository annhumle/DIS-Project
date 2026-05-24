CREATE TABLE persons (
    person_id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    gender TEXT NOT NULL,
    birthdate DATE NOT NULL
);

CREATE TABLE cycles (
    cycle_id SERIAL PRIMARY KEY,
    start_date DATE NOT NULL,
    end_date DATE,
    person_id INTEGER NOT NULL REFERENCES persons(person_id)
);

CREATE TABLE flow_levels (
    flow_level_id SERIAL PRIMARY KEY,
    level_name TEXT UNIQUE NOT NULL
);

CREATE TABLE physical_symptom (
    physical_symptom_id SERIAL PRIMARY KEY,
    physical_symptom_name TEXT UNIQUE NOT NULL
);

CREATE TABLE daily_logs (
    daily_log_id SERIAL PRIMARY KEY,
    date DATE NOT NULL,
    cycle_day INTEGER NOT NULL,
    cycle_id INTEGER NOT NULL REFERENCES cycles(cycle_id),
    flow_level_id INTEGER REFERENCES flow_levels(flow_level_id)
);

CREATE TABLE daily_log_symptoms (
    daily_log_id INTEGER NOT NULL REFERENCES daily_logs(daily_log_id) ON DELETE CASCADE,
    physical_symptom_id INTEGER NOT NULL REFERENCES physical_symptom(physical_symptom_id),
    PRIMARY KEY (daily_log_id, physical_symptom_id)
);
