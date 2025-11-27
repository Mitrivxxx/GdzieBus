import { BrowserRouter, Routes, Route } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import Home from "./pages/Map";
import Login from "./pages/Login";
import AddStopForm from "./pages/AddStopForm";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Wszystkie strony pod głównym layoutem */}
        <Route element={<MainLayout />}>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/add-stop" element={<AddStopForm />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
