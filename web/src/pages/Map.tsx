import { APIProvider, Map as GoogleMap, AdvancedMarker } from "@vis.gl/react-google-maps";

const center = { lat: 52.237049, lng: 21.017532 };

export default function Map() {
  const apiKey = import.meta.env.VITE_GOOGLE_MAPS_API_KEY as string;
  // Aby używać AdvancedMarker, wymagane jest Map ID. 
  // Możesz utworzyć własne Map ID w Google Cloud Console.
  // "DEMO_MAP_ID" działa tylko w celach demonstracyjnych.
  const mapId = "DEMO_MAP_ID"; 

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
        >
          <AdvancedMarker position={center} />
        </GoogleMap>
      </APIProvider>
    </div>
  );
}
