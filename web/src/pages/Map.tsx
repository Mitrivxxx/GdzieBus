import { useEffect, useState } from "react";
import { APIProvider, Map as GoogleMap, AdvancedMarker, InfoWindow } from "@vis.gl/react-google-maps";
import { getLastPosition } from "../api/gps";

const center = { lat: 52.237049, lng: 21.017532 };
const HARDCODED_VEHICLE_ID = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

interface GpsPosition {
  vehicleId: string;
  latitude: number;
  longitude: number;
  speedKmh: number;
  directionDegrees: number;
  timestamp: string;
}

export default function Map() {
  const apiKey = import.meta.env.VITE_GOOGLE_MAPS_API_KEY as string;
  const mapId = "DEMO_MAP_ID"; 

  const [position, setPosition] = useState<GpsPosition | null>(null);
  const [isInfoWindowOpen, setIsInfoWindowOpen] = useState(false);

  useEffect(() => {
    getLastPosition(HARDCODED_VEHICLE_ID)
      .then((data) => {
        setPosition(data);
      })
      .catch((err) => console.error("Failed to fetch position", err));
  }, []);

  return (
    <div style={{ width: "100%", height: "100%" }}>
      <APIProvider apiKey={apiKey}>
        <GoogleMap
          defaultCenter={center}
          defaultZoom={13}
          mapId={mapId}
          mapTypeControl={false}
          streetViewControl={false}
          style={{ width: "100%", height: "100%" }}
          // Jeśli mamy pozycję, centrujemy na niej mapę (opcjonalne, ale przydatne)
          center={position ? { lat: position.latitude, lng: position.longitude } : center}
        >
          {position && (
            <>
              <AdvancedMarker 
                position={{ lat: position.latitude, lng: position.longitude }} 
                onClick={() => setIsInfoWindowOpen(true)}
              />

              {isInfoWindowOpen && (
                <InfoWindow
                  position={{ lat: position.latitude, lng: position.longitude }}
                  onCloseClick={() => setIsInfoWindowOpen(false)}
                >
                  <div style={{ color: "black" }}>
                    <h3>Pojazd: {position.vehicleId}</h3>
                    <p>Prędkość: {position.speedKmh} km/h</p>
                    <p>Kierunek: {position.directionDegrees}°</p>
                    <p>Czas: {new Date(position.timestamp).toLocaleString()}</p>
                    <p>Lat: {position.latitude}</p>
                    <p>Lng: {position.longitude}</p>
                  </div>
                </InfoWindow>
              )}
            </>
          )}
        </GoogleMap>
      </APIProvider>
    </div>
  );
}
