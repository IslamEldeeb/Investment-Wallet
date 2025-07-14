# Dinar Investments API

Dinar Investments is a RESTful API for managing investors, investments, and wallets.

## Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL 


### Running the API

1. Clone the repository:
   ```
   git clone https://github.com/IslamEldeeb/Investment-Wallet.git
   cd DinarInvestments
   ```

2. Restore dependencies:
   ```
   dotnet restore
   ```

3. Update `appsettings.json` with your database connection string.

4. Run database migrations:
   ```
   dotnet ef database update
   ```

5. Start the API:
   ```
   dotnet run --project DinarInvestments.API
   ```

The API will be available at `http://localhost:5037` 

## Available APIs

### Investor Endpoints

- `GET /api/investor/getAll`  
  Get all investors.

- `POST /api/investor/create`  
  Create a new investor.  
  **Body:**
  ```json
  {
    "name": "Islam Eldeeb",
    "email": "islameldeeb@outlook.com"
  }
  ```

- `PUT /api/investor/update/{id}`  
  Update investor info.  
  **Body:**
  ```json
  {
    "name": "Islam Eldeeb",
    "email": "islameldeeb@outlook.com"
  }
  ```

- `POST /api/investor/fundWallet`  
  Fund an investor's wallet.  
  **Body:**
  ```json
  {
    "investorId": 1,
    "amount": 1000
  }
  ```

- `GET /api/investor/balance/{investorId}`  
  Get the balance of an investor's wallet.

### Investment Endpoints

- `POST /api/investment/invest`  
  Invest in an opportunity.  
  **Body:**
  ```json
  {
    "investorId": 1,
    "opportunityId": 2,
    "amount": 500
  }
  ```

