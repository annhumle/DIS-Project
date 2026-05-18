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