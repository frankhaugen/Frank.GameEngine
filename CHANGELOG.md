# Changelog

All notable changes to this project are documented here. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [0.1.0] - 2026-03-31

### Added

- NuGet-ready **0.1.0** versioning and **per-package descriptions** for shipping libraries under `src/`.
- `GameEngine.Shutdown()` and `IDisposable` for coordinated teardown of background input/audio work.
- `dotnet pack` verification in CI.

### Fixed

- `Frank.GameEngine.Audio.Ogg.AudioPlayer`: implemented `PlayLooping` and `Stop` (previously `NotImplementedException`); `Play` no longer disposes the output device before playback can start.
- `PackageProjectUrl` corrected to `https://github.com/frankhaugen/Frank.GameEngine`.
- `PackageRequireLicenseAcceptance` set to `false` for typical OSS consumption.

### Changed

- `Frank.GameEngine.Rendering.Experimental` is **not** packed (`IsPackable=false`).

