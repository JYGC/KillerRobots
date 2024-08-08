namespace KillerRobots.Models

type PlayerStatus =
  | Dead = 0
  | Alive = 1

type Player(position: int * int, status: PlayerStatus) =
  member _.Position: int * int = position
  member _.Status = status
