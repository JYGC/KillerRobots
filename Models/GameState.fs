namespace KillerRobots.Models

open System

type GameState(board: Board, robots: Robot list, player: Player) =
  member _.Board = board
  member _.Robots = robots
  member _.Player = player
