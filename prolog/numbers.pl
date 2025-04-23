
max(X, Y, X) :- X>Y, !.
max(_, Y, Y).

max(X, Y, Z, U) :-
    max(X, Y, M), max(M, Z, U).

max(X, Y, Z, X) :- X>Y, X>Z, !.
max(_, Y, Z, Y) :- Y>Z, !.
max(_, _, Z, Z).

fact_up(0, 1).
fact_up(N, X) :-
    N > 0,
    N1 is N - 1,
    fact_up(N1, X1),
    X is N * X1. 

fact_down(N, X) :- 
    fact_tail(N, 1, X).

fact_tail(0, Acc, Acc).
fact_tail(N, Acc, X) :-
    N > 0,
    NewAcc is N * Acc,
    N1 is N - 1,
    fact_tail(N1, NewAcc, X).


% Сумма цифр
digit_sum_up(0, 0) :- !.
digit_sum_up(N, S) :-
    Digit is N mod 10,
    NewN is N div 10,
    digit_sum_up(NewN, NewSum),
    S is NewSum + Digit.

digit_sum_down(N, S) :-
    digit_sum_tail(N, 0, S).

digit_sum_tail(0, Acc, Acc) :- !.

digit_sum_tail(N, Acc, S) :-
    NewN is N div 10,
    NewAcc is Acc + N mod 10,
    digit_sum_tail(NewN, NewAcc, S).

% Проверка на свободность от квадратов
square_free(1).
square_free(N) :-
    N > 1,
    \+ has_square_factor(N, 2).

has_square_factor(N, K) :-
    K * K =< N,
    (
        0 is N mod (K * K); 
        NextK is K + 1,
        has_square_factor(N, NextK)
    ).


% Чтение списка
r_list(A, N):-r_list(A, N, 0, []).
r_list(A, N, N, A):-!.
r_list(A, N, K, B):-read(X), append(B,[X],B1), K1 is K+1, r_list(A, N, K1, B1).

% Запись в список
w_list([]):-!.
w_list([H|T]) :- write(H), nl, w_list(T).

% Добавление в список
my_ap([], Y, Y).
my_ap([H|T], Y, [H|T1]) :- my_ap(T, Y, T1).

% Проверка на наличие в списке
in_list([], _) :- false.
in_list([X|T], X).
in_list([_|T], X) :- in_list(T, X).

% Получение элемента по индексу
get_at(0, [Head|_], Head).

get_at(Index, [_|Tail], Element) :-
    Index > 0,
    NewIndex is Index - 1,
    get_at(NewIndex, Tail, Element).



% Сумма элементов списка
sum_list_up([], 0).

sum_list_up([H|T], Sum) :-
    sum_list_up(T, TS),
    Sum is H + TS.

sum_list_down(List, Sum) :-
    sum_list_down(List, 0, Sum).

sum_list_down([], Acc, Acc).

sum_list_down([H|T], Acc, Sum) :-
    NewAcc is Acc + H,
    sum_list_down(T, NewAcc, Sum).

% Удаление элементов с не равной суммой цифр
remove_if_sum_equal([], _, []).

remove_if_sum_equal([X | Tail], TargetSum, Result) :-
    digit_sum_down(X, Sum),
    (Sum =:= TargetSum -> remove_if_sum_equal(Tail, TargetSum, Result); Result = [X | NewResult], remove_if_sum_equal(Tail, TargetSum, NewResult)).



%Произведение цифр numProd(+N,?S), вниз
numProdDown(X,Answer):-
    numProdDownInner(X,1,Answer).
numProdDownInner(0,Acc,Acc):-!.
numProdDownInner(X,Acc,Answer):-
    X1 is X div 10,
    Acc1 is Acc * (X mod 10),
    numProdDownInner(X1,Acc1,Answer).

%Количество нечетных цифр числа, больших 3
numMoreThenThreeCount(X,Answer):-
    numMoreThenThreeCountInner(X,0,Answer).
numMoreThenThreeCountInner(0,Acc,Acc):-!.
numMoreThenThreeCountInner(X,Acc,Answer):-
    X1 is X div 10,
   (((X mod 10) > 3, (X mod 10) mod 2 =:= 1) ->
        Acc1 is Acc + 1
    ;
        Acc1 = Acc
   ),
    numMoreThenThreeCountInner(X1,Acc1,Answer).

%НОД для двух чисел
gcd(A, 0, A) :- !.
gcd(A, B, GCD) :-
    R is A mod B,
    gcd(B, R, GCD).



