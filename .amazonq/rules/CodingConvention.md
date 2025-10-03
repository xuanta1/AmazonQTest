# Quy tắc Coding Convention cho C# (.NET)

Mục đích của quy tắc này là để đảm bảo tất cả code C# được tạo, tái cấu trúc hoặc sửa đổi bởi Amazon Q tuân thủ các tiêu chuẩn C# được áp dụng trong dự án.

## 1. Quy tắc đặt tên (Naming Conventions)

Amazon Q phải tuân thủ nghiêm ngặt quy tắc PascalCase và camelCase của C#:

| Thành phần | Quy tắc | Ví dụ |
| :--- | :--- | :--- |
| **Class, Method, Property, Namespace** | **PascalCase** | `public class UserService { public string FullName { get; set; } }` |
| **Biến cục bộ (local variables)** | **camelCase** | `var localVariable = 0;` |
| **Tham số phương thức (method parameters)** | **camelCase** | `public void SetValue(int newValue)` |
| **Hằng số (Constants/readonly fields)** | **PascalCase** | `public const int MaxSize = 100;` |
| **Trường riêng tư (Private fields)** | **camelCase**, có thể thêm tiền tố `_` | `private readonly ILogger _logger;` |

## 2. Thụt lề và Khoảng trắng (Indentation & White Space)

-   Luôn sử dụng **4 dấu cách (spaces)** để thụt lề (KHÔNG dùng Tab).
-   Sử dụng **khoảng trắng** xung quanh các toán tử nhị phân (ví dụ: `a = b + c;`).
-   Dấu ngoặc nhọn `{` phải nằm trên **cùng dòng** với câu lệnh mà nó thuộc về (K&R style không được chấp nhận).

    ```csharp
    // CHUẨN
    public void MyMethod() 
    {
        if (condition)
        {
            // code
        }
    }
    ```

## 3. Khai báo kiểu dữ liệu và từ khóa

-   Ưu tiên sử dụng từ khóa kiểu tích hợp sẵn của C# thay vì tên lớp .NET Framework (ví dụ: dùng `int` thay vì `Int32`, dùng `string` thay vì `String`).
-   Sử dụng từ khóa `var` khi kiểu dữ liệu rõ ràng từ ngữ cảnh (ví dụ: khởi tạo đối tượng mới), nhưng **tránh** dùng `var` khi kiểu dữ liệu bị mơ hồ.

## 4. Ghi chú (Comments & Documentation)

-   Tất cả các thành phần công cộng (`public`) phải có **XML Documentation Comments** (ví dụ: `/// <summary>...</summary>`) để mô tả chức năng, tham số và giá trị trả về.
-   Hạn chế các bình luận nội tuyến (`//`) để giải thích code rõ ràng hơn; thay vào đó, hãy cố gắng làm cho code tự giải thích.

## Hướng dẫn ưu tiên cho Amazon Q

Khi Amazon Q tạo hoặc sửa đổi code C#, hãy xem các quy tắc này là **ưu tiên cao nhất** so với bất kỳ phong cách nào khác. Đặc biệt, hãy kiểm tra và đảm bảo các quy tắc đặt tên (`PascalCase`/`camelCase`) và quy tắc thụt lề (`4 spaces`) luôn được áp dụng.