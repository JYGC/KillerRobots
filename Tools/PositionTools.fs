namespace KillerRobots.Tools

module PositionTools =
  open KillerRobots.Models
  open System
  let getRandomX (board: Board) (random: Random) =
    random.Next(board.LeftWallXCoordinate + 1, board.RightWallXCoordinate - 1)

  let getRandomY (board: Board) (random: Random) =
    random.Next(board.TopWallYCoordinate + 1, board.BottomWallYCoordinate - 1)