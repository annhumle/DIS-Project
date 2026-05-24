import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";

import { getCycles } from "../actions/cycleActions";
import { getFlowLevels } from "../actions/flowLevelActions";
import { getPhysicalSymptoms } from "../actions/physicalSymptomActions";
import {
    getAllLogs,
    createDailyLog,
    updateDailyLog
} from "../actions/dailyLogActions";

import "../../css/HomePage.css";

function toLocalIso(date) {
    const y = date.getFullYear();
    const m = String(date.getMonth() + 1).padStart(2, "0");
    const d = String(date.getDate()).padStart(2, "0");
    return `${y}-${m}-${d}`;
}

function parseLocalIso(iso) {
    const [y, m, d] = iso.split("-").map(Number);
    return new Date(y, m - 1, d);
}

function todayIso() {
    return toLocalIso(new Date());
}

function cycleForDate(dateIso, cycles) {
    const picked = parseLocalIso(dateIso);
    return cycles.find(c => {
        const start = parseLocalIso(c.startDate.slice(0, 10));
        if (picked < start) return false;
        if (!c.endDate) return true;
        const end = parseLocalIso(c.endDate.slice(0, 10));
        return picked <= end;
    });
}

function cycleDayWithin(dateIso, cycle) {
    if (!cycle) return null;
    const start = parseLocalIso(cycle.startDate.slice(0, 10));
    const picked = parseLocalIso(dateIso);
    return Math.floor((picked - start) / (1000 * 60 * 60 * 24)) + 1;
}

function HomePage() {

    const dispatch = useDispatch();

    const cycles = useSelector(state => state.cycleState.cycles);
    const flowLevels = useSelector(state => state.flowLevelState.flowLevels);
    const physicalSymptoms = useSelector(state => state.physicalSymptomState.physicalSymptoms);
    const dailyLogs = useSelector(state => state.dailyLogState.dailyLogs);

    const [selectedDate, setSelectedDate] = useState(todayIso());

    useEffect(() => {
        dispatch(getCycles());
        dispatch(getFlowLevels());
        dispatch(getPhysicalSymptoms());
    }, []);

    useEffect(() => {
        if (cycles.length > 0) {
            dispatch(getAllLogs(cycles));
        }
    }, [cycles.length]);

    const existingLog = dailyLogs.find(log => log.date.slice(0, 10) === selectedDate);
    const loggedDateIsos = new Set(dailyLogs.map(log => log.date.slice(0, 10)));
    const cycleForSelected = cycleForDate(selectedDate, cycles);

    return (
        <>
            <header className="cycle-header">
                <h1 className="cycle-title">Cycle Tracker</h1>
            </header>
            <main className="cycle-page">
                <aside className="card">
                    <Calendar
                        selectedDate={selectedDate}
                        onSelect={setSelectedDate}
                        loggedDateIsos={loggedDateIsos}
                    />
                </aside>

                <LogPanel
                    key={`${selectedDate}-${existingLog?.dailyLogId ?? "new"}`}
                    selectedDate={selectedDate}
                    existingLog={existingLog}
                    cycleForSelected={cycleForSelected}
                    cycles={cycles}
                    flowLevels={flowLevels}
                    physicalSymptoms={physicalSymptoms}
                />
            </main>
        </>
    );
}

