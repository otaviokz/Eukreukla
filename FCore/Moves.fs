module Moves
open Types
open Common

// Func: Contains : int * Piece -> Position -> bool
// Desc: Checks if a POSITION contains a int*Piece (ex. BLACK at 1) tuple
// Args: x: int*piece = the boaqrd position and piece color to be be searched.
//       board: Position = the actual board to search in
let rec Contains (x: int*Piece) (Position(board)) = 
    match board with
        | [] -> false
        | hd::tl -> if hd = x then true else Contains x (Position(tl))


// Func: GetPiece : int -> Position -> Piece
// Desc: Returns the color of the piece at x position
// Args: x: int = the board position
//       board: Position = the current game estate
let rec GetPiece (x: int) (Position(board)) = 
    match board with
        | [] -> Piece.EMPTY
        | hd::tl -> if (Contains (x,Piece.BLACK) (Position(board))) then Piece.BLACK
                            elif (Contains (x,Piece.WHITE) (Position(board))) then Piece.WHITE
                            else Piece.EMPTY


// Func: SortBoard : Position -> (int * Piece) list
// Desc: Sort the (int*Piece) tuples of a Position by the int field of taht tuple
// Args: board: Position = the current game state 
let SortBoard (Position(board)) = 
    [
        for i in 1 .. 25 do
            yield (i,GetPiece i (Position(board)))
    ]


// Func: IsEmpty : int -> Position -> bool
// Desc: Checks if a place of the board is empty
// Args: x: int = number of the board place
//       board: Position = the current game state
let rec IsEmpty (x: int) (Position(board)) = 
    match board with
        | [] -> true
        | hd::tl -> let casa,cor = hd in
                        if (casa = x && cor = Piece.EMPTY) then true 
                        elif (casa = x && cor <> Piece.EMPTY) then false
                        else IsEmpty x (Position(tl))


//List containing all the positions and where can we go from any of them
let l1 = (1, [2;6;7])::(2, [1;3;7])::(3,[2;4;7;8;9])::(4,[3;5;9])::(5,[4;9;10])::[]
let l2 = (6,[1;7;11])::(7,[1;2;3;6;8;11;12;13])::(8,[3;7;9;13])::(9,[3;4;5;8;10;13;14;15])::(10,[5;9;15])::[]
let l3= (11,[6;7;12;16;17])::(12,[7;11;13;17])::(13,[7;8;9;12;14;17;18;19])::(14,[9;13;15;19])::(15,[9;10;14;19;20])::[]
let l4= (16,[11;17;21])::(17,[11;12;13;16;18;21;22;23])::(18,[13;17;19;23])::(19,[13;14;15;18;20;23;24;25])::(20,[15;19;25])::[]
let l5= (21,[16;17;22])::(22,[17;21;23])::(23,[17;18;19;22;24])::(24,[19;23;25])::(25,[19;20;24])::[]
let listaVizinhos = l1@l2@l3@l4@l5


// Func: RemoveFromList : int -> Position -> (int * Piece) list
// Desc: Removes an element from a generic list
// Args: x: int = number of the board place
//       lst: Position = the current game state 
let rec RemoveFromList (x: int) (Position(lst)) = 
    match lst with
        | [] -> []
        | hd::tl -> let casa,cor = hd in
                        if x = casa then (RemoveFromList x (Position(tl))) else hd::(RemoveFromList x (Position(tl)))


