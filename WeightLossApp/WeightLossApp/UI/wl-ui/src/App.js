import './App.css';
import {Home} from './components/Home';
import {IngridientsData} from './components/IngridientsData.js';
import {Route, Routes, NavLink} from 'react-router-dom';


function App() {
  return (
    <div className="App">
      <nav className="navbar navbar-dark bg-dark navbar-expand-lg">
        <NavLink className="navbar-brand mx-3" to={"/"}>Weight Loss App</NavLink>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNavDropdown">
          <ul className="navbar-nav">
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

      <Routes>
        <Route path='/' element={<Home/>} />
        <Route path='/ingridientsData' element={<IngridientsData/>} />
      </Routes>
    </div>
  );
}

export default App;
