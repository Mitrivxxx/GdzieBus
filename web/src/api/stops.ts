export interface StopDto {
  stopName: string;
  stopCode?: string;
  latitude?: number;
  longitude?: number;
  address?: string;
  city?: string;
  zone?: string;
}
const API_STOP = "api/stop";

export async function addStop(stop: StopDto) {
  const response = await fetch(API_STOP, {
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
  const response = await fetch(API_STOP);
  if (!response.ok) {
    throw new Error("Failed to fetch stops");
  }
  return response.json();
}
