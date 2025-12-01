import { Link } from "react-router-dom";

  type HeaderProps = {
    onAdminClick: () => void;
    onLoginClick: () => void;
  };
export default function Header({ onAdminClick, onLoginClick }: HeaderProps) {

  return (
    <header>
      <nav>
        <button onClick={onAdminClick}> Admin Panel</button>
        <button onClick={onLoginClick}> Zaloguj</button>
      </nav>
    </header>
  );
};
