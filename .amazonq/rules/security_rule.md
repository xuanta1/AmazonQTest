# Quy tắc Bảo mật và Vận hành (Security & Operational Rules)

Mục đích của quy tắc này là để đảm bảo tất cả code được Amazon Q tạo ra hoặc sửa đổi tuân thủ các phương pháp bảo mật tốt nhất (Security Best Practices), đặc biệt trong các ứng dụng Web và Cloud.

## 1. Bảo mật đầu vào (Input Validation & Sanitization)

-   **KHÔNG BAO GIỜ** tin tưởng bất kỳ dữ liệu đầu vào nào từ người dùng, file, hoặc nguồn bên ngoài.
-   **Luôn luôn** thực hiện kiểm tra dữ liệu đầu vào (Input Validation) nghiêm ngặt (kiểm tra kiểu dữ liệu, độ dài, định dạng, dải giá trị cho phép).
-   **Luôn luôn** thực hiện xử lý vệ sinh dữ liệu (Sanitization) và **Escape** dữ liệu trước khi chèn vào:
    * **Cơ sở dữ liệu (Database):** Sử dụng các câu lệnh tham số hóa (Parameterized Queries) hoặc ORM (Object-Relational Mapping) để ngăn chặn tấn công **SQL Injection**. Tuyệt đối không dùng nối chuỗi để tạo query.
    * **HTML/DOM:** Sử dụng các hàm mã hóa (HTML Encoding) để ngăn chặn tấn công **Cross-Site Scripting (XSS)**.

## 2. Quản lý bí mật (Secrets Management)

-   **TUYỆT ĐỐI KHÔNG** nhúng bất kỳ thông tin nhạy cảm nào (như chuỗi kết nối cơ sở dữ liệu, API keys, mật khẩu, private keys) trực tiếp vào mã nguồn hoặc file cấu hình plaintext.
-   **Luôn luôn** sử dụng dịch vụ quản lý bí mật (Secrets Management Service) của AWS (ví dụ: **AWS Secrets Manager**) hoặc các biến môi trường được mã hóa/an toàn để truy cập thông tin nhạy cảm.
-   Nếu tạo mã liên quan đến token hoặc khóa, hãy đảm bảo rằng chúng được xử lý an toàn (ví dụ: sử dụng HTTPS/SSL, giới hạn phạm vi, đặt thời hạn hết hạn).

## 3. Quản lý phiên (Session Management)

-   **Luôn luôn** sử dụng các cơ chế xác thực và quản lý phiên an toàn.
-   **KHÔNG** lưu trữ thông tin nhạy cảm (như vai trò, quyền) trực tiếp trong cookie phía client mà không được mã hóa.
-   Đặt thời gian hết hạn (expiration) ngắn cho các phiên làm việc và yêu cầu đăng nhập lại thường xuyên nếu giao dịch nhạy cảm.

## 4. Ghi nhật ký và Xử lý lỗi (Logging and Error Handling)

-   **KHÔNG BAO GIỜ** ghi nhật ký (log) thông tin nhạy cảm của người dùng (như mật khẩu, số thẻ tín dụng, PAT, SSH Keys) vào file log. Chỉ ghi nhật ký các thông tin cần thiết cho việc debug và vận hành.
-   Thông báo lỗi cho người dùng cuối phải chung chung và không tiết lộ chi tiết về lỗi (ví dụ: không hiển thị stack trace, tên database, hoặc cấu trúc hệ thống).

## Hướng dẫn ưu tiên cho Amazon Q

Khi Amazon Q Developer thực hiện các tác vụ tạo code tương tác với I/O, database, hoặc xử lý thông tin nhạy cảm, hãy ưu tiên áp dụng các quy tắc bảo mật trên. Nếu có lựa chọn giữa một phương pháp nhanh chóng nhưng kém bảo mật và một phương pháp chậm hơn nhưng an toàn, **hãy chọn phương pháp an toàn hơn.**