% Проверка на глобальный максимум
has_greater([Head|_], Element) :- Head > Element.
has_greater([_|Tail], Element) :- has_greater(Tail, Element). 

is_global_max(List, Index) :-
    get_at(Index, List, Element),
    \+ has_greater(List, Element).

% Проверка на локальный минимум
is_local_min(List, Index) :-
    get_at(Index, List, Element),
    check_left_neighbor(List, Index, Element),
    check_right_neighbor(List, Index, Element).

check_left_neighbor(List, Index, Element) :-
    LeftIndex is Index - 1,
    ( LeftIndex >= 0 ->
        get_at(LeftIndex, List, Left),
        Left > Element
    ; true ).

check_right_neighbor(List, Index, Element) :-
    RightIndex is Index + 1,
    length(List, Length),
    ( RightIndex < Length ->
        get_at(RightIndex, List, Right),
        Right > Element
    ; true ).  

% Циклический сдвиг влево на 1
rotate_left([], []).
rotate_left([Head | Tail], Rotated) :-
    my_ap(Tail, [Head], Rotated). 




% Основной предикат (лог_задача 1)
solve_guys :-
    People = [ [voronov, VJob], [pavlov, PJob], [levitsky, LJob], [sakharov, SJob] ],

    member([_, dancer], People),
    member([_, artist], People),
    member([_, singer], People),
    member([_, writer], People),

    % Условие 1: Воронов и Левицкий не певцы
    member([voronov, VJob], People), VJob \= singer,
    member([levitsky, LJob], People), LJob \= singer,

    % Условие 2: Павлов и писатель позировали художнику => ни Павлов, ни писатель не художник
    member([pavlov, PJob], People), PJob \= artist,
    member([WriterName, writer], People),
    member([ArtistName, artist], People),
    WriterName \= ArtistName,
    WriterName \= pavlov,

    % Условие 3 и 4: Писатель ≠ Сахаров, Писатель ≠ Воронов
    WriterName \= sakharov,
    WriterName \= voronov,

    % Условие 5: Воронов ≠ Левицкий (имена разные по-любому)

    % Вывод результата
    print_list(People).

print_list([]).
print_list([[Name, Job] | T]) :-
    format("~w - ~w~n", [Name, Job]),
    print_list(T).





% Задание 5
is_prime(2).
is_prime(N) :-
    N > 2,
    N mod 2 =\= 0,
    has_no_divisors(N, 3).

has_no_divisors(N, K) :- 
    K * K > N, !.
has_no_divisors(N, K) :- 
    N mod K =\= 0,
    NextK is K + 2,
    has_no_divisors(N, NextK).

max_prime_divisor(N, MaxDiv) :- 
    N > 1,
    max_prime_divisor(N, 2, 1, MaxDiv).

max_prime_divisor(1, _, Acc, Acc) :- !.
max_prime_divisor(N, K, Acc, MaxDiv) :- 
    K * K > N, !,
    (is_prime(N) -> MaxDiv = N ; MaxDiv = Acc).

max_prime_divisor(N, K, Acc, MaxDiv) :- 
    N mod K =:= 0,
    (is_prime(K) -> NewAcc = max(Acc, K) ; NewAcc = Acc),
    NextN is N // K,
    max_prime_divisor(NextN, K, NewAcc, MaxDiv).

max_prime_divisor(N, K, Acc, MaxDiv) :- 
    N mod K =\= 0,
    NextK is K + 1,
    max_prime_divisor(N, NextK, Acc, MaxDiv).


max_odd_nonprime_divisor(N, MaxDiv) :-
    N > 1,
    max_odd_nonprime_divisor(N, 3, 1, MaxDiv).

max_odd_nonprime_divisor(1, _, Acc, Acc) :- !.

max_odd_nonprime_divisor(N, K, Acc, MaxDiv) :- 
    K * K > N, !,
    (N > 1, N mod 2 =\= 0, \+ is_prime(N) -> 
        MaxDiv = max(Acc, N) 
    ; 
        MaxDiv = Acc
    ).

max_odd_nonprime_divisor(N, K, Acc, MaxDiv) :- 
    N mod K =:= 0,
    (K mod 2 =\= 0, \+ is_prime(K) -> 
        NewAcc = max(Acc, K),
        NextN is N // K,
        (NextN =:= 1 -> MaxDiv = NewAcc
         ; max_odd_nonprime_divisor(NextN, K, NewAcc, MaxDiv))
    ; 
        NextN is N // K,
        (NextN =:= 1 -> MaxDiv = Acc
         ; max_odd_nonprime_divisor(NextN, K, Acc, MaxDiv))
    ).

