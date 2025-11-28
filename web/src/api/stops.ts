export interface StopDto {
  stopName: string;
  stopCode?: string;
  latitude?: number;
  longitude?: number;
  address?: string;
  city?: string;
  zone?: string;
}

export async function addStop(stop: StopDto) {
  const response = await fetch("/api/stop", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(stop),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Failed to add stop: ${errorText}`);
  }

  return response.json();
}

export async function getAllStops(): Promise<StopDto[]> {
  const response = await fetch("/api/stop");
  if (!response.ok) {
    throw new Error("Failed to fetch stops");
  }
  return response.json();
}
