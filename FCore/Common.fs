module Common
open Types

let contains (position: int*Piece) (Position(board)) = 
    let rec loop p board =
        match board with
            | [] -> false
            | hd::tl -> if hd = position then true else loop p tl
    loop position board

let RemoveFromList (x: int) (Position(lst)) = 
    let rec loop p board =
        match board with
            | [] -> []
            | hd::tl -> 
            let casa,cor = hd in
                if x = casa 
                    then (loop x tl) 
                    else hd::(loop x tl)
    loop x lst



// Func: containsValue : Piece -> Position -> bool
// Desc: verify if the board contains, at least, one piece of the given color.
// Args: piece: Piece = the piece color which must be searched.
//       board: Position = the actual board to search in
let containsValue (piece: Piece) (Position(board)) = 
    let rec loop p board =
        match board with
            | [] -> false
            | hd::tl -> if snd hd = p then true else loop p tl
    loop piece board

// Func: countPieces : Piece -> Position -> int
// Desc: count the total number of pieces of the given color
// Args: piece: Piece = the piece color which must be searched.
//       board: Position = the actual board to search in
let countPieces (piece : Piece) (Position(board)) = 
    let rec loop pieceColor board acc = 
        match board with
            | [] -> acc
            | hd::tl -> 
            if (snd hd = pieceColor) 
                then loop pieceColor tl (acc + 1) 
                else loop pieceColor tl acc
    loop piece board 0

(* This are the constants used in the imperative board to control the pieces.
  WHITE = 1; BLACK = 0; EMPTY = -1; NEXT_MOVE = 2; EATED_PIECE = 3; *)

// Func: parseToPiece : int -> Piece
// Desc: convert a int to it Piece equivalent
// Args: piece:int = the int value from the imperative board.
let parseToPiece piece =
    match piece with
    | 1  -> Piece.WHITE
    | 0  -> Piece.BLACK
    | _ -> Piece.EMPTY
// Func: parseOfPiece : Piece -> int
// Desc: convert a piece to a int equivalent
// Args: piece:Piece = the piece from the funcional board
let parseOfPiece piece =
    match piece with
    | Piece.WHITE -> 1
    | Piece.BLACK -> 0
    | _ -> -1

// Func: convertTabuleiroToPosition : int array -> Position
// Desc: converts a int array game board (the data structure used in the imperative version) 
//       to a Position board.
// Args: arrayBoard:int array = the array to be converted to a Position board.
let convertTabuleiroToPosition (arrayBoard:int array) =
    let list = Position[for n in 1 .. 25 do yield (n, parseToPiece(arrayBoard.[n]))]
    list

// Func: GetListFromNode : TreeOfPosition -> Position
// Desc: retrieves the list of positions inside the tree, which *should* be just a leaf here
// Args: node:TreeOfPosition = the tree resulting from minimax
let GetListFromNode node = 
    match node with
    | LeafP(position, staticValue) -> position
    | _ -> failwith "Not leaf node" 0

// Func: GetArrayFromPosition : Position -> int array
// Desc: maps the board representation from list to array
// Args: positionBoard: Position = a Position (list) to convert to
let GetArrayFromPosition (Position(positionBoard)) = 
    let arrayBoard : int array = Array.zeroCreate 26
    let rec loop  (positionB:(int * Piece) list) n =
        match positionB with
        | [] -> ()
        | hd::tl -> arrayBoard.[n] <-  hd |> snd |> parseOfPiece;
                    loop tl (n + 1)
    loop positionBoard 1
    arrayBoard

// Func: parseOfPiece : Piece -> int
// Desc: convert a piece to a int equivalent
// Args: piece:Piece = the piece from the funcional board
let ConvertTreeOfPositionToTabuleiro tree =
    let result = GetArrayFromPosition(GetListFromNode(tree))
    result

// Func: SortPositionList : Position list -> (int * Piece) list list
// Desc: 
// Args: 
let SortPositionList (positionList : Position list) =
    List.map (fun (Position(position)) -> List.sort(position)) positionList