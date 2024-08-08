namespace KillerRobots.Actions

module RobotActions =
  open System
  open KillerRobots.Models

  let moveNumberCloserToTargetCoordinate (number: int) (targetCoordinate: int) =
    if number > targetCoordinate then
      number - 1
    elif number < targetCoordinate then
      number + 1
    else
      number
  
  let getNextRobotPosition (userKeyPress: ConsoleKey) (oldGameState: GameState) (robot: Robot) =
    let (proposedPlayerX, proposedPlayerY) = 
      PlayerActions.getProposedNewPlayerPosition
        userKeyPress
        oldGameState
    let (oldRobotX, oldRobotY) = robot.Position

    if robot.Status = RobotStatus.Online then
      let newRobotX =
        moveNumberCloserToTargetCoordinate
          oldRobotX
          proposedPlayerX

      let newRobotY =
        moveNumberCloserToTargetCoordinate
          oldRobotY
          proposedPlayerY
      
      (newRobotX, newRobotY)
    else
      robot.Position

  let getNewRobotStatus (newRobotPosition: (int * int)) (destroyedRobotPositions: (int * int) list) =
    let isRobotDestroyed =
      destroyedRobotPositions
        |> List.contains(newRobotPosition)
    if isRobotDestroyed then
      RobotStatus.Destroyed
    else
      RobotStatus.Online

  let getNextRobotState (newRobotPosition: (int * int)) (destroyedRobotPositions: (int * int) list) =
    let newRobotStatus = getNewRobotStatus newRobotPosition destroyedRobotPositions
    new Robot(newRobotPosition, newRobotStatus)
  
  let getNextRobotStates (userKeyPress: ConsoleKey) (oldGameState: GameState) =
    let nextRobotPositions =
      oldGameState.Robots
        |> List.map(fun r ->
          getNextRobotPosition
            userKeyPress
            oldGameState
            r)
    let destroyedRobotPositions =
      nextRobotPositions
        |> Seq.countBy(fun p -> p)
        |> Seq.filter(fun pc ->
          let (_, count) = pc
          count > 1)
        |> Seq.map(fun pc ->
          let (position, _) = pc
          position)
        |> Seq.toList
    nextRobotPositions
      |> List.map(fun p ->
        getNextRobotState p destroyedRobotPositions)