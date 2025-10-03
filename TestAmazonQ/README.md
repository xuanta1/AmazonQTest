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

1. Build project:
```bash
dotnet build
```

2. Chạy ứng dụng:
```bash
dotnet run
```

3. Truy cập Swagger UI: `https://localhost:7xxx/swagger`

## Cách test với Swagger

1. Mở Swagger UI
2. Thực hiện login với username: `admin`, password: `password`
3. Copy token từ response
4. Click nút "Authorize" trên Swagger UI
5. Nhập: `Bearer <token>`
6. Test các protected endpoints

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