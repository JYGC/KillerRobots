namespace KillerRobots.Tools

module ScreenTools =
  open KillerRobots.Models
  open System
  
  let getRobotColor (robot: Robot) =
    match robot.Status with
    | RobotStatus.Destroyed -> ConsoleColor.Cyan
    | _ -> ConsoleColor.Magenta

  let getPlayerColor (player: Player) =
    match player.Status with
    | PlayerStatus.Dead -> ConsoleColor.Red
    | _ -> ConsoleColor.DarkYellow