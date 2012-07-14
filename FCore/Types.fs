namespace Types
open System.Collections.Generic

(*
Let game position be represented by objects of the player "position". This type will vary from game to game,
and we assume nothing about it. There must be some way of knowing what moves can be made from a position: assume
that there is a function moves : position -> position list
*)

type Piece = | BLACK | WHITE | EMPTY
type Position = Position of  list<int * Piece>

// Tree
// label:the position it represents (lets say 'p')
// list of sub nodes: the childrens of that position (computeMoves (p))
(*
 Example:
 Branch(p, [a;b])
    Branch(a, [c;d])
        Leaf(c)
        Leaf(d)
    Leaf(b)
 
            p1
           / \
          a2  b3
         / \
       c4  d5
*)
// LeafN: Leaf of Number   | BranchN: Branch of Number
// LeafP: Leaf of Position | BranchP: Branch of Position
//type TreeOfNumber = LeafN of int | BranchN of Position * TreeOfNumber list
[<CustomEquality;CustomComparison>]
type TreeOfPosition =
    | LeafP   of Position * int
    | BranchP of Position * TreeOfPosition list
        
    //Func: compare
    //Retu: -1: first parameter is less than the second
    //       0: first parameter is equal to the second
    //       1: first parameter is greater than the second
    static member mycompare (x, y) = 
        match x, y with
        // Compare values stored as part of your type
        | LeafP(_, n1), LeafP(_, n2) -> compare n1 n2
        | _ -> 0 // or 1 depending on which is list...

    override x.Equals(yobj) = 
        match yobj with
        | :? TreeOfPosition as y -> (x = y)
        | _ -> false

    override x.GetHashCode() = hash (x)
    interface System.IComparable with
       member x.CompareTo yobj = 
          match yobj with 
          | :? TreeOfPosition as y -> TreeOfPosition.mycompare(x, y)
          | _ -> invalidArg "yobj" "cannot compare value of different types" 
