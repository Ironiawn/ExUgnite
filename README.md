# ExUgnite
The Ugnite Client (Steam/EGS/GOG 'clone') made in C# (not updated anymore)

Includes a auto-updater and a tiny Unity Engine API

The project is just a concept now and have several security issues(!!)

Please note:

- The passwords on the code and the servers doesn't not work anymore
- The project is outdated (mid 2019)
- Framework target: 4.7.2
- Uses MySQL
- Can use AWS S3/Azure/Google Cloud or other S3 based service to host files
- Can open games (or any other kind of software) via .URL shortcut made at user's desktop after the download and installation (like Steam)
- Have a tiny 'thropies system' (use 'Unity API' to insert on DB)

# Package Requirements (NuGet)
Those packages are listed in 'packages.config' file

 - BouncyCastle
 - cef.redist.x64
 - cef.redist.x86
 - CefSharp.Common
 - CefSharp.WinForms
 - Google.Protobuf
 - ini-parser
 - MySql.Data
 - SharpCompress
 
# Other Additions #
The project includes the following:

- ISS Installer file
- .NET Reactor projects for both updater and client

# Outro #

Don't judge me, I'm a noob!
