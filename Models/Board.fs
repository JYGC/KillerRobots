namespace KillerRobots.Models

open System

type Board() =
  let __windowWidth = Console.WindowWidth
  let __windowHeight = Console.WindowHeight
  
  member _.LeftWallXCoordinate = 0
  member _.RightWallXCoordinate = __windowWidth - 1
  member _.TopWallYCoordinate = 0
  member _.BottomWallYCoordinate = __windowHeight - 1
  member this.ValidXCoordinates = [this.LeftWallXCoordinate..this.RightWallXCoordinate]
  member this.ValidYCoordinates = [this.TopWallYCoordinate..this.BottomWallYCoordinate]

  member private this.GetTopWallCoordinates = 
    this.ValidXCoordinates
      |> List.map(fun x -> (x, this.TopWallYCoordinate))

  member private this.GetBottomWallCoordinates = 
    this.ValidXCoordinates
      |> List.map(fun x -> (x, this.BottomWallYCoordinate))

  member private this.GetLeftWallCoordinates = 
    this.ValidYCoordinates
      |> List.map(fun y -> (this.LeftWallXCoordinate, y))

  member private this.GetRightWallCoordinates = 
    this.ValidYCoordinates
      |> List.map(fun y -> (this.RightWallXCoordinate, y))

  member this.GetWallCoordinates =
    List.empty
      |> List.append this.GetTopWallCoordinates
      |> List.append this.GetBottomWallCoordinates
      |> List.append this.GetLeftWallCoordinates
      |> List.append this.GetRightWallCoordinates