export async function getLastPosition(vehicleId: string) {
  const res = await fetch(`/api/gps/last-position/${vehicleId}`);

  if (!res.ok) throw new Error("Błąd pobierania pozycji");
  return res.json();
}
