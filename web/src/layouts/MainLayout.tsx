import { Outlet, useLocation } from "react-router-dom";
import Header from "../components/Header";
import Footer from "../components/Footer";
import Map from "../pages/Map";
import "./MainLayout.scss"; // <--- IMPORT STYLI
import { useState } from "react";
import AdminPanel from "@/components/AdminPanel";

export default function MainLayout() {
  const location = useLocation();
  const isLoginOpen = location.pathname === "/login";
  const [isAdminOpen, setAdminOpen] = useState(false);
  const [activeAdminSection, setActiveAdminSection] = useState<"none" | "addStop">("none");
  
  return (
    <div className="app-layout">
      <header className="app-layout__header">
        <Header onAdminClick={() => {setAdminOpen(!isAdminOpen)}} />
      </header>

      <main className="app-layout__main">
        <div className="app-layout__admin-panel">
          {isAdminOpen && (
          <AdminPanel
            activeSection={activeAdminSection}
            onSectionChange={setActiveAdminSection}
          />
        )}
        </div>
        <div className="app-layout__map-container">
          <Map />
          {isLoginOpen && (
            <div className="login-modal">
              <div className="login-modal__content">
                <Outlet />
              </div>
            </div>
          )}
        </div>
      </main>

      <footer className="app-layout__footer">
        <Footer />
      </footer>
    </div>

  );
}