module Lab2
open System
open Lists
open BinaryTree
open ListUtils
open TasksList
open NormalizedFactorPairs
open RandomWordOrder
open SortByAverageDeviation

[<EntryPoint>]
let main args =
    let sum a b = a + b
    let countDigit acc n = 
        acc + 1
    
    let maxDigit a b =
     match (a, b) with
     | (a, b) when a >= b -> a
     | _ -> b

    let isEven n = (n % 2 = 0)

    let smallerThan5 n = n < 5
    // списки 

    let list1 = readList 5
    Console.WriteLine("Список list1:")
    printList list1

    let resList = reduce list1 sum isEven 0
    Console.WriteLine("сумма четных элементов списка:" + resList.ToString())
    let minList = minElement list1
    Console.WriteLine("мин элемент:" + minList.ToString())


    Console.Write("Сумма чётных: ")
    Console.WriteLine(sum_even list1)
    Console.Write("Количество нечётных: ")
    Console.WriteLine(count_odd list1)
    Console.Write("Самый часто встречающийся элемент: ")
    Console.WriteLine(find_most_frequent list1)
    let tree = 
        empty
        |> insert "Berlin"
        |> insert "Amsterdam"
        |> insert "Copenhagen"
        |> insert "Dublin"
        |> insert "Edinburgh"

    Console.WriteLine("Вывод двоичного дерева:")
    print_tree tree

    Console.WriteLine("Проверка элемента по индексу 3 на глобальный максимум:")
    Console.Write("Чёрч: ")
    Console.WriteLine(is_global_max_church list1 3)
    Console.Write("List: ")
    Console.WriteLine(is_global_max_list list1 3)
    Console.WriteLine("Перемещение всех элементов перед минимумом в конец:")
    Console.Write("Чёрч: ")
    print_list (move_before_min_church list1)
    Console.Write("List: ")
    print_list (move_before_min_list list1)
    Console.WriteLine("Два наименьших элемента списка:")
    Console.Write("Чёрч: ")
    Console.WriteLine(find_two_min_church list1)
    Console.Write("List: ")
    Console.WriteLine(find_two_min_list list1)
    Console.WriteLine("Проверка на чередование положительных/отрицательных:")
    Console.Write("Чёрч: ")
    Console.WriteLine(is_alternating_church list1)
    Console.Write("List: ")
    Console.WriteLine(is_alternating_list list1)
    Console.WriteLine("Количество минимальных элементов:")
    Console.Write("Чёрч: ")
    Console.WriteLine(count_min_elements_church list1)
    Console.Write("List: ")
    Console.WriteLine(count_min_elements_list list1)
    
    Console.Write("Введите число: ")
    let N = Console.ReadLine() |> int
    Console.WriteLine("Построенные кортежи (a,b): ")
    print_list (getNormalizedPairs N)

    Console.WriteLine("Перемешивание строки 'Moscow Paris NewYork London Tokyo Berlin':")
    Console.WriteLine(mixWords "Moscow Paris NewYork London Tokyo Berlin")

    Console.WriteLine("Сортировка по квадратичному отклонению среднего веса ASCII-кода символа строки от следнего веса первой строки:")
    Console.WriteLine("Исходный массив: 'hello', 'world', 'functional', 'programming', 'fsharp', 'example']")
    Console.Write("Отсортированный массив: ")
    print_list (sortByAverageDeviation ["hello"; "world"; "functional"; "programming"; "fsharp"; "example"])

    0
