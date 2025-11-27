import { Link } from "react-router-dom";

  type HeaderProps = {
    onAdminClick: () => void;
  };
export default function Header({ onAdminClick }: HeaderProps) {

  return (
    <header>
      <nav>
        <Link to="/">Strona główna</Link>
        <button onClick={onAdminClick} style={{ padding: "0.5rem 1rem", cursor: "pointer" }}>Admin Panel</button>
        <Link to="/login">Zaloguj się</Link>
      </nav>
    </header>
  );
};
