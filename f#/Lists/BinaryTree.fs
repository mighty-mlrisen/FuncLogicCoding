module BinaryTree

open System

type BinaryTree =
    | Leaf
    | Node of string * BinaryTree * BinaryTree

let empty = Leaf

let rec insert value tree =
    match tree with
    | Leaf -> Node(value, Leaf, Leaf)
    | Node(v, left, right) ->
        if value < v then
            Node(v, insert value left, right)
        elif value > v then
            Node(v, left, insert value right)
        else
            tree

let rec contains value tree =
    match tree with
    | Leaf -> false
    | Node(v, left, right) ->
        if value = v then true
        elif value < v then contains value left
        else contains value right

let rec traverse tree =
    match tree with
    | Leaf -> []
    | Node(v, left, right) -> traverse left @ [v] @ traverse right

let print_tree tree =
    let rec print tree_arr =
        match tree_arr with
        | [] -> Console.WriteLine("")
        | head::tail ->
            Console.Write(head + " ")
            print tail
    print (traverse tree)