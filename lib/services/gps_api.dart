import 'package:http/http.dart' as http;
import 'dart:convert';

Future<void> sendLocation(double lat, double lon) async {
  await http.post(
    Uri.parse("http://192.168.1.112:5259/api/gps/update"),
    headers: {"Content-Type": "application/json"},
    body: jsonEncode({
      "vehicleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "latitude": lat,
      "longitude": lon,
      "speedKmh": 0,
      "directionDegrees": 0
    }),
  );
}