max_odd_nonprime_divisor(N, K, Acc, MaxDiv) :- 
    N mod K =\= 0,
    NextK is K + 2,
    max_odd_nonprime_divisor(N, NextK, Acc, MaxDiv).

digit_product(0, 0) :- !.
digit_product(N, S) :-
    N > 0,
    digit_product_tail(N, 1, S).

digit_product_tail(0, Acc, Acc) :- !.
digit_product_tail(N, Acc, S) :-
    N > 0,
    Digit is N mod 10,
    NewN is N // 10,
    NewAcc is Acc * Digit,
    digit_product_tail(NewN, NewAcc, S).

target_gcd(N, Result) :-
    N > 0,
    max_odd_nonprime_divisor(N, MaxDiv),
    digit_product(N, Product),
    gcd(MaxDiv, Product, Result).




% Задание 6
% Четные индексы затем нечетные индексы
split_even_odd(List, Result) :-
    split(List, 0, Even, Odd),
    my_ap(Even, Odd, Result),
    !.

split([], _, [], []).

split([H|T], Index, [H|Even], Odd) :-
    0 is Index mod 2,
    NewIndex is Index + 1,
    split(T, NewIndex, Even, Odd).

split([H|T], Index, Even, [H|Odd]) :-
    1 is Index mod 2,
    NewIndex is Index + 1,
    split(T, NewIndex, Even, Odd).

% Частоты уникальных элементов
count_occurrences(List, L1, L2) :-
    sort(List, Sorted),
    count_all(List, Sorted, L2),
    L1 = Sorted.

count_all(_, [], []).
count_all(List, [H|T], [Count|Counts]) :-
    count_in_list(List, H, Count),
    count_all(List, T, Counts).

count_in_list(List, Elem, Count) :-
    filter_elements(List, Elem, Filtered),
    length(Filtered, Count).

filter_elements([], _, []).
filter_elements([H|T], Elem, [H|Rest]) :-
    H == Elem,
    filter_elements(T, Elem, Rest).
filter_elements([H|T], Elem, Rest) :-
    H \== Elem,
    filter_elements(T, Elem, Rest).


% Лог задача 2


% Поворот списка (для удобства в определении "справа", "слева", "напротив")
rotate([H|T], R) :- append(T, [H], R).

solve_domino :-
    % Персонажи с фамилиями и профессиями
    Players = [ [F1,P1], [F2,P2], [F3,P3], [F4,P4] ],

    % Фамилии
    member([kuznetsov,_], Players),
    member([tokarev,_], Players),
    member([slesarev,_], Players),
    member([rezchikov,_], Players),

    % Профессии (фамилии других игроков)
    member([_,kuznetsov], Players);
    member([_,tokarev], Players);
    member([_,slesarev], Players);
    member([_,rezchikov], Players),

    % Все фамилии разные
    Fams = [F1,F2,F3,F4],
    sort(Fams, SortedF),
    length(SortedF, 4),

    % Все профессии разные
    Profs = [P1,P2,P3,P4],
    sort(Profs, SortedP),
    length(SortedP, 4),

    % Игроки в кругу: [1,2,3,4]
    Table = [ [F1,P1], [F2,P2], [F3,P3], [F4,P4] ],

    % Условия:
    % Напротив Кузнецова сидит слесарь
    nth0(KIndex, Table, [kuznetsov,_]),
    OppositeKIndex is (KIndex + 2) mod 4,
    nth0(OppositeKIndex, Table, [_,slesarev]),

    % Напротив Резчикова сидит резчик
    nth0(RIndex, Table, [rezchikov,_]),
    OppositeRIndex is (RIndex + 2) mod 4,
    nth0(OppositeRIndex, Table, [_,rezchikov]),

    % Справа от Слесарева сидит токарь
    nth0(SlIndex, Table, [slesarev,_]),
    RightIndex is (SlIndex + 1) mod 4,
    nth0(RightIndex, Table, [_,tokarev]),

    % Кто слева от Кузнеца?
    LeftIndex is (KIndex + 3) mod 4,
    nth0(LeftIndex, Table, [WhoLeft,_]),

    write("Слева от кузнеца сидит: "), write(WhoLeft), nl.