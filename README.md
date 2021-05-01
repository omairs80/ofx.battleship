# Ofx Battleship Api

The project is structured using the Onion architecture / Ports & Adapters pattern with dependencies always pointing inwards.

## Project structure
### Ofx.Battleship.Api
Contains the entry point into the application.

### Ofx.Battleship.Controller
Adapter project that contains the api controllers. Dependecies are also setup here.

### Ofx.Battleship.Application
Contains the commands and command handlers.

### Ofx.Battleship.Domain
Contains the business model of the battleship game.

Swagger documentation (when running locally): http://localhost:5000/swagger/index.html

## Apis
### Create board for a player
Create a game board for playerId 1 
```
POST https://ofxbattleship.azurewebsites.net/api/battleship/1/board
```

### Add battle ship
Add a battle ship to playerId 1
```
POST https://ofxbattleship.azurewebsites.net/api/battleship/1/battleship`
{
  "size": 3,            // the number of squares the ship will occupy in a 10x10 board
  "startingRow": 1,     // starting row position
  "startingColumn": 1,  // starting column position
  "isHorizontal": true  // decides whether the ship will occupy the board horizontally or vertically
}
```
Note: Battle ships cannot overlap each other (even over a sunk ship)

### Attack battle ship
Attack a battle ship of playerId 1
```
DELETE https://ofxbattleship.azurewebsites.net/api/battleship/1/battleship
{
  "row": 1,     // Attacking at row 1 of the player's board
  "column": 3   // Attacking at column 3 of the player's board
}
```
When all the coordinates/squares occupied a battle ship are hit, the ship will be "sunk". However, you cannot add another ship in a space occupied by a sunk ship.

### Clear / delete a player's board
Delete the board of playerId 1
```
DELETE https://ofxbattleship.azurewebsites.net/api/battleship/1/battleship
```