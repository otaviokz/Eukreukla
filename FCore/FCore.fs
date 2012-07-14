(*
Autor: Pedro Dusso (http://www.inf.ufrgs.br/~pmdusso) & Otavio Zabaleta
Theory reference: 
    + “Why Function Programming Matter”, John Hughes
Technical reference:
    + "The Definitive Guide to F#", Don Syme, Adam Granicz, Antonio Cisternino
    + “Programming F#”, Chris Smith
    + hubFS: THE place for F#, http://cs.hubfs.net/
    + F# Programming, http://en.wikibooks.org/wiki/F_Sharp_Programming
    + MSDN F# Developer Center, http://msdn.microsoft.com/en-us/fsharp/default.aspx
*)

(*
 Classes = function + data
 Data associated with a class are fields
 Function associated with a class are properties or members

*)

module FCore
open System
open Types
open Moves
open Common
open Evaluation



type Fminmax = 
    val private m_MyColorPiece : Piece
    val private m_MaximumDepth : int
    //val m_StartPosition: Position
    member private this.Eval = new Evaluation(this.m_MyColorPiece, this.m_MaximumDepth)

    //Constructor - receive three parameters
    new(pcColor, maxDepth) = //, startPos //m_StartPosition = startPos
        {
            m_MyColorPiece = pcColor;
            m_MaximumDepth = maxDepth
        }

    // Prop: Minimax = [|
    //                   (List.min:TreeOfPosition list -> TreeOfPosition); 
    //                   (List.max:TreeOfPosition list -> TreeOfPosition)
    //                 |]
    // Desc: this is a array of functions. Every recursion in the evaluateTree function, the index of the
    // current player changes, changing the function which must be used for that level.
    // Args: 0 or 1
    member private this.Minimax = [|(List.min:TreeOfPosition list -> TreeOfPosition); List.max|]

(*
Game tree: nodes are labelled by positions, such that the children of a node are labelled with the position that can
be reached in one move from that node.
A game tree is built by repeated applications of moves. Starting from the root position, moves is used to generate the
labels for the sub-trees of the root. Move is then used again to generate the sub-trees of the sub-trees and so on.
This pattern of recursion can be expressed as a higher-order function (reptree f a = node a (map (reptree f) (f a))
*)
    // Func: computeTree : Position -> TreeOfPosition
    // Desc: Takes a game-position as its argument and returns a tree of all positions that can be reached from it in one move.
    // Args: (position : Position) a position (board configuration) to generate the next moves from.
    member private this.ComputeTree (position : Position) = 
        let rec loop depth node = 
            let nextMoves = Moves.GetAllMoves node this.m_MyColorPiece
            if this.Eval.IsWin node || this.Eval.IsLoss node|| this.Eval.IsDepthEnough depth || nextMoves.IsEmpty then
                LeafP(node,0)
            else
                BranchP(node, nextMoves |> List.map (loop (depth + 1)))
        loop 0 position

    // Func: GetEvalOfNode : TreeOfPosition -> int
    // Desc: retrives just the static evaluated value from a leafP.
    // Args: node : TreeOfPosition = just works if is a leaf.
    member private this.GetEvalOfNode node = 
        match node with
        | LeafP(position, staticValue) -> staticValue
        | _ -> failwith "Not leaf node" 0

    member private this.EvaluateTree (tree, player) =
        let rec loop minOrmax node =
            match node with
            | LeafP(position, 0) -> LeafP(position, this.Eval.EvaluateLeaf(position))
            | LeafP(position, _ )-> failwith "Not valid leaf"
            | BranchP(position, children) -> LeafP(position,(children |> List.map (loop (1 - minOrmax)) |> this.Minimax.[minOrmax] |> this.GetEvalOfNode))
        match tree with
        | BranchP(position, children) -> children |> List.map (loop (1 - player)) |> this.Minimax.[player]
        | _ -> failwith "Not branch"

    // Func: ReturnBestMove: int array -> int array
    // Desc: this function is just wrapper function. It receives the int array from the C# code,
    //       convert it to a Position then call the Functional Minimax. After that, convert the
    //       result, a TreeOfPosition structure, to a int array again.
    // Args: arrayBorad: int array = the board game representation in the imperative design
    member public this.ReturnBestMove arrayBoard = 
        //Convert from Board Array to Position
        let positionBoard = Common.convertTabuleiroToPosition arrayBoard
        //Calculate the next position
        let resultPosition = this.EvaluateTree(this.ComputeTree(positionBoard), 1)
        //Convert from TreeOfPosition to Board Array
        let resultTabuleiro = Common.ConvertTreeOfPositionToTabuleiro(resultPosition)
        resultTabuleiro
