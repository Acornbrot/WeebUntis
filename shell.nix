{ pkgs ? import <nixpkgs> {} }:

(pkgs.buildFHSEnv {
  name = "simple-x11-env";
  targetPkgs = pkgs: (with pkgs; [
    udev
    alsa-lib
    fontconfig
    glew
  ]) ++ (with pkgs.xorg; [
    # Avalonia UI
    libX11
    libICE
    libSM
    libXi
    libXcursor
    libXext
    libXrandr
  ]);
  multiPkgs = pkgs: (with pkgs; [
    udev
    alsa-lib
  ]);
  runScript = "nu";
}).env
