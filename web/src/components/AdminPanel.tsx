import { useEffect, useState } from "react";
import AddStopForm from "../pages/forms/AddStopForm";
import { getAllStops, type StopDto } from "../api/stops";
import "./AdminPanel.scss";

export type AdminSection = "dashboard" | "stops" | "buses";

type AdminPanelProps = {
  activeSection: AdminSection;
  onSectionChange: (section: AdminSection) => void;
};

export default function AdminPanel({ activeSection, onSectionChange }: AdminPanelProps) {
  const [stops, setStops] = useState<StopDto[]>([]);

  useEffect(() => {
    if (activeSection === "stops") {
      getAllStops()
        .then(setStops)
        .catch((err) => console.error("Failed to fetch stops", err));
    }
  }, [activeSection]);

  return (
    <div className="admin-panel">
      <div className="admin-panel__sidebar">
        <h3 className="admin-panel__menu-title">Menu</h3>
        <ul className="admin-panel__menu-list">
          <li 
            className={`admin-panel__menu-item ${activeSection === "stops" ? "admin-panel__menu-item--active" : ""}`}
            onClick={() => onSectionChange("stops")}
          >
            Przystanki
          </li>
          <li 
            className={`admin-panel__menu-item ${activeSection === "buses" ? "admin-panel__menu-item--active" : ""}`}
            onClick={() => onSectionChange("buses")}
          >
            Autobusy
          </li>
        </ul>
      </div>

      {/*section content*/}
      <div className="admin-panel__content">
        {activeSection === "dashboard" && (
            <div className="admin-panel__dashboard-placeholder">
                <h2>Witaj w Panelu Administratora</h2>
                <p>Wybierz opcję z menu po lewej stronie, aby rozpocząć zarządzanie.</p>
            </div>
        )}
        {activeSection === "stops" && (
            <div>
              <div className="admin-panel__header">
                <h2 className="admin-panel__section-title">Zarządzanie Przystankami</h2>
                <button onClick={() => alert("Otwieranie formularza dodawania przystanku")}>Dodaj Przystanek</button>
              </div>  
                <div className="admin-list">
                    {stops.map((stop, index) => (
                        <div key={index} className="admin-list__item">
                            <div>
                                <strong>{stop.stopName}</strong>
                                <div style={{ fontSize: "0.85rem", color: "#666" }}>
                                    {stop.city} {stop.address && `, ${stop.address}`}
                                </div>
                                <button onClick={() => alert(`Edycja przystanku: ${stop.stopName}`)}>Edytuj</button>
                                <button onClick={() => alert(`Usuwanie przystanku: ${stop.stopName}`)}>Usuń</button>
                            </div>
                        </div>
                    ))}
                    {stops.length === 0 && <p>Brak przystanków w bazie.</p>}
                </div>
            </div>
        )}
        {activeSection === "buses" && (
            <div>
                <h2 className="admin-panel__section-title">Zarządzanie Autobusami</h2>
                {/*dupa dupa*/}
            </div>
        )}
      </div>
    </div>
  );
}
