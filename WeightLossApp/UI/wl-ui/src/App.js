import './App.css';
import { Home } from './components/Home';
import { IngridientsData } from './components/IngridientsData.js';
import { Route, Routes, NavLink } from 'react-router-dom';



// Main app component generates basic layout
function App() {
  return (
    <div className="App">
      {/* Navigation component */}
      <nav className="navbar navbar-dark bg-dark navbar-expand-lg">
        <NavLink className="navbar-brand mx-3" to={"/"}>Weight Loss App</NavLink>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        {/* Place to add new links */}
        <div className="collapse navbar-collapse" id="navbarNavDropdown">
          <ul className="navbar-nav">
            {/* Link example */}
            <li className="nav-item">
              <NavLink className="nav-link" to="/ingridientsData">Ingridients Data</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to={"/"}>Features</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to={"/"}>Pricing</NavLink>
            </li>
          </ul>
        </div>
      </nav>

      {/* Place to register links
          path: content of "to" attribute in NavLink component 
          element: component that has to be rendered */}
      <Routes>
        <Route path='/' element={<Home/>} />
        <Route path='/ingridientsData' element={<IngridientsData/>} />
      </Routes>
    </div>
  );
}

export default App;