// Func:  AddToList : 'a -> 'a list -> 'a list
// Desc: Adds an element to a list   
// Args: x: 'a = the element to be added
//       lst: 'a list = the list being modified                     
let AddToList x (lst: 'a list) = x::lst


// Func: MovePiece : Position -> Piece -> int -> int -> (int * Piece) list
// Desc: Returns a game sate after a piece moves
// Args: board: Position = the current game state
//       color: Piece = the color of the piece being moved
//       _f: int = the piece position before moving
//       _t: int = the piece position after moving
let MovePiece (Position(board)) (color: Piece) (_f: int) (_t: int) = 
    let delete_f = RemoveFromList _f (Position(board))
    let delete_t = RemoveFromList _t (Position(delete_f))
    let add_f = AddToList(_f, Piece.EMPTY) delete_t
    let add_t = AddToList(_t, color)  add_f
    SortBoard (Position(add_t))


// Func: GenerateEmptyMovesTAIL : int -> Piece -> Position -> int list -> Position list -> Position list
// Desc: Generate all the moves a piece can execute without killing a enemy
// Args: pos: int = piece position
//       color: Piece = the color of the piece
//       board: Position = the current game state
//       emptyNeighbors: int list = the list of empty board places acessible from "pos"
//       acc: Position list = accumulator used fot tail recursion 
let rec GenerateEmptyMovesTAIL (pos: int) (color: Piece) (Position(board)) (emptyNeighbors: int list) (acc: Position list)= 
    match emptyNeighbors with
        | [] -> acc
        | hd::tl -> GenerateEmptyMovesTAIL pos color (Position(board)) tl ((Position((MovePiece (Position(board)) color pos hd)))::acc)


// Func: GetVizinhos : int -> (int * int list) list -> int list
// Desc: Returns int list representing all the board places that can be accessed from "casa" place
// Args: casa: int = piece position
//       lst: (int * int list) list = the reference list containing all the neighbors for all board places
let rec GetVizinhos (casa:int) (lst: (int * int list) list) =    
    match lst with           
        | [] -> []        
        | hd::tl -> let i,(l: int list) = hd in
                    if (i = casa) then l else GetVizinhos casa tl
                    

// Func: GetEmptyVizinhos : int list -> Position -> int list
// Desc: Check if the board places contained in "lst" and returns only the EMPTY ones
// Args: lst: int list = neigbors list for a board place
//       board: Position = the current game state
let rec GetEmptyVizinhos (lst: int list) (Position(board))= 
    match lst with
        | [] -> []
        | hd::tl -> if IsEmpty hd (Position(board ))
                        then hd::(GetEmptyVizinhos tl (Position(board))) 
                        else (GetEmptyVizinhos tl (Position(board)))


// Func: GetEnemyVizinhos : int list -> Position -> Piece -> int list
// Desc: Check if the board places contained in "lst" and returns only the ones occupied by enemi pieces
// Args: lst: int list = neigbors list for a board place
//       board: Position = the current game state
//       color: Piece = the player's color
let rec GetEnemyVizinhos (lst: int list) (Position(board)) (color: Piece) = 
    match lst with 
        | [] -> []
        | hd::tl -> if ((IsEmpty hd (Position(board))) = false && (GetPiece hd (Position(board))) <> color) 
                        then hd::(GetEnemyVizinhos tl (Position(board)) color)
                        else (GetEnemyVizinhos tl (Position(board)) color)


// Func: GetEmptyMovesTAIL : int * Piece -> Piece -> Position -> Position list
// Desc: Gets all the possible moves that a piece can do without killing any enemy
// Args: place: int*Piece = the piece being processed
//       color: Piece = the player's color 
//       board: Position = the current game state
let GetEmptyMovesTAIL (place: int*Piece) (color: Piece) (Position(board))=
    let casa,cor = place        
    //Get piece neighbors
    if cor = color then
        let neighbors = GetVizinhos casa listaVizinhos
        //Get piece empty neighbors
        let empty = GetEmptyVizinhos neighbors (Position(board))
        //Get piece enemy neighbors
        let enemy = GetEnemyVizinhos neighbors (Position(board)) color
        //Get killless moves
        let emptyMovList = (GenerateEmptyMovesTAIL casa color (Position(board)) empty [])//::(GenerateKillMoves casa color (Position(board))) 
        emptyMovList       
    else []


// Func: LoopGetEmptyMoves : Position -> Piece -> Position -> Position list
// Desc: Applies GetEmptyMovesTail to all the player's pieces
// Args: board: Position = the current game state
//       color: the player's color
//       remainingBoard: Position = contains all the player's pieces still unproccessed
let rec LoopGetEmptyMoves (Position(board)) (color: Piece) (Position(remainingBoard))= 
    match remainingBoard with
        | [] -> []
        | hd::tl -> let casa,cor = hd in
                        let recursionResult = (LoopGetEmptyMoves (Position(board)) color (Position(tl))) in
                            (GetEmptyMovesTAIL (casa,cor) color (Position(board)))@recursionResult
    
                            
// Func: LandingAfterKill : int -> int -> int
// Desc: Find's out where a piece will land after killing a specific neighbor
// Args: _f: int = the piece position before moving
//       _t: int = the piece position after moving
let LandingAfterKill (_f: int) (_t: int) = 
    if _t < _f then  _t - (_f - _t)
    else  _t + (_t - _f)


// Func: Position -> int -> int -> Piece -> Position
// Desc: Returns the resulting game state of a player's piece killing an enemy piece
// Args: board: Position = the current game state
//       me: int = the player piece position
//       enemy:int = the enemy piece position
//       color: Piece = the player's color
let KillEnemy (Position(board)) (me: int) (enemy: int) (color: Piece) = 
    //Get enemy color
    let enemyColor = if color = Piece.BLACK then Piece.WHITE else Piece.BLACK       
    //Delete enemy piece
    let del_enemy = RemoveFromList enemy (Position(board))
    //Replace it for empty piece
    let add_empty = AddToList (enemy,Piece.EMPTY) del_enemy
    //Delete player position before kill
    let del_me = RemoveFromList me (Position(add_empty))
    //Replace it for empty piece
    let add_empty2 = AddToList (me, Piece.EMPTY) del_me
    let landing = (LandingAfterKill me enemy)
    //Delete landing position
    let del_landing = RemoveFromList landing (Position(add_empty2))
    //Add my piece to landing place
    (Position(SortBoard (Position(AddToList (landing, color) del_landing))))
               
                
// Func: CheckCanKill : Position -> int -> int -> bool
// Desc: Checks if a player's piece can kill an enemy's one
// Args: board: Position = the current game state
//       _f: int = the player piece position
//       _t: int = the enemy piece position
let CheckCanKill  (Position(board)) (_f: int) (_t: int) = 
    if _t < _f then
        let prox = _t - (_f - _t) in
            if prox > 0 && prox < 26 then
                if (IsEmpty prox (Position(board)))
                    then true
                    else false
            else false
    else 
        let prox1 = _t + (_t - _f) in
            if prox1 > 0 && prox1 < 26 then
                if (IsEmpty prox1 (Position(board)))
                    then true
                    else false
            else false


// Func: WhoCanYouKill : Position -> int -> int list -> int list
// Desc: Returns a int list containg all the pieces that a player's piece can kill
// Args: board: Position = the current game state
//       casa: int = the player's piece position
//       enemyNeighbors: int list = all the anemy neighbors of the player's piece
let rec WhoCanYouKill (Position(board)) (casa: int) (enemyNeighbors: int list) = 
    match enemyNeighbors with
        | [] -> []
        | hd::tl -> if CheckCanKill (Position(board)) casa hd then hd::(WhoCanYouKill (Position(board)) casa tl)
                    else (WhoCanYouKill (Position(board)) casa tl)


// Func: Piece -> Position -> (int * Piece) list
// Desc: Returns a partial board containing only the player's pieces
// Args: color: Piece = the player's color
//       board: Position = the current game state
let rec GetMyBoard (color: Piece) (Position(board)) =
    match board with
        | [] -> []
        | hd::tl -> let casa,cor = hd in
                        if (cor = color) 
                            then hd::(GetMyBoard color (Position(tl))) 
                            else GetMyBoard color (Position(tl))


// Func: Position -> int -> Piece -> int list -> Position list
// Desc: Retunrs a Position list containing all the resulting game states resultant of a killing move, for a specific player's piece
// Args: board: Position = the current game state
//       casa: int = the piece's position
//       color: Piece = the player's color
//       killableNeighbors: int list = all the neighbors a piece can kill
let rec GetPieceKills (Position(board)) (casa: int) (color: Piece) (killableNeighbors: int list) =
    match killableNeighbors with
        | [] -> []
        | hd::tl -> let landing = LandingAfterKill casa hd
                    let boardAfterKill = (KillEnemy (Position(board)) casa hd color)
                    boardAfterKill::(GetPieceKills (Position(board)) casa color tl)
    

// Func: GetAllKillMoves : Position -> Piece -> Position -> Position list
// Desc: Retunrs a Position list containing all the resulting game states resultant of a killing move, for all the player's pieces
// Args: board: Position = the current game state
//       color: Piece = the player's color
//       myBoard: Position = partial board containing only the player's pieces
let rec GetAllKillMoves (Position(board)) (color: Piece) (Position(myBoard)) =
        match myBoard with
            | [] -> []
            | hd::tl -> let casa,cor = hd
                        let vizinhos = GetVizinhos casa listaVizinhos
                        let enemyVizinhos = GetEnemyVizinhos vizinhos (Position(board)) color
                        let killableEnemies = WhoCanYouKill  (Position(board)) casa enemyVizinhos 
                        let killMoves =  (GetPieceKills (Position(board)) casa color killableEnemies)
                        if (List.length killMoves) > 0 then 
                            killMoves@(GetAllKillMoves (Position(board)) color (Position(tl)))
                        else (GetAllKillMoves (Position(board)) color (Position(tl)))


// Func: GetAllMoves : Position -> Piece -> Position list
// Desc: Returns a list containing all the valid moves for a specific game state
// Args: board: Position = the current game state
//       color: Piece = the player's color
let GetAllMoves (Position(board)) (color:Piece) = 
    let myPieces = GetMyBoard color (Position(board))
    let killMoves = GetAllKillMoves  (Position(board)) color (Position(myPieces))
    if (List.length killMoves > 0) then        
        killMoves
    else
        let emptyMoves = LoopGetEmptyMoves (Position(board)) color (Position(board))
        emptyMoves