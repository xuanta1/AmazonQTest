# Security Guidelines

## Environment Variables Required

Before running the application, set these environment variables:

```bash
# JWT Secret Key (minimum 32 characters)
JWT_KEY=your-super-secure-jwt-key-here-minimum-32-chars

# Database Connection (if using external DB)
CONNECTION_STRING=your-secure-connection-string
```

## AWS Secrets Manager Integration

For production, use AWS Secrets Manager to store sensitive configuration:

1. Store JWT key in AWS Secrets Manager
2. Update Program.cs to retrieve secrets from AWS
3. Never commit secrets to source control

## Security Features Implemented

- ✅ Input validation on all user inputs
- ✅ Password hashing with BCrypt (work factor 12)
- ✅ JWT token validation and secure generation
- ✅ Security headers (XSS, CSRF protection)
- ✅ Error handling without information disclosure
- ✅ Parameterized queries (via Entity Framework)
- ✅ Authentication and authorization

## Security Checklist

- [ ] Replace hardcoded JWT key with environment variable
- [ ] Configure HTTPS in production
- [ ] Set up proper CORS policy
- [ ] Enable request rate limiting
- [ ] Configure secure session management
- [ ] Set up proper logging (without sensitive data)
- [ ] Regular security updates for dependencies