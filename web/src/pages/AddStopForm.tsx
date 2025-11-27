import { useState } from "react";
import { addStop, type StopDto } from "../api/stops";

export default function AddStopForm() {
  const [formData, setFormData] = useState<StopDto>({
    stopName: "",
    stopCode: "",
    latitude: 0,
    longitude: 0,
    address: "",
    city: "",
    zone: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "latitude" || name === "longitude" ? parseFloat(value) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await addStop(formData);
      alert(`Dodano przystanek: ${formData.stopName}`);
      setFormData({
        stopName: "",
        stopCode: "",
        latitude: 0,
        longitude: 0,
        address: "",
        city: "",
        zone: "",
      });
    } catch (error) {
      console.error(error);
      alert("Błąd podczas dodawania przystanku");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", maxWidth: "400px", gap: "10px" }}>
      <label>
        Nazwa przystanku:
        <input name="stopName" value={formData.stopName} onChange={handleChange} required />
      </label>
      <label>
        Kod przystanku:
        <input name="stopCode" value={formData.stopCode} onChange={handleChange} />
      </label>
      <label>
        Szerokość geograficzna:
        <input name="latitude" type="number" step="any" value={formData.latitude} onChange={handleChange} />
      </label>
      <label>
        Długość geograficzna:
        <input name="longitude" type="number" step="any" value={formData.longitude} onChange={handleChange} />
      </label>
      <label>
        Adres:
        <input name="address" value={formData.address} onChange={handleChange} />
      </label>
      <label>
        Miasto:
        <input name="city" value={formData.city} onChange={handleChange} />
      </label>
      <label>
        Strefa:
        <input name="zone" value={formData.zone} onChange={handleChange} />
      </label>
      <button type="submit" style={{ marginTop: "1rem", padding: "0.5rem" }}>Dodaj</button>
    </form>
  );
}
