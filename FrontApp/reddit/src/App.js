import LoginForm from "./Components/Login";

import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import Register from "./Components/Register";
import Dashboard from "./Components/Dashboard";
import EditProfile from "./Components/EditProfile";
import NewTopic from "./Components/NewTopic";
import FullTopic from "./Components/FullTopic";
import Search from "./Components/Search";
function App() {
  return (
    <>
        <Router>
            <Routes>
            <Route  path="/" element={<LoginForm />} />
            <Route path='/Register' element={<Register/>}></Route>
            <Route  path='/Dashboard' element={<Dashboard/>}></Route>
            <Route  path='/EditProfile' element={<EditProfile/>}></Route>
            <Route exact path='/NewTopic' element={<NewTopic/>}></Route>
            <Route path="/FullTopic" element={<FullTopic/>}></Route>
            <Route path="/Search" element={<Search/>}></Route>
            </Routes>
        </Router>
    </>
  );
}

export default App;