namespace KillerRobots.Screen

open System
open KillerRobots.Models
open KillerRobots.Tools

type Screen(gameState: GameState) =
  let drawObjects ((x, y): int * int) (color: ConsoleColor) =
    Console.SetCursorPosition( x, y)
    Console.BackgroundColor <- color
    Console.Write(" ")
    Console.BackgroundColor <- ConsoleColor.Black

  member _.ClearConsole () =
    gameState.Board.ValidXCoordinates
      |> List.map(fun x ->
        gameState.Board.ValidYCoordinates
          |> List.map(fun y ->
            drawObjects (x, y) ConsoleColor.Black))
      |> ignore

  member _.Draw () =
    gameState.Board.GetWallCoordinates
      |> List.map(fun c -> drawObjects c ConsoleColor.Gray)
      |> ignore
    gameState.Robots
      |> List.map(fun r ->
        drawObjects
          r.Position
          (ScreenTools.getRobotColor r))
      |> ignore
    drawObjects
      gameState.Player.Position
      (ScreenTools.getPlayerColor gameState.Player)