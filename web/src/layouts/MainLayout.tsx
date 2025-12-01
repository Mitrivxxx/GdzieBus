import { useState } from "react";
import Header from "../components/Header";
import Map from "../pages/Map";
import AdminPanel, { type AdminSection } from "@/components/AdminPanel";
import MainPanel from "@/components/MainPanel";
import LoginPanel from "@/components/LoginPanel";
import "./MainLayout.scss";

export default function MainLayout() {
  const [isAdminOpen, setAdminOpen] = useState(false);
  const [isLoginOpen, setLoginOpen] = useState(false);
  const [activeAdminSection, setActiveAdminSection] = useState<AdminSection>("dashboard");

  const handleToggleAdmin = () => {
    setAdminOpen((prev) => !prev);
    if (isLoginOpen) setLoginOpen(false);
  };

  const handleToggleLogin = () => {
    setLoginOpen((prev) => !prev);
    if (isAdminOpen) setAdminOpen(false);
  };

  return (
    <div className="app-layout">
      <main className="app-layout__main">
        <div className="app-layout__map-container">
          <Map />

          <header className="app-layout__header">
            <Header 
              onAdminClick={handleToggleAdmin} 
              onLoginClick={handleToggleLogin}
            />
          </header>

          <div className="app-layout__panels">
            <div className="app-layout__main-panel">
              <MainPanel />
            </div>
            
            {isAdminOpen && (
              <div className="app-layout__admin-panel">
                <AdminPanel
                  activeSection={activeAdminSection}
                  onSectionChange={setActiveAdminSection}
                />
              </div>
            )}

            {isLoginOpen && (
              <div className="app-layout__login-panel">
                <LoginPanel 
                  onLogin={() => { console.log("Logged in"); setLoginOpen(false); }}
                  onClose={() => setLoginOpen(false)}
                />
              </div>
            )}
          </div>
        </div>
      </main>
    </div>
  );
}