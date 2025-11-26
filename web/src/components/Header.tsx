import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header>
      <nav>
        <Link to="/">Strona główna</Link>
        <Link to="/login">Zaloguj się</Link>
      </nav>
    </header>
  );
}
