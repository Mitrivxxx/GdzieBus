import AddStopForm from "../pages/AddStopForm";

type AdminPanelProps = {
  activeSection: "none" | "addStop";
  onSectionChange: (section: "none" | "addStop") => void;
};

export default function AdminPanel({ activeSection, onSectionChange }: AdminPanelProps) {
  return (
    <div style={{
      position: "absolute",
      top: 0,
      left: 0,
      width: "80%",
      height: "100%",
      backgroundColor: "#f0f0f0",
      zIndex: 10,
      padding: "1rem",
      boxShadow: "2px 0 5px rgba(0,0,0,0.3)"
    }}>
      <h2>Panel Admina</h2>
      <nav style={{ marginBottom: "1rem" }}>
        <button onClick={() => onSectionChange("addStop")} style={{ marginRight: "1rem" }}>Dodaj przystanek</button>
        {/* inne sekcje admina */}
      </nav>

      <div>
        {activeSection === "addStop" && <AddStopForm />}
      </div>
    </div>
  );
}
