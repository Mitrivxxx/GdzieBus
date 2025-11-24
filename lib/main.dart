import 'dart:async';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geolocator/geolocator.dart';
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
  Position? _lastSentPos;

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
      body: GoogleMap(
        mapType: MapType.normal,
        initialCameraPosition: _kInitialPosition,
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
        zoomControlsEnabled: true,
        myLocationButtonEnabled: true,
        myLocationEnabled: true,
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _centerOnUser,
        child: const Icon(Icons.center_focus_strong),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.startFloat,
    );
  }

  void _startLocationStream() {
    _positionSub = Geolocator.getPositionStream(
      locationSettings: const LocationSettings(
        accuracy: LocationAccuracy.high,
        distanceFilter: 0, // Pobieraj ciągle, filtrujemy ręcznie
      ),
    ).listen((pos) {
      bool shouldSend = false;
      final now = DateTime.now();

      if (_lastSentPos == null || _lastSentTime == null) {
        shouldSend = true;
      } else {
        final distance = Geolocator.distanceBetween(
          _lastSentPos!.latitude,
          _lastSentPos!.longitude,
          pos.latitude,
          pos.longitude,
        );
        final timeDiff = now.difference(_lastSentTime!);

        // Wyślij jeśli przesunięto o > 5m LUB minęło > 10s
        if (distance >= 5 || timeDiff.inSeconds >= 10) {
          shouldSend = true;
        }
      }

      if (shouldSend) {
        _lastSentPos = pos;
        _lastSentTime = now;
        sendLocation(pos.latitude, pos.longitude);
      }
    });
  }

  @override
  void dispose() {
    _positionSub?.cancel();
    super.dispose();
  }
}

