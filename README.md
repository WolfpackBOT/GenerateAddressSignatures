# GenerateAddressSignatures
This program is used to generate signatures for registering your WOLF addresses for the snapshot.

It is written in C# .Net Core, with publishing targets for Windows, MacOS, and Linux. These ready to use, compiled archives are located in the in the dist folder.

### Instructions
1. Download the archive from the dist folder for your operating system.
2. Decompress file
3. Modify the parameters (see below) in run.bat (or run.sh depending on your operating system).
4. Execute run.bat or run.sh

### Run Parameters
1. Absolute file path to the wolfcoin-cli file.
2. Text file name (and optional full path) for the resulting output file that will contain all of your addresses with the signatures.
