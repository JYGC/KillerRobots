namespace KillerRobots.Actions

module GameStateActions =
  open KillerRobots.Models
  open KillerRobots.Tools
  open System

  let rec gotoNextGameState (gameState: GameState) =
    ScreenTools.clearConsole(gameState)
    ScreenTools.draw(gameState)

    let numberOfOnlineRobots =
      gameState.Robots
        |> List.filter(fun r -> r.Status = RobotStatus.Online)
        |> List.length

    if gameState.Player.Status = PlayerStatus.Dead then
      ScreenTools.printLostMessage gameState
    elif numberOfOnlineRobots = 0 then
      ScreenTools.printWonMessage gameState
    else
      computeNextGameState(gameState)

  and computeNextGameState(gameState: GameState) =
    let keyPress = Console.ReadKey()

    let newRobots =
      RobotActions.getNextRobotStates
        keyPress.Key
        gameState

    let newPlayer =
      PlayerActions.getNextPlayerState 
        keyPress.Key 
        gameState
        newRobots

    gotoNextGameState (new GameState(
      board = gameState.Board,
      robots = newRobots,
      player = newPlayer))

  let private makeRobots (board: Board) (random: Random) =    
    List.init 10 (fun _ -> new Robot(
      (PositionTools.getRandomX board random,
      PositionTools.getRandomY board random),
      RobotStatus.Online))

  let rec makePlayer (board: Board) (robots: Robot list) (random: Random) =
    let newPlayerPosition =
      (PositionTools.getRandomX board random,
      PositionTools.getRandomY board random)
    let newPlayerPositionHasRobot =
      robots
      |> List.map(fun r -> r.Position)
      |> List.contains newPlayerPosition
    if newPlayerPositionHasRobot then
      makePlayer board robots random
    else
      new Player(newPlayerPosition, PlayerStatus.Alive)
  
  let startGame() =
    Console.CursorVisible <- false
    
    let board = new Board()
    let random = Random()
    let robots = makeRobots board random
    let player = makePlayer board robots random
    
    gotoNextGameState (new GameState(board, robots, player))
      |> ignore
    Console.CursorVisible <- true
