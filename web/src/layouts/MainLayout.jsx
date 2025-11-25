import { Outlet, useLocation } from "react-router-dom";
import Header from "../components/Header";
import Footer from "../components/Footer";
import Map from "@/pages/Map";
import "./MainLay out.scss"; // <--- IMPORT STYLI

export default function MainLayout() {
  const location = useLocation();
  const isLoginOpen = location.pathname === "/login";

  return (
    <div className="main-layout">
      <div className="main-layout__map-container">
        <Map />
      </div>

      <div className="main-layout__header-wrapper">
        <Header />
      </div>

      {isLoginOpen && (
        <div className="login-modal">
          <div className="login-modal__content">
            <Outlet />
          </div>
        </div>
      )}

      <div className="main-layout__footer-wrapper">
         <Footer />
      </div>
    </div>
  );
}