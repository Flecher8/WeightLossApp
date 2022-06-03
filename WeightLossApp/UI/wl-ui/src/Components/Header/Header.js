import {Link} from 'react-router-dom';


function Header() {
    return (
        <nav className="navbar navbar-dark bg-dark navbar-expand-lg">
        <Link className="navbar-brand mx-3" to={"/"}>Weight Loss App</Link>
        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNavDropdown">
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link" to="/IngridientData">Ingridients Data</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to={"/"}>Features</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to={"/"}>Pricing</Link>
            </li>
          </ul>
        </div>
      </nav>
    )
}

export default Header;