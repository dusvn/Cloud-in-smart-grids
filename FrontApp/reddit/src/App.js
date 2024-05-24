import LoginForm from "./Components/Login";

import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Register from "./Components/Register";

function App() {
  return (
    <>
        <Router>
            <Routes>
            <Route   path="/" element={<LoginForm />} />
            <Route path='/Register' element={<Register/>}></Route>
            </Routes>
        </Router>
    </>
  );
}

export default App;