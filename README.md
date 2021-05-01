# Ofx Battleship Api

The project is structured using the Onion architecture / Ports & Adapters pattern with dependencies always pointing inwards.

## Ofx.Battleship.Api
Contains the entry point into the application.

## Ofx.Battleship.Controller
Adapter project that contains the api controllers. Dependecies are also setup here.

## Ofx.Battleship.Application
Contains the commands and command handlers.

## Ofx.Battleship.Domain
Contains the business model of the battleship game.

Swagger documentation (when running locally): http://localhost:5000/swagger/index.html