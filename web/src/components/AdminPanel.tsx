import AddStopForm from "../pages/AddStopForm";

type AdminPanelProps = {
  activeSection: "none" | "addStop";
  onSectionChange: (section: "none" | "addStop") => void;
};

export default function AdminPanel({ activeSection, onSectionChange }: AdminPanelProps) {
  return (
    <div>
      <h2>Panel Admina</h2>
      
      {activeSection === "none" && (
        <nav style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
          <button 
            onClick={() => onSectionChange("addStop")} 
            style={{ padding: "10px", cursor: "pointer" }}
          >
            Dodaj przystanek
          </button>
          {/* Tutaj możesz dodać więcej przycisków w przyszłości */}
        </nav>
      )}

      {activeSection === "addStop" && (
        <div>
          <h3 style={{ marginTop: 0 }}>Dodawanie przystanku</h3>
          <AddStopForm onBack={() => onSectionChange("none")} />
        </div>
      )}
    </div>
  );
}
