namespace KillerRobots.Models

type EndgameResult =
  | Lose = 0
  | Win = 1

type GameState(board: Board, robots: Robot list, player: Player) =
  member _.Board = board
  member _.Robots = robots
  member _.Player = player
