# PHẦN MỀM CHO HỆ POP-UP PHÂN LOẠI SẢN PHẨM HASAKI
![Hasaki Popup System](/assets/hasaki2.jpg)
### NGUYÊN LÝ HOẠT ĐỘNG
Kiện hàng được shipper vận chuyển về nhà máy và sau đó đưa lên băng chuyền để phân loại. Dựa vào barcode/QR code dán trên kiện hàng, Phần mềm đọc mã vạch truy vấn trong Database để nhận lại số cửa (zone) cần phân loại, sau đó gửi số cửa xuống băng chuyền để pop up kiện hàng vào zone.
### SƠ ĐỒ KẾT NỐI
![Hasaki Popup System](/assets/hasaki_diagram.PNG)
### CÁC CHỨC NĂNG
- Đọc mã vạch, khối lượng, kích thước của kiện hàng, gửi số cửa chia xuống băng chuyền và lưu lịch sử.
- Kết nối với Bộ điều khiển PLC Mitsubishi qua giao thức Modbus TCP.
- Kết nối với Đầu đọc mã vạch Cognex Dataman 360 sử dụng SDK do hãng phát triển.
- Kết nối với Camera đo kích thước Cognex Dimension 3D-A1000 qua kết nối TCP/IP.
- Kết nối với Băng tải cân sử dụng giao thức Modbus Rs-485.
### CÁC CÔNG NGHỆ SỬ DỤNG
- Windows Forms
- MS SQL Server

