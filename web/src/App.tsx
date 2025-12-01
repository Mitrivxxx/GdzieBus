import { BrowserRouter, Routes, Route } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import Home from "./pages/Map";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Wszystkie strony pod głównym layoutem */}
        <Route element={<MainLayout />}>
          <Route path="/" element={<Home />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
