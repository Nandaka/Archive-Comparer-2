Features
===========
- Search for duplicated archive content based on CRC32 value from each file inside the archive.
- Delete to recycle bin/permanent delete.
- Use 7z.dll for archive access (download separately).

Requirement
===========
- Microsoft .Net Framework 4
- 7zip dll (7z.dll) v9.10 or newer (http://www.7-zip.org/, check your OS-bitness)
- SevenZipSharp v0.63.3849.28440 or newer (http://sevenzipsharp.codeplex.com/)
- log4net 1.2.11.0 or newer (http://logging.apache.org/log4net/)

Configuration
===========
- Matching Setting
  - File Pattern      : Whitelist pattern for archive file (using regex), 
                        e.g.: zip$|rar$|cbz$|cbr$|7z$
  - Blacklist Pattern : Blacklist pattern for file inside the archive (using regex)
                        e.g.: humbs.db$|pdf$|nfo$|txt$|zip$|rar$|^__MACOSX|license|DS_Store|^\.
- Application
  - Enable Textbox Logging: Log to Log Tab, slow! Use only for checking if the 7z.dll is loaded.
  - 7z.dll Path       : The 7z.dll location, try to use the 32-bit first.
  - Thread Priority   : Drop down list to set the thread priority, 
                        Recommended to set to lowest except you are using quad core.
  - Reset Column Width: Reset the column width (duh!), usefull when the frozen list is too wide.
- Logging: Detailed logging flag to be written to log file.


Build Notes
===========
- Download all the required library and put it inside 
  'ArchiveComparer2.Library/lib' before compiling.

Changelog
===========
- 20130630
  - Add icon.
  - Add configuration update handler.

- 20130519
  - Fix bug for clear resolved button. 

- 20130201
  - Add auto select button for Original, Equal Count, or Subset match.

- 20120922
  - Add separator for each duplicated group.
  - Add confirmation when closing the form if there still any entry.

- 20120325
  - Fix stop button behaviour.

- 20120121
  - Fix noMatch crc detection.