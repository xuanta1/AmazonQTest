# JWT Web API - .NET 7

Project Web API sử dụng .NET 7 với JWT Authentication

## Cấu trúc Project

```
JwtWebApi/
├── Controllers/
│   ├── AuthController.cs      # API login/logout
│   └── TestController.cs      # API test authentication
├── Models/
│   ├── LoginRequest.cs        # Model cho login request
│   └── LoginResponse.cs       # Model cho login response
├── Services/
│   └── JwtService.cs          # Service tạo JWT token
├── Program.cs                 # Cấu hình ứng dụng
└── appsettings.json          # Cấu hình JWT
```

## Các API Endpoints

### 1. Login
- **POST** `/api/auth/login`
- **Body**: 
```json
{
  "username": "admin",
  "password": "password"
}
```
- **Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expires": "2024-01-01T12:00:00Z"
}
```

### 2. Logout
- **POST** `/api/auth/logout`
- **Headers**: `Authorization: Bearer <token>`
- **Response**:
```json
{
  "message": "Logged out successfully"
}
```

### 3. Test Endpoints
- **GET** `/api/test/public` - Endpoint công khai
- **GET** `/api/test/protected` - Endpoint cần authentication

## Cách chạy ứng dụng

### Prerequisites
- .NET 8 SDK
- Git

### Cài đặt
1. Clone repository:
```bash
git clone https://github.com/xuanta1/TestAmazonQ.git
cd TestAmazonQ
```

2. Restore packages:
```bash
dotnet restore
```

3. Navigate to project:
```bash
cd TestAmazonQ
```

### Chạy ứng dụng

#### Tùy chọn 1: Chạy bình thường (giữ data cũ)
```bash
# Sử dụng script
reset-db.bat
# HOẶC
run.bat
# HOẶC manual
dotnet run
```

#### Tùy chọn 2: Reset database và chạy
```bash
reset-db.bat --reset
```

#### Tùy chọn 3: Quản lý database thủ công
```bash
# Xóa và tạo lại database
dotnet ef database drop --force
dotnet ef database update

# Sau đó chạy ứng dụng
dotnet run
```

### Truy cập ứng dụng
- **Swagger UI:** `https://localhost:7xxx/swagger`
- **API Base URL:** `https://localhost:7xxx/api`

### Tài khoản mặc định
- **Username:** `admin`
- **Password:** `admin123`

### Quick Start Scripts

| Script | Mô tả |
|--------|-------|
| `run.bat` | Chạy ứng dụng bình thường |
| `reset-db.bat` | Chạy ứng dụng (giữ data) |
| `reset-db.bat --reset` | **Reset database và chạy** |

## Cách test với Swagger

1. Mở Swagger UI
2. Thực hiện login với username: `admin`, password: `admin123`
3. Copy token từ response
4. Click nút "Authorize" trên Swagger UI
5. Nhập: `Bearer <token>`
6. Test các protected endpoints

## Testing

Chạy unit tests:
```bash
cd TestAmazonQ.Tests
dotnet test
```

## Database Reset Options

1. **Sử dụng Script (Khuyến nghị)**
   ```bash
   reset-db.bat --reset
   ```

2. **Xóa file thủ công**
   ```bash
   del app.db
   dotnet run
   ```

3. **Entity Framework Commands**
   ```bash
   dotnet ef database drop --force
   dotnet ef database update
   ```

## Cấu hình JWT

Trong `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "MySecretKeyForJwtTokenGeneration123456789",
    "Issuer": "JwtWebApi",
    "Audience": "JwtWebApiUsers"
  }
}
```

## Tính năng

- ✅ JWT Authentication
- ✅ Login/Logout API
- ✅ Swagger UI với JWT support
- ✅ Protected endpoints
- ✅ Token validation
- ✅ Minimal code implementation