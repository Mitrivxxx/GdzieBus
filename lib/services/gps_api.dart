import 'package:http/http.dart' as http;
import 'dart:convert';

// Prosty wrapper zgodny z wymaganym fragmentem w pytaniu
Future<void> sendLocation(double lat, double lng) async {
  // TODO: podmień IP/port na właściwe dla środowiska
  final url = Uri.parse("http://192.168.1.112:5259/api/gps-test");
  final body = jsonEncode({
    'Lat': lat,
    'Lng': lng,
  });
  final resp = await http.post(
    url,
    headers: const { 'Content-Type': 'application/json' },
    body: body,
  );
  print('[SEND LOCATION] status=${resp.statusCode} body=${resp.body}');
}

// Bardziej rozbudowana funkcja z dodatkowymi polami (używana przez strumień)
Future<void> sendPositionToBackend({
  required String vehicleId,
  required double lat,
  required double lng,
  double? altitudeMeters,
  double? accuracyMeters,
  double? speedKmh,
  double? directionDeg,
}) async {
  final url = Uri.parse('http://192.168.1.112:5259/api/gps'); // TODO: zmień na produkcyjny
  final payload = {
    'vehicleId': vehicleId,
    'lat': lat,
    'lng': lng,
    if (altitudeMeters != null) 'altitudeMeters': altitudeMeters,
    if (accuracyMeters != null) 'accuracyMeters': accuracyMeters,
    if (speedKmh != null) 'speedKmh': speedKmh,
    if (directionDeg != null) 'directionDegrees': directionDeg,
    'timestamp': DateTime.now().toIso8601String(),
  };
  final body = jsonEncode(payload);
  print('[GPS SEND] POST ${url.toString()} BODY: $body');
  try {
    final resp = await http.post(
      url,
      headers: const { 'Content-Type': 'application/json' },
      body: body,
    );
    print('[GPS RESP] ${resp.statusCode} ${resp.body}');
  } catch (e) {
    print('[GPS ERROR] $e');
  }
}

Future<void> testPing() async {
  try {
    final url = Uri.parse('http://192.168.1.112:5259/api/gps-test'); // dostosuj endpoint
    final resp = await http.get(url);
    print('[PING] ${resp.statusCode} ${resp.body}');
  } catch (e) {
    print('[PING ERROR] $e');
  }
}
