import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import { getCycles } from "../actions/cycleActions";

function HomePage() {

    const dispatch = useDispatch();

    const cycles = useSelector(state => state.cycleState.cycles);
    const loading = useSelector(state => state.cycleState.loading);

    useEffect(() => {
        dispatch(getCycles());
    }, []);

    return (
        <main>
            <h1>Cycles</h1>

            {loading && <p>Loading...</p>}
            {cycles.map(cycle => (
                <div key={cycle.cycleId}>
                    <p>Cycle ID: {cycle.cycleId}</p>
                    <p>Start date: {new Date(cycle.startDate).toLocaleDateString()}</p>
                    <p>End date: {cycle.endDate ? new Date(cycle.endDate).toLocaleDateString() : "No end date"}</p>
                </div>
            ))}
        </main>
    );
}

export default HomePage;