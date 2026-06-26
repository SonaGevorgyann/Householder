# Householder QR Solver

A web application that solves systems of linear equations using the **Householder QR decomposition** algorithm. The project consists of an **ASP.NET Core Web API** backend and a lightweight **HTML/CSS/JavaScript** frontend, allowing users to enter a matrix and vector, compute the solution, and inspect intermediate calculation steps.

## Features

* Solve linear systems of the form **Ax = b**
* Implementation of the **Householder QR decomposition** algorithm
* Back-substitution for solving the upper triangular system
* Residual calculation to verify solution accuracy
* Step-by-step visualization of Householder vectors
* RESTful API for numerical computations
* Interactive browser-based interface

## Technologies

* C#
* .NET 8
* ASP.NET Core Web API
* HTML
* CSS
* JavaScript

## Project Structure

```text
HouseholderApi/
├── Controllers/
│   └── SolverController.cs
├── HouseholderSolver.cs
├── Program.cs
├── wwwroot/
│   └── index.html
└── HouseholderApi.csproj
```

## Getting Started

### Prerequisites

* .NET 8 SDK

### Installation

Clone the repository:

```bash
git clone https://github.com/SonaGevorgyann/Householder.git
cd HouseholderApi
```

Run the application:

```bash
dotnet run
```

Open your browser and navigate to:

```text
http://localhost:5000
```

The frontend is automatically served by the ASP.NET Core application.

## API Endpoint

### POST `/api/solver`

#### Request

```json
{
  "A": [[2,1,-1],[-3,-1,2],[-2,1,2]],
  "B": [8,-11,-3]
}
```

#### Response

```json
{
  "success": true,
  "x": [2,3,-1],
  "residual": 2.84e-15
}
```

In addition to the solution vector, the API returns:

* QR decomposition results
* Householder vectors for each iteration
* Intermediate computation steps
* Residual error

## Algorithm

The solver follows these steps:

1. Construct Householder reflection vectors.
2. Transform the coefficient matrix into an upper triangular matrix (**R**).
3. Apply the same transformations to the right-hand-side vector.
4. Solve the resulting system using back-substitution.
5. Compute the residual to verify numerical accuracy.

## Learning Outcomes

This project helped me gain practical experience with:

* Numerical linear algebra
* Householder QR decomposition
* Matrix transformations
* ASP.NET Core Web API development
* REST API design
* Frontend and backend integration
* Clean and modular C# development

## Future Improvements

* Support non-square matrices using least-squares approximation
* Matrix import/export
* Save computation history
* Interactive visualization of Householder reflections
* Unit and integration tests

## Author

**Sona Gevorgyan**

GitHub: https://github.com/SonaGevorgyann

