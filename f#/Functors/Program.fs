open System

type Maybe<'a> =
    | Just of 'a
    | Nothing

// Функтор
module Functor =
    let map f maybe =
        match maybe with
        | Just x -> Just (f x)
        | Nothing -> Nothing

    module Laws =
        let identity x =
            map id x = x

        let composition f g x =
            map (f >> g) x = (map f >> map g) x

// Аппликативный функтор
module Applicative =
    let pure' x = Just x
    
    let apply maybeF maybeX =
        match maybeF, maybeX with
        | Just f, Just x -> Just (f x)
        | _ -> Nothing

    module Laws =
        let identity v =
            apply (pure' id) v = v

        let composition u v w =
            apply (apply (apply (pure' (>>)) u) v) w = apply u (apply v w)

        let homomorphism f x =
            apply (pure' f) (pure' x) = pure' (f x)

        let interchange u y =
            apply u (pure' y) = apply (pure' (fun f -> f y)) u

// Монада
module Monad =
    let return' x = Just x
    
    let bind maybe f =
        match maybe with
        | Just x -> f x
        | Nothing -> Nothing

    module Laws =
        let leftIdentity x f =
            bind (return' x) f = f x

        let rightIdentity m =
            bind m return' = m

        let associativity m f g =
            bind (bind m f) g = bind m (fun x -> bind (f x) g)

let testLaws() =
    let testCases = [Just 5; Nothing]
    
    Console.WriteLine("Проверка законов функтора:")
    testCases |> List.iter (fun x ->
        Console.WriteLine($"Тождественный закон для {x}: {Functor.Laws.identity x}")
        Console.WriteLine($"Закон композиции для {x}: {Functor.Laws.composition ((+) 2) ((*) 3) x}")
    )

    Console.WriteLine("\nПроверка законов аппликативного функтора:")
    testCases |> List.iter (fun v ->
        Console.WriteLine($"Тождественный закон для {v}: {Applicative.Laws.identity v}")
    )
    Console.WriteLine($"Гомоморфизм: {Applicative.Laws.homomorphism ((+) 2) 5}")
    Console.WriteLine($"Закон перестановки: {Applicative.Laws.interchange (Just ((+) 2)) 5}")

    Console.WriteLine("\nПроверка законов монады:")
    let f x = Just (x + 2)
    let g x = Just (x * 3)
    testCases |> List.iter (fun m ->
        Console.WriteLine($"Левый нейтральный элемент (левая единица): {Monad.Laws.leftIdentity 5 f}")
        Console.WriteLine($"Правый нейтральный элемент (правая единица) для {m}: {Monad.Laws.rightIdentity m}")
        Console.WriteLine($"Ассоциативность для {m}: {Monad.Laws.associativity m f g}")
    )

testLaws()