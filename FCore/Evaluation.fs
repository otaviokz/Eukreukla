namespace Evaluation
open Types
open Common

type Evaluation =
    val m_MyColorPiece : Piece
    val m_MaximumDepth : int

    //Construtor
    new(pcColor, maxDeepth) =
        {
            m_MyColorPiece = pcColor;
            m_MaximumDepth = maxDeepth;
        }
    
    // Func: not : Piece -> Piece
    // Desc: Takes a color piece and return the opposite color piece
    // Args: (piece : Piece) the color piece to negate
    member private this.Not (piece : Piece) = 
            match piece with
            | BLACK -> WHITE
            | WHITE -> BLACK
            | EMPTY -> EMPTY

    member public this.IsDepthEnough (depth) = 
        if (depth = this.m_MaximumDepth) then true else false

    // Func: isWin : Position -> bool
    // Desc: Takes a game-position and the piece color of the opponent. Verify if there is, at least, one
    // value of that piece (the computers opponent color) in the board. If there is, the game is not win yet.
    // Args: (position:Position) a position (board configuration) to find for pieces
    member public this.IsWin (position : Position) = 
        if(Common.containsValue (this.Not this.m_MyColorPiece) position) then false else true
    // Func: isLoss : Position -> bool
    // Desc: Takes a game-position and the piece color of the opponent. Verify if there is, at least, one
    // value of that opposite piece (the computers color) in the board. If there is, the game is not lost yet.
    // Args: (position:Position) a position (board configuration) to find for pieces
    member public this.IsLoss (position : Position) = 
        if(Common.containsValue this.m_MyColorPiece position) then false else true

(*
The static evaluation must be used at the limit of the look-ahead, and may be used to guide the algorithm earlier.
The rsult of the static evaluation is a measure of the promise of a position from the computer's point of view.
The larger the result, the better the position for the computer. the smaller the result, the worse the position.
*)

    // Func: evaluateLeaf : Position -> int
    // Desc: calculates how good a position is, in numerical terms. The higher the value, the better the position
    // Args: (position : Position) a position (board configuration) to be evaluated
    member public this.EvaluateLeaf (position : Position) =
        let computerPiecesCount = Common.countPieces this.m_MyColorPiece position
        let enemyPiecesCount = Common.countPieces (this.Not this.m_MyColorPiece) position
        
        if this.IsWin position then 12
        elif this.IsLoss position then -12
        else computerPiecesCount - enemyPiecesCount

