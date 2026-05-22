CREATE TABLE persons (
    person_id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
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