namespace KillerRobots.Tools

module ScreenTools =
  open KillerRobots.Models
  open System

  let drawObjects ((x, y): int * int) (color: ConsoleColor) =
    Console.SetCursorPosition(x, y)
    Console.BackgroundColor <- color
    Console.Write(" ")
    Console.BackgroundColor <- ConsoleColor.Black

  let clearConsole (gameState: GameState) =
    gameState.Board.ValidXCoordinates
      |> List.map(fun x ->
        gameState.Board.ValidYCoordinates
          |> List.map(fun y ->
            drawObjects (x, y) ConsoleColor.Black))
      |> ignore
  
  let getRobotColor (robot: Robot) =
    match robot.Status with
    | RobotStatus.Offline -> ConsoleColor.Cyan
    | _ -> ConsoleColor.Magenta

  let getPlayerColor (player: Player) =
    match player.Status with
    | PlayerStatus.Dead -> ConsoleColor.Red
    | _ -> ConsoleColor.DarkYellow

  let draw (gameState: GameState) =
    gameState.Board.GetWallCoordinates
      |> List.map(fun c -> drawObjects c ConsoleColor.Gray)
      |> ignore
    gameState.Robots
      |> List.map(fun r ->
        drawObjects
          r.Position
          (getRobotColor r))
      |> ignore
    drawObjects
      gameState.Player.Position
      (getPlayerColor gameState.Player)
  
  let printLostMessage (gameState: GameState) =
    let lostMessage = "GAME OVER. YOU DIED."
    let messageX = (gameState.Board.RightWallXCoordinate / 2) - (lostMessage.Length / 2)
    let messageY = gameState.Board.BottomWallYCoordinate / 2
    Console.SetCursorPosition(messageX, messageY)
    Console.ForegroundColor <- ConsoleColor.Red
    Console.Write(lostMessage)
    Console.ResetColor()
    0
  
  let printWonMessage (gameState: GameState) =
    let lostMessage = "ALL ROBOTS OFFLINE. YOU SURVIVED."
    let messageX = (gameState.Board.RightWallXCoordinate / 2) - (lostMessage.Length / 2)
    let messageY = gameState.Board.BottomWallYCoordinate / 2
    Console.SetCursorPosition(messageX, messageY)
    Console.ForegroundColor <- ConsoleColor.Green
    Console.Write(lostMessage)
    Console.ResetColor()
    0