# MiPPPI
Microsoft PrintToPDF Post-Processing Printer Installer

# Description
A situation was encountered where it was desirable to perform post-processing on a document that was triggered for printing using ```Microsoft Print To PDF```. However, it will prompt the user for an output path that is not retrievable in an application. I.e. after an automated printing job, an application is unable to act on the result file. There are some (paid) third-party tools for managing this, which naturally, was not the preferred option.

In Windows this can be resolved by adding a new printer which refers to the ```Microsoft Print To PDF``` driver. A printer port can then be configured as an output path. Due to this action requiring manual input from user(s) or system administrators, it was decided to publish this tool (which can be embedded into other applications for easy configuration).

# Requirements
This software requires a device running on the Microsoft Windows Operating System with ```Microsoft Print To PDF``` installed.

# Usage
```MiPPPi.exe [printer name] [identifier] [filename]```

An application can trigger a print to the added printer. Since a scan result is set to the expected output path, the application can scan for new files inside the target directory.

The appdata (roaming) directory ```%appdata%``` contains the directory ```MiPPPI```. Inside this folder a subdirectory matching with ```identifier```is present (which could be a company or product name). Inside this folder the file ```filename``` will be present.

# Technical
If you want to persue this result without utilizing this application, that is also possible. In that case you will have to navigate to the control panel in Windows, where the ```Printers and Scanners``` can be opened. Click on ```Add Device``` and ```Add Manually```. Select the option to add a local printer with manual configuration. In the following prompt, choose to create a new port with type ```Local Port```. The name can be configured as a path to a file (PDF). Click on ```Windows Update``` to retrieve the full printer list. Under ```Microsoft``` pick the ```Microsoft Print to PDF``` option.
