# HASAKI PRODUCT CLASSIFICATION POPUP SYSTEM
![Hasaki Popup System](/assets/hasaki2.jpg)

### OVERVIEW
The parcel is stransported by the shipper to the Factory and then put on the conveyor belt for sorting. Based on the barcode/QR code on parcel, the software read barcode, query in the Database to receives back zone number, and then send zone number to the conveyor belt pop up parcel into zone.

### CONNECTION DIAGRAM
![Hasaki Popup System](/assets/hasaki_diagram.PNG)

### FEATURES
- Read barcode, weight, size of parcel, send zone number to PLC and save history.
- Connect to PLC controller using Modbus TCP.
- Connect to the Cognex Dataman 360 using SDK.
- Connect to the 3D A1000 using TCP/IP.
- Connect to the Weight conveyor using Modbus RS-485.
  
### FRAMEWORK USED
- C# Windows Forms

