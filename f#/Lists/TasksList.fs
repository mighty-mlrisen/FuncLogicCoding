module TasksList
open System

let rec print_list list = 
    match list with
    | [] -> Console.Write("\n")
    | head::tail ->
        Console.Write((head |> string) + " ")
        print_list tail

// Задание 11
let is_global_max_church arr index =
    let rec find_max current_max remaining_list =
        match remaining_list with
        | head :: tail ->
            let new_max = if head > current_max then head else current_max
            find_max new_max tail
        | [] -> current_max
    
    let rec get_element idx lst =
        match lst with
        | head :: tail ->
            if idx = 0 then head
            else get_element (idx - 1) tail
        | [] -> failwith "Out of bounds"
    
    match arr with
    | [] -> false
    | _ ->
        let element = get_element index arr
        let globalMax = find_max Int32.MinValue arr
        element = globalMax

let is_global_max_list arr index =
    if index < 0 || index >= List.length arr then
        false
    else
        let element = List.item index arr
        let globalMax = List.max arr
        element = globalMax

// Задание 12
let rec reverse_list lst =
    let rec rev acc = function
        | [] -> acc
        | head :: tail -> rev (head :: acc) tail
    rev [] lst

let rec find_min_index current_min current_index min_index remaining_list index =
    match remaining_list with
    | [] -> min_index
    | head :: tail ->
        if head < current_min then
            find_min_index head (index + 1) index tail (index + 1)
        else
            find_min_index current_min (current_index + 1) min_index tail (index + 1)
    
let rec take_before index acc main_list current =
    match main_list with
    | [] -> reverse_list acc
    | head :: tail ->
        if current < index then
            take_before index (head :: acc) tail (current + 1)
        else
            reverse_list acc

let rec take_after index acc main_list current =
    match main_list with
    | [] -> reverse_list acc
    | head :: tail ->
        if current >= index then
            take_after index (head :: acc) tail (current + 1)
        else
            take_after index acc tail (current + 1)

let move_before_min_church arr =
    match arr with
    | [] -> []
    | _ ->
        let min_index = find_min_index Int32.MaxValue 0 0 arr 0
        let arr_before = take_before min_index [] arr 0
        let arr_after = take_after min_index [] arr 0
        arr_after @ arr_before

let move_before_min_list arr =
    match arr with
    | [] -> []
    | _ ->
        let min_index = List.findIndex (fun x -> x = List.min arr) arr
        let before_min, from_min = List.splitAt min_index arr
        from_min @ before_min

// Задание 13
let find_two_min_church arr =
    let rec find_min_and_rest minVal rest arr =
        match arr with
        | [] -> minVal, rest
        | head :: tail ->
            if head < minVal then
                find_min_and_rest head (minVal :: rest) tail
            else
                find_min_and_rest minVal (head :: rest) tail
                
    let min1, rest1 = find_min_and_rest Int32.MaxValue [] arr
    let min2, _ = find_min_and_rest Int32.MaxValue [] rest1
    min1, min2

let find_two_min_list arr =
    match arr |> List.sort with
    | [] | [_] -> failwith "Not enough elements"
    | x :: y :: _ -> x, y

// Задание 14
let rec is_alternating_church arr = 
    match arr with
    | [] | [_] -> true
    | x1 :: x2 :: tail ->
        let signMatches = 
            x1 * x2 <= 0
        if not signMatches then false
        else is_alternating_church (x2 :: tail)

let is_alternating_list arr =
    arr
    |> List.pairwise
    |> List.forall (fun (x1, x2) -> x1 * x2 < 0)

// Задание 15
let count_min_elements_church arr =
    let rec find_min_and_count curr_min count arr =
        match arr with
        | [] -> (curr_min, count)
        | head :: tail ->
            if head < curr_min then
                find_min_and_count head 1 tail
            elif head = curr_min then
                find_min_and_count curr_min (count + 1) tail
            else
                find_min_and_count curr_min count tail
    
    match arr with
    | [] -> 0
    | head :: tail ->
        let (_, count) = find_min_and_count head 1 tail
        count

let count_min_elements_list arr =
    match arr with
    | [] -> 0
    | _ ->
        let min_val = List.min arr
        arr |> List.filter (fun x -> x = min_val) |> List.length

// Задание 16
let filter_between_avg_and_max_church arr =
    let rec sum_list acc arr =
        match arr with
        | [] -> acc
        | head :: tail -> sum_list (acc + head) tail
    
    let rec count_list acc arr =
        match arr with
        | [] -> acc
        | _ :: tail -> count_list (acc + 1) tail
    
    let rec find_max curr_max arr =
        match arr with
        | [] -> curr_max
        | head :: tail ->
            let new_max = if head > curr_max then head else curr_max
            find_max new_max tail
    
    let rec filter_elements avg max acc arr = 
        match arr with
        | [] -> reverse_list acc
        | head :: tail ->
            if float head > avg && head < max then
                filter_elements avg max (head :: acc) tail
            else
                filter_elements avg max acc tail
    
    match arr with
    | [] -> []
    | _ ->
        let total_sum = sum_list 0 arr
        let total_count = count_list 0 arr
        let avg = float total_sum / float total_count
        let max = find_max Int32.MinValue arr
        filter_elements avg max [] arr

let filter_between_avg_and_max_list arr =
    match arr with
    | [] -> []
    | _ ->
        let avg = List.averageBy float arr
        let max = List.max arr
        arr 
        |> List.filter (fun x -> float x > avg && x < max)