function Calendar({ selectedDate, onSelect, loggedDateIsos }) {

    const [viewMonth, setViewMonth] = useState(() => {
        const d = parseLocalIso(selectedDate);
        return new Date(d.getFullYear(), d.getMonth(), 1);
    });

    function goPrev() {
        setViewMonth(new Date(viewMonth.getFullYear(), viewMonth.getMonth() - 1, 1));
    }

    function goNext() {
        setViewMonth(new Date(viewMonth.getFullYear(), viewMonth.getMonth() + 1, 1));
    }

    const monthLabel = viewMonth.toLocaleDateString(undefined, {
        month: "long",
        year: "numeric"
    });

    const firstDayOfMonth = viewMonth.getDay();
    const leadingEmpty = (firstDayOfMonth + 6) % 7;
    const daysInMonth = new Date(
        viewMonth.getFullYear(),
        viewMonth.getMonth() + 1,
        0
    ).getDate();

    const today = todayIso();

    const cells = [];

    for (let i = 0; i < leadingEmpty; i++) {
        cells.push(<div key={`pad-${i}`} className="cal-cell empty" />);
    }

    for (let day = 1; day <= daysInMonth; day++) {
        const iso = toLocalIso(new Date(viewMonth.getFullYear(), viewMonth.getMonth(), day));
        const classes = ["cal-cell"];
        if (iso === selectedDate) classes.push("selected");
        if (iso === today) classes.push("today");
        if (loggedDateIsos.has(iso)) classes.push("has-log");

        cells.push(
            <button
                key={iso}
                type="button"
                className={classes.join(" ")}
                onClick={() => onSelect(iso)}
            >
                {day}
            </button>
        );
    }

    return (
        <div className="cal">
            <div className="cal-header">
                <button type="button" className="cal-nav" onClick={goPrev} aria-label="Previous month">‹</button>
                <span className="cal-month">{monthLabel}</span>
                <button type="button" className="cal-nav" onClick={goNext} aria-label="Next month">›</button>
            </div>
            <div className="cal-weekdays">
                {["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"].map(d => (
                    <span key={d}>{d}</span>
                ))}
            </div>
            <div className="cal-grid">{cells}</div>
        </div>
    );
}

function LogPanel({ selectedDate, existingLog, cycleForSelected, cycles, flowLevels, physicalSymptoms }) {

    const dispatch = useDispatch();

    const [flowLevelId, setFlowLevelId] = useState(existingLog?.flowLevelId ?? "");
    const [symptomIds, setSymptomIds] = useState(existingLog?.symptomIds ?? []);
    const [submitting, setSubmitting] = useState(false);

    function toggleSymptom(id) {
        setSymptomIds(prev =>
            prev.includes(id) ? prev.filter(s => s !== id) : [...prev, id]
        );
    }

    function handleSave() {
        if (!cycleForSelected) return;

        const basePayload = {
            date: selectedDate,
            cycleDay: cycleDayWithin(selectedDate, cycleForSelected),
            flowLevelId: flowLevelId === "" ? null : Number(flowLevelId),
            symptomIds
        };

        setSubmitting(true);

        const promise = existingLog
            ? dispatch(updateDailyLog(existingLog.dailyLogId, basePayload))
            : dispatch(createDailyLog({ ...basePayload, cycleId: cycleForSelected.cycleId }));

        promise
            .then(() => dispatch(getAllLogs(cycles)))
            .finally(() => setSubmitting(false));
    }

    const prettyDate = parseLocalIso(selectedDate).toLocaleDateString(undefined, {
        weekday: "long",
        month: "long",
        day: "numeric"
    });

    const dayNumber = cycleDayWithin(selectedDate, cycleForSelected);

    return (
        <section className="card">
            <h2 className="panel-date">{prettyDate}</h2>
            {cycleForSelected ? (
                <span className="day-pill">Day {dayNumber} of your cycle</span>
            ) : (
                <span className="day-pill day-pill--muted">No cycle on this date</span>
            )}

            <div className="field">
                <label htmlFor="flowLevel">Flow level</label>
                <select
                    id="flowLevel"
                    value={flowLevelId}
                    onChange={e => setFlowLevelId(e.target.value)}
                >
                    <option value="">None</option>
                    {flowLevels.map(level => (
                        <option key={level.flowLevelId} value={level.flowLevelId}>
                            {level.levelName}
                        </option>
                    ))}
                </select>
            </div>

            <fieldset>
                <legend>Physical symptoms</legend>
                <div className="symptom-grid">
                    {physicalSymptoms.map(symptom => {
                        const checked = symptomIds.includes(symptom.physicalSymptomId);
                        return (
                            <label
                                key={symptom.physicalSymptomId}
                                className={`symptom-chip${checked ? " active" : ""}`}
                            >
                                <input
                                    type="checkbox"
                                    checked={checked}
                                    onChange={() => toggleSymptom(symptom.physicalSymptomId)}
                                />
                                {symptom.physicalSymptomName}
                            </label>
                        );
                    })}
                </div>
            </fieldset>

            <button
                type="button"
                className="save-button"
                onClick={handleSave}
                disabled={submitting || !cycleForSelected}
            >
                {submitting ? "Saving..." : existingLog ? "Update" : "Save"}
            </button>
        </section>
    );
}

export default HomePage;
