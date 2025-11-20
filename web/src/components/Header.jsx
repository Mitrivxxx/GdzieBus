import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header style={{ padding: "20px", borderBottom: "1px solid #ddd" }}>
      <nav>
        <Link to="/" style={{ marginRight: "20px" }}>Strona główna</Link>
        <Link to="/login">Zaloguj się</Link>
      </nav>
    </header>
  );
}
