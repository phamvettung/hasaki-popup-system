# Phần mềm cho Hệ thống Pop-up phân loại sản phẩm
![Hasaki Popup System](/assets/hasaki2.jpg)
### NGUYÊN LÝ HOẠT ĐỘNG
Kiện hàng được shipper vận chuyển về nhà máy và sau đó đưa lên băng chuyền để phân loại. Dựa vào mã Barcode và QR code dán trên kiện hàng, Phần mềm đọc mã vạch và gọi API để nhận lại số cửa (zone) cần phân loại, sau đó gửi số cửa xuống băng chuyền để pop-up kiện hàng vào zone.
### CÁC CHỨC NĂNG
- Đọc mã vạch, khối lượng, kích thước của kiện hàng, gửi số cửa chia xuống băng chuyền và lưu lịch sử.
- Kết nối với Bộ điều khiển PLC Mitsubishi qua giao thức Modbus TCP.
- Kết nối với Đầu đọc mã vạch Cognex Dataman 360 sử dụng SDK do hãng phát triển.
- Kết nối với Camera đo kích thước Cognex Dimension 3D-A1000 qua kết nối TCP/IP.
- Kết nối với Băng tải cân sử dụng giao thức Modbus Rs-485.
### CÁC CÔNG NGHỆ SỬ DỤNG
- Windows Forms, MS SQL Server
- Ngôn ngữ: C#
### HOẠT ĐỘNG HỆ THỐNG
https://github.com/user-attachments/assets/1bf8ff31-df06-4624-83cf-f6f360fa19f3

