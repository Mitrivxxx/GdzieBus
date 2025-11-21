import 'dart:async';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:geolocator/geolocator.dart';


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
  @override
  void initState() {
    super.initState();
    _ensurePermission();
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

    final pos = await Geolocator.getCurrentPosition();
    final controller = await _controller.future;
    controller.animateCamera(CameraUpdate.newLatLng(
    LatLng(pos.latitude, pos.longitude),
    ));
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
      body: GoogleMap(
        mapType: MapType.normal, // Typ mapy: normalna, satelitarna, hybrydowa
        initialCameraPosition: _kInitialPosition,
        onMapCreated: (GoogleMapController controller) {
          _controller.complete(controller);
        },
        // Opcjonalnie: ukryj przyciski zoomu i moją lokalizację dla czystego wyglądu
        zoomControlsEnabled: true,
        myLocationButtonEnabled: true,
        myLocationEnabled: true,
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
}

