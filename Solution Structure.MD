src/
├── Application/                  # Application Layer
│   ├── Commands/                  # Command handlers
│   ├── Queries/                   # Query handlers
│   ├── Services/                  # Application services
│   ├── DTOs/                      # Data Transfer Objects
│   └── Contracts/                 # Interfaces for application layer
│
├── Domain/                        # Core Domain Layer
│   ├── Models/                    # Aggregates, Entities, Value Objects
│   ├── Events/                    # Domain events
│   ├── Enums/                     # Domain-specific enums
│   ├── Services/                  # Domain services
│   ├── Policies/                  # Business rules/policies
│   ├── Specifications/            # Business specifications
│   ├── Exceptions/                # Domain exceptions
│   └── Interfaces/                # Repository interfaces, domain interfaces
│
├── Infrastructure/                # Infrastructure Layer
│   ├── Persistence/               # Database context, migrations
│   │   ├── Repositories/          # Repository implementations
│   │   └── Configurations/        # Entity configurations
│   ├── Identity/                  # Authentication/authorization
│   ├── Caching/                   # Caching implementations
│   ├── Messaging/                 # Event bus, message brokers
│   ├── FileStorage/               # File storage implementations
│   └── ExternalServices/          # External service integrations
│
├── Presentation/                  # Presentation Layer
│   ├── WebAPI/                    # REST API controllers
│   ├── GraphQL/                   # GraphQL endpoints
│   ├── gRPC/                      # gRPC services
│   └── Contracts/                 # API contracts (DTOs)
│
└── Tests/                         # Test projects
    ├── UnitTests/                 # Unit tests
    ├── IntegrationTests/          # Integration tests
    └── EndToEndTests/            # End-to-end tests