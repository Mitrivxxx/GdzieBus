import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  const handleLogin = () => {
    // Tutaj w przyszłości dodasz logikę logowania (np. strzał do API)
    console.log("Zalogowano!");
    
    // Po zalogowaniu wracamy na stronę główną (zamykamy modal)
    navigate("/");
  };

  return (
    <div style={{ textAlign: "center" }}>
      <h1>Logowanie</h1>
      <p>Wprowadź swoje dane (symulacja)</p>
      
      <button 
        onClick={handleLogin}
        style={{
          padding: "10px 20px",
          backgroundColor: "#007bff",
          color: "white",
          border: "none",
          borderRadius: "5px",
          cursor: "pointer",
          fontSize: "16px"
        }}
      >
        Zaloguj się
      </button>
      
      <br /><br />
      
      <button 
        onClick={() => navigate("/")}
        style={{
          background: "none",
          border: "none",
          color: "#666",
          cursor: "pointer",
          textDecoration: "underline"
        }}
      >
        Anuluj
      </button>
    </div>
  );
}
