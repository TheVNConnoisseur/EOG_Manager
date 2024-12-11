# EOG Manager
Program that lets the user create .EOG files used in games developed by ANiM.

### Notes on its usage
1. The program will check if the file has the correct sampling rate and is an actual OGG file, otherwise it will refuse to convert it to .EOG.

### How are EOG files structured?
While the code also documents how a .EOG file is structured, here it is also the same information on a more accessible manner:
The file is divided into 2 parts:
  * **Header**: EOG Signature (4 bytes) + (Number of samples * 4) (4 bytes)
  * **Raw data**: a bog-standard OGG Vorbis file, complete with its standard header.

Besides that, the only other thing important to take note is that the frequency rate is 44.1 KHz.

## Licenses
[NVorbis](https://www.nuget.org/packages/NVorbis/) - MIT License
