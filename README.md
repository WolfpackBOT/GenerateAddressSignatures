# GenerateAddressSignatures
This program is used to generate signatures for registering your WOLF addresses for the snapshot.

It is written in C# .Net Core, with publishing targets for Windows, MacOS, and Linux. These ready to use, compiled archives are located in the dist folder. Currently, Windows is the only operating system tested. We will get the MacOS and Linux versions tested soon.

### Downloads
[GenerateAddressSignatures-win10-x64.zip - Windows](https://wolfpackbotdownloads.azureedge.net/wolfpackbotdownloadscontainer/tools/GenerateAddressSignatures-win10-x64.zip)

### Requirements
1. Make sure you have your wolfcoin.conf configured to allow RPC calls. If you have to modify the conf, you will need to restart your wallet after making the changes.

To modify the config file, within the QT wallet menu, go to Tools >> Open Wallet Configuration file.

~~~~
rpcuser=A USERNAME THAT YOU SET
rpcpassword=A STRONG PASSWORD
listen=1
server=1
daemon=1
~~~~

2. wolfcoin-cli.exe must be within your wallet's install directory, and you need to know the location of this file.

If you don't know the location, leave your wallet open, then open Task Manager. Locate the Wolfcoin Core process in the list of running processes, right click it and click Open file location.

### Instructions
1. Download and extract the archive.
2. Modify the parameters (see below) in run.bat (or run.sh depending on your operating system).
3. Execute run.bat or run.sh
4. Once the program completes, copy the contents of the generated file.
5. On the website on the Snapshot page, click Claim Many WOLF Addresses.
6. Paste the generated signatures into the textbox and click Claim.

### Run Parameters
1. Absolute file path to the wolfcoin-cli file.

Again, if you don't know the location, leave your wallet open, then open Task Manager. Locate the Wolfcoin Core process in the list of running processes, right click it and click Open file location.

2. Text file name (and optional full path) for the resulting output file that will contain all of your addresses with the signatures.

### run.bat Example
I stored my wallet at: C:\Users\MyUsername\Wolfcoin Wallet <br/>
So, the contents of the run.bat file would look similar to this <br/><br/>
`win10-x64\GenerateAddressSignatures.exe "C:\Users\MyUsername\Wolfcoin Wallet\wolfcoin-cli.exe" "WalletSignatures.txt"`
