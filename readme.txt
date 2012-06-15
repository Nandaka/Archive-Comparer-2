Features
===========
- Search for duplicated archive content based on CRC32 value.

Requirement
===========
- Microsoft .Net Framework 4
- 7zip dll (7z.dll) v9.10 or newer (http://www.7-zip.org/, check your OS-bit)
- SevenZipSharp v0.63.3849.28440 or newer (http://sevenzipsharp.codeplex.com/)
- log4net 1.2.11.0 or newer (http://logging.apache.org/log4net/)

Build Notes
===========
- Download all the required library and put it inside 
  'ArchiveComparer2.Library/lib' before compiling.

Changelog
===========
- 20120325
  - Fix stop button behaviour.

- 20120121
  - Fix noMatch crc detection.