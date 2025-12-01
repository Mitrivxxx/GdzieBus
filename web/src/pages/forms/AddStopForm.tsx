import { useState } from "react";
import { addStop, type StopDto } from "../../api/stops";

type AddStopFormProps = {
  onBack?: () => void;
};

export default function AddStopForm({ onBack }: AddStopFormProps) {
  const [formData, setFormData] = useState<StopDto>({
    stopName: "",
    stopCode: "",
    latitude: 0,
    longitude: 0,
    address: "",
    city: "",
    zone: "",
  });

  const [errors, setErrors] = useState<Record<string, string>>({});
  const [serverError, setServerError] = useState<string | null>(null);

  const validate = () => {
    const newErrors: Record<string, string> = {};

    if (!formData.stopName.trim()) newErrors.stopName = "Nazwa przystanku jest wymagana";
    if (!formData.stopCode?.trim()) newErrors.stopCode = "Kod przystanku jest wymagany";
    
    if (formData.latitude === undefined || formData.latitude < -90 || formData.latitude > 90) {
      newErrors.latitude = "Szerokość geograficzna musi być między -90 a 90";
    }
    if (formData.longitude === undefined || formData.longitude < -180 || formData.longitude > 180) {
      newErrors.longitude = "Długość geograficzna musi być między -180 a 180";
    }

    if (!formData.city?.trim()) newErrors.city = "Miasto jest wymagane";

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "latitude" || name === "longitude" ? parseFloat(value) : value,
    }));
    // Czyścimy błąd pola przy edycji
    if (errors[name]) {
      setErrors((prev) => ({ ...prev, [name]: "" }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setServerError(null);

    if (!validate()) return;

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
    } catch (error: any) {
      console.error(error);
      const message = error.message.replace("Failed to add stop: ", "").replace(/"/g, "");
      setServerError(message);
    }
  };

  const inputStyle = {
    padding: "8px",
    borderRadius: "4px",
    border: "1px solid #ccc",
    width: "100%"
  };

  return (
    <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", maxWidth: "400px", gap: "10px" }}>
      {serverError && <div className="error-alert">{serverError}</div>}
      
      <label>
        Nazwa przystanku:
        <input 
          name="stopName" 
          value={formData.stopName} 
          onChange={handleChange} 
          style={inputStyle}
          className={errors.stopName ? "input-error" : ""}
        />
        {errors.stopName && <div className="error-message">{errors.stopName}</div>}
      </label>

      <label>
        Kod przystanku:
        <input 
          name="stopCode" 
          value={formData.stopCode} 
          onChange={handleChange} 
          style={inputStyle}
          className={errors.stopCode ? "input-error" : ""}
        />
        {errors.stopCode && <div className="error-message">{errors.stopCode}</div>}
      </label>

      <label>
        Szerokość geograficzna:
        <input 
          name="latitude" 
          type="number" 
          step="any" 
          value={formData.latitude} 
          onChange={handleChange} 
          style={inputStyle}
          className={errors.latitude ? "input-error" : ""}
        />
        {errors.latitude && <div className="error-message">{errors.latitude}</div>}
      </label>

      <label>
        Długość geograficzna:
        <input 
          name="longitude" 
          type="number" 
          step="any" 
          value={formData.longitude} 
          onChange={handleChange} 
          style={inputStyle}
          className={errors.longitude ? "input-error" : ""}
        />
        {errors.longitude && <div className="error-message">{errors.longitude}</div>}
      </label>

      <label>
        Adres:
        <input 
          name="address" 
          value={formData.address} 
          onChange={handleChange} 
          style={inputStyle}
        />
      </label>

      <label>
        Miasto:
        <input 
          name="city" 
          value={formData.city} 
          onChange={handleChange} 
          style={inputStyle}
          className={errors.city ? "input-error" : ""}
        />
        {errors.city && <div className="error-message">{errors.city}</div>}
      </label>

      <label>
        Strefa:
        <input 
          name="zone" 
          value={formData.zone} 
          onChange={handleChange} 
          style={inputStyle}
        />
      </label>

      <div style={{ display: "flex", gap: "10px", marginTop: "1rem" }}>
        {onBack && (
          <button type="button" onClick={onBack} style={{ flex: 1, padding: "0.5rem", backgroundColor: "#ccc", border: "none", cursor: "pointer" }}>
            Powrót
          </button>
        )}
        <button type="submit" style={{ flex: 1, padding: "0.5rem", backgroundColor: "aquamarine", border: "none", cursor: "pointer" }}>
          Dodaj
        </button>
      </div>
    </form>
  );
}
