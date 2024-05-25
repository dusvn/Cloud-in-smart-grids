import LoginForm from "./Components/Login";

import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Register from "./Components/Register";
import Dashboard from "./Components/Dashboard";

function App() {
  return (
    <>
        <Router>
            <Routes>
            <Route exact path="/" element={<LoginForm />} />
            <Route path='/Register' element={<Register/>}></Route>
            <Route path='Dashboard' element={<Dashboard/>}></Route>
            </Routes>
        </Router>
    </>
  );
}

export default App;