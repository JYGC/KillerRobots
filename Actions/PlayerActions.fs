namespace KillerRobots.Actions

module PlayerActions =
  open System
  open KillerRobots.Models
  open KillerRobots.Tools

  let rec getPlayerTeleportPosition (random: Random) (oldGameState: GameState) =
    let playerTeleportPosition =
      (PositionTools.getRandomX oldGameState.Board random,
      PositionTools.getRandomY oldGameState.Board random)
    let newPositionIsRobot =
      oldGameState.Robots
        |> List.map(fun r -> r.Position)
        |> List.contains(playerTeleportPosition)
    if newPositionIsRobot then
      getPlayerTeleportPosition
        random
        oldGameState
    else
      playerTeleportPosition

  let getProposedNewPlayerPosition (userKeyPress: ConsoleKey) (oldGameState: GameState) =
    let (x, y) = oldGameState.Player.Position
    match userKeyPress with
    | ConsoleKey.UpArrow -> (x, y - 1)
    | ConsoleKey.DownArrow -> (x, y + 1)
    | ConsoleKey.LeftArrow -> (x - 1, y)
    | ConsoleKey.RightArrow -> (x + 1, y)
    | ConsoleKey.Spacebar -> getPlayerTeleportPosition (new Random()) oldGameState
    | _ -> (x, y)

  let private getPlayerStatus (newRobetStates: Robot list) (newPlayerPosition) =
    let hasRobotKilledPlayer =
      newRobetStates
        |> List.map(fun r -> r.Position)
        |> List.contains newPlayerPosition
    match hasRobotKilledPlayer with
    | true -> PlayerStatus.Dead
    | _ -> PlayerStatus.Alive

  let getPlayerPosition (oldGameState: GameState) (newProposedPlayerPosition) =
    let hasPlayerHitWall =
      oldGameState.Board.GetWallCoordinates
        |> List.contains newProposedPlayerPosition
    
    if hasPlayerHitWall then
      oldGameState.Player.Position
    else
      newProposedPlayerPosition

  let getNextPlayerState (userKeyPress: ConsoleKey) (oldGameState: GameState) (newRobetStates: Robot list) =
    let proposedNewPlayerPosition =
      getProposedNewPlayerPosition
        userKeyPress
        oldGameState
    
    let newPlayerPosition =
      getPlayerPosition
        oldGameState
        proposedNewPlayerPosition

    let newPlayerStatus =
      getPlayerStatus
        newRobetStates
        newPlayerPosition

    new Player(
      newPlayerPosition, 
      newPlayerStatus)