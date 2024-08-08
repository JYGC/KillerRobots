namespace KillerRobots.Models

type RobotStatus =
  | Destroyed = 0
  | Online = 1

type Robot(position: int * int, status: RobotStatus) =
  member _.Position: int * int = position
  member _.Status = status

