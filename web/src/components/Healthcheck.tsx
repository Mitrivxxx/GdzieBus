// src/components/HealthCheck.tsx
import { useEffect, useState } from "react";

interface HealthResponse {
  status: string;
  db: number;
}

export default function HealthCheck() {
  const [status, setStatus] = useState<string>("loading...");

  useEffect(() => {
    const fetchHealth = async () => {
      try {
        const res = await fetch("http://localhost:5259/health/full");
        if (!res.ok) throw new Error("Network response was not ok");

        const data: HealthResponse = await res.json();
        setStatus(data.status);
      } catch (error) {
        setStatus("error");
        console.error("Health check failed:", error);
      }
    };

    fetchHealth();
  }, []);

  return <div id="health-status">{status}</div>;
}
