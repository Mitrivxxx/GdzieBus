import 'dart:async';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geolocator/geolocator.dart';
import 'package:flutter/services.dart';
import 'services/gps_api.dart';


void main() => runApp(const MyApp());

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: FullScreenMap(),
    );
  }
}

class FullScreenMap extends StatefulWidget {
  const FullScreenMap({super.key});

  @override
  State<FullScreenMap> createState() => _FullScreenMapState();
}

class _FullScreenMapState extends State<FullScreenMap> {
  // Subskrypcja strumienia pozycji
  StreamSubscription<Position>? _positionSub;
  DateTime? _lastSentTime;
  Position? _lastPos; // przechowywana ostatnia pozycja do wyświetlenia

  @override
  void initState() {
    super.initState();
    _ensurePermission();
    _startLocationStream();
  }

  Future<void> _ensurePermission() async {
    final serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      // Możesz pokazać dialog albo snackBar
      return;
    }

    LocationPermission permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
    }
    if (permission == LocationPermission.deniedForever) {
      // Tu możesz poinformować użytkownika o konieczności wejścia w ustawienia systemowe
      return;
    }

    // Pobierz bieżącą pozycję i zaktualizuj overlay
    try {
      final pos = await Geolocator.getCurrentPosition();
      _lastPos = pos;
      if (mounted) setState(() {});
      final controller = await _controller.future;
      controller.animateCamera(CameraUpdate.newLatLng(
        LatLng(pos.latitude, pos.longitude),
      ));
    } catch (e) {
      debugPrint('Nie udało się pobrać początkowej pozycji: $e');
    }
  }
  
  Future<void> _centerOnUser() async {
    try {
      final pos = await Geolocator.getCurrentPosition();
      if (!mounted) return; // Bezpieczne użycie BuildContext po await
      final controller = await _controller.future;
      await controller.animateCamera(
        CameraUpdate.newLatLngZoom(
          LatLng(pos.latitude, pos.longitude),
          16,
        ),
      );
    } catch (e) {
      if (!mounted) return; // Widget mógł zostać zniszczony
      debugPrint('Błąd pobierania lokalizacji: $e');
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Nie udało się pobrać lokalizacji')),
      );
    }
  }
  // Kontroler do zarządzania kamerą mapy (opcjonalne, ale przydatne)
  final Completer<GoogleMapController> _controller = Completer();

  // Pozycja początkowa kamery (np. Warszawa)
  // Format: LatLng(szerokość, długość)
  static const CameraPosition _kInitialPosition = CameraPosition(
    target: LatLng(52.2297, 21.0122),
    zoom: 14.0, // Poziom przybliżenia
  );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // Jeśli chcesz mapę pod paskiem statusu (totalny fullscreen),
      // ustaw extendBodyBehindAppBar na true (jeśli masz AppBar)
      // lub po prostu nie używaj AppBar.
      body: Stack(
        children: [
          GoogleMap(
            mapType: MapType.normal,
            initialCameraPosition: _kInitialPosition,
            onMapCreated: (GoogleMapController controller) {
              _controller.complete(controller);
            },
            zoomControlsEnabled: true,
            myLocationButtonEnabled: true,
            myLocationEnabled: true,
          ),
          // Panel z aktualną pozycją
          Positioned(
            top: 40,
            left: 16,
            child: Container(
              padding: const EdgeInsets.all(8),
              decoration: BoxDecoration(
                color: Colors.black.withOpacity(0.55),
                borderRadius: BorderRadius.circular(8),
              ),
              child: DefaultTextStyle(
                style: const TextStyle(color: Colors.white, fontSize: 12),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Text(_lastPos == null ? 'Brak danych GPS' : 'LAT: ${_lastPos!.latitude.toStringAsFixed(6)}'),
                    if (_lastPos != null)
                      Text('LON: ${_lastPos!.longitude.toStringAsFixed(6)}'),
                    if (_lastPos != null)
                      Text('ALT(m): ${_lastPos!.altitude.toStringAsFixed(1)} ACC(m): ${_lastPos!.accuracy.toStringAsFixed(1)}'),
                    if (_lastPos != null)
                      Text('SPD(km/h): ${( _lastPos!.speed * 3.6).toStringAsFixed(1)} DIR: ${_lastPos!.heading.toStringAsFixed(0)}°'),
                    const SizedBox(height:4),
                    Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        ElevatedButton(
                          style: ElevatedButton.styleFrom(padding: const EdgeInsets.symmetric(horizontal:10, vertical:6)),
                          onPressed: _lastPos == null ? null : () async {
                            final p = _lastPos!;
                            final text = 'lat=${p.latitude}\nlon=${p.longitude}\nalt=${p.altitude}\nacc=${p.accuracy}\nspeedKmh=${(p.speed*3.6).toStringAsFixed(2)}\nheadingDeg=${p.heading}';
                            await Clipboard.setData(ClipboardData(text: text));
                            if (!mounted) return;
                            ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('Skopiowano pozycję')));
                          },
                          child: const Text('KOPIUJ'),
                        ),
                        const SizedBox(width:8),
                        ElevatedButton(
                          style: ElevatedButton.styleFrom(padding: const EdgeInsets.symmetric(horizontal:10, vertical:6)),
                          onPressed: () => testPing(),
                          child: const Text('PING'),
                        ),
                        const SizedBox(width:8),
                        ElevatedButton(
                          style: ElevatedButton.styleFrom(padding: const EdgeInsets.symmetric(horizontal:10, vertical:6)),
                          onPressed: _manualSendCurrentPosition,
                          child: const Text('WYŚLIJ'),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          ),
          // Prosty bloczek z samymi koordynatami (lat/lon)
          Positioned(
            bottom: 16,
            left: 16,
            child: Container(
              padding: const EdgeInsets.symmetric(horizontal:12, vertical:8),
              decoration: BoxDecoration(
                color: Colors.blueGrey.withOpacity(0.75),
                borderRadius: BorderRadius.circular(8),
              ),
              child: Text(
                _lastPos == null
                    ? 'Oczekiwanie na GPS...'
                    : 'LAT: ${_lastPos!.latitude.toStringAsFixed(6)}\nLON: ${_lastPos!.longitude.toStringAsFixed(6)}',
                style: const TextStyle(color: Colors.white, fontSize: 13, height: 1.3),
              ),
            ),
          ),
        ],
      ),

      // Opcjonalnie: Dodaj przycisk pływający nad mapą
      floatingActionButton: FloatingActionButton(
        onPressed: _centerOnUser,
        child: const Icon(Icons.center_focus_strong),
      ),
      // Ważne: Ustawienie lokalizacji przycisku, aby nie zasłaniał logo Google
      floatingActionButtonLocation: FloatingActionButtonLocation.startFloat,
    );
  }

  void _startLocationStream() {
    final settings = const LocationSettings(
      accuracy: LocationAccuracy.high,
      distanceFilter: 0, // 0 aby otrzymać pierwszą próbkę bez ruchu
    );
    _positionSub = Geolocator.getPositionStream(locationSettings: settings)
        .listen((pos) async {
      _lastPos = pos; // aktualizuj daną do panelu
      if (mounted) setState(() {});
      // Throttle wysyłkę (np. co >=5s)
      final now = DateTime.now();
      if (_lastSentTime != null && now.difference(_lastSentTime!) < const Duration(seconds: 5)) {
        return;
      }
      _lastSentTime = now;
      debugPrint('SEND: lat=${pos.latitude} lon=${pos.longitude} alt=${pos.altitude} acc=${pos.accuracy}');

      // Wyślij do backendu (TODO: podmień vehicleId oraz endpoint na produkcyjny)
      await sendPositionToBackend(
        vehicleId: '3fa85f64-5717-4562-b3fc-2c963f66afa6', // TODO: dynamiczny identyfikator
        lat: pos.latitude,
        lng: pos.longitude,
        altitudeMeters: pos.altitude,
        accuracyMeters: pos.accuracy,
        speedKmh: pos.speed * 3.6,
        directionDeg: pos.heading,
      );
    });
  }

  Future<void> _manualSendCurrentPosition() async {
    try {
      final pos = await Geolocator.getCurrentPosition();
      _lastPos = pos;
      if (mounted) setState(() {});
      await sendLocation(pos.latitude, pos.longitude);
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(const SnackBar(content: Text('Wysłano bieżącą pozycję')));
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Błąd wysyłania pozycji: $e')));
    }
  }

  @override
  void dispose() {
    _positionSub?.cancel();
    super.dispose();
  }
}

