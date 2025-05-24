% Размещения с повторениями
razm_povt(Alphabet, 0, Razm, Razm):-!.
razm_povt(Alphabet, N, Cur, Razm):-
    in_list(El,Alphabet),
    N1 is N-1,
    razm_povt(Alphabet, N1, [El|Cur], Razm).

% Сочетания без повторений
comb([], _, 0):-!.
comb([H|Sub], [H|Set], K):-
    K1 is K-1,
    comb(Sub,Set,K1).
comb(Sub, [H|Set], K):-comb(Sub, Set, K).

% Нерекурсивные размещения с повторениями
nonrec_razm_povt(Alphabet, K, Result) :-
    length(Result, K),
    maplist(member_(Alphabet), Result).

member_(Alphabet, Elem) :- member(Elem, Alphabet).

% Нерекурсивные сочетания без повторений
nonrec_comb(Alphabet, K, Result) :-
    length(Alphabet, N),
    K =< N,
    numlist(1, N, Indices),
    comb_indices(Indices, K, CombIndices),
    maplist(nth1_(Alphabet), CombIndices, Result).

nth1_(List, Index, Elem) :- nth1(Index, List, Elem).

comb_indices(_, 0, []).
comb_indices([H|T], K, [H|Comb]) :-
    K > 0,
    K1 is K - 1,
    comb_indices(T, K1, Comb).
comb_indices([_|T], K, Comb) :-
    K > 0,
    comb_indices(T, K, Comb).

% Слова длины K с 3 буквами a
words_with_3a(Alphabet, K, Word) :-
    length(Word, K),
    include(==(a), Word, As),
    length(As, 3),
    maplist(member_(Alphabet), Word).

% Генератор размещений с повторениями
next_razm_povt(Alphabet, K, R) :-
    razm_povt(Alphabet, K, [], R).

% Генератор сочетаний без повторений
next_comb(Alphabet, K, C) :-
    comb(Alphabet, K, C, _).

% Предикат для слов с 3 'a'
words_3a(Alphabet, K, Word) :-
    length(Word, K),
    count_a(Word, 3),
    maplist(member_(Alphabet), Word).

count_a([], 0).
count_a([a|T], N) :-
    count_a(T, N1),
    N is N1 + 1.
count_a([H|T], N) :-
    H \= a,
    count_a(T, N).

% Вывод в файл всех комб. объектов
open_output_file(Filename, Stream) :-
    open(Filename, write, Stream).

close_output_file(Stream) :-
    close(Stream).

% 1. Размещения без повторений
razm_bez_povt(_, 0, Razm, Razm) :- !.
razm_bez_povt(Alphabet, N, Cur, Razm) :-
    select(El, Alphabet, RestAlphabet),
    N1 is N-1,
    razm_bez_povt(RestAlphabet, N1, [El|Cur], Razm).

write_razm_povt(Alphabet, K, Filename) :-
    open_output_file(Filename, Stream),
    (   razm_bez_povt(Alphabet, K, [], R),
        write(Stream, R),
        nl(Stream),
        fail
    ;   close_output_file(Stream)
    ).

% 2. Перестановки
perm([], []).
perm(List, [X|Perm]) :-
    select(X, List, Rest),
    perm(Rest, Perm).

write_perm(Alphabet, Filename) :-
    open_output_file(Filename, Stream),
    (   perm(Alphabet, P),
        write(Stream, P),
        nl(Stream),
        fail
    ;   close_output_file(Stream)
    ).

% 3. Слова длины 5 с ровно 2 буквами 'a'
count_a([], 0).
count_a([a|T], N) :-
    count_a(T, N1),
    N is N1 + 1.
count_a([H|T], N) :-
    H \= a,
    count_a(T, N).

all_distinct_except_a([]).
all_distinct_except_a([a|T]) :-
    all_distinct_except_a(T).
all_distinct_except_a([H|T]) :-
    H \= a,
    \+ member(H, T),
    all_distinct_except_a(T).

word_2a(Alphabet, Word) :-
    length(Word, 5),
    maplist(in_list(Alphabet), Word),
    count_a(Word, 2),
    all_distinct_except_a(Word).

write_words_2a(Alphabet, Filename) :-
    open_output_file(Filename, Stream),
    (   word_2a(Alphabet, W),
        write(Stream, W),
        nl(Stream),
        fail
    ;   close_output_file(Stream)
    ).

% 4. Слова длины 6 с 2 повторяющимися буквами
count_repeats(Word, DoublesCount, RepeatTimes) :-
    msort(Word, Sorted),
    group_pairs(Sorted, Groups),
    count_matching_groups(Groups, RepeatTimes, DoublesCount),
    check_other_groups(Groups, RepeatTimes).

group_pairs([], []).
group_pairs([X|Xs], [[X|Ys]|Zss]) :-
    group_pairs(Xs, Yss),
    (   Yss = [[Y|Ys]|Zss],
        X = Y
    ->  true
    ;   Yss = Zss,
        Ys = []
    ).

count_matching_groups(Groups, Times, Count) :-
    include([G]>>length(G, Times), Groups, Matching),
    length(Matching, Count).

check_other_groups([], _).
check_other_groups([G|Gs], Times) :-
    (   length(G, 1)
    ;   length(G, Times)
    ),
    check_other_groups(Gs, Times).

word_doubles(Alphabet, Word) :-
    length(Word, 6),
    maplist(in_list(Alphabet), Word),
    count_repeats(Word, 2, 2).

write_words_doubles(Alphabet, Filename) :-
    open_output_file(Filename, Stream),
    (   word_doubles(Alphabet, W),
        write(Stream, W),
        nl(Stream),
        fail
    ;   close_output_file(Stream)
    ).

% 1. Количество путей длины n в смешанном графе
% Вершины
node(a).
node(b).
node(c).
node(d).
node(e).

% Рёбра (смешанный граф)
edge(a,b).
edge(b,c).
edge(c,d).
edge(d,a).
edge(b,d).
edge(d,e).
edge(e,c).

% Уникальное добавление элемента в список
add_unique(X, [], [X]).
add_unique(X, [X|T], [X|T]).
add_unique(X, [H|T], [H|R]) :-
    add_unique(X, T, R).

all_nodes(Nodes) :-
    collect_nodes([], Nodes).

collect_nodes(Acc, Nodes) :-
    node(X),
    add_unique(X, Acc, NewAcc),
    fail.
collect_nodes(Acc, Acc).

% Подсчёт количества путей длины N
path_count(N, Count) :-
    all_nodes(Nodes),
    count_from_all(Nodes, N, 0, Count).

count_from_all([], _, Acc, Acc).
count_from_all([Node|Rest], N, Acc, Count) :-
    count_paths(Node, N, C),
    Acc1 is Acc + C,
    count_from_all(Rest, N, Acc1, Count).

count_paths(_, 0, 1) :- !.
count_paths(Node, N, Count) :-
    N > 0,
    N1 is N - 1,
    count_edges(Node, N1, 0, Count).

count_edges(Node, N1, Acc, Count) :-
    ( edge(Node, Next),
      count_paths(Next, N1, C),
      Acc1 is Acc + C,
      count_edges(Node, N1, Acc1, Count)
    );
    Count = Acc.

% Составление независимого множества вершин
independent_set(Set) :-
    all_nodes(Nodes),
    build_independent(Nodes, [], Set).

build_independent([], Set, Set).
build_independent([H|T], Acc, Set) :-
    \+ adjacent(H, Acc),
    append_custom(Acc, [H], NewAcc),
    build_independent(T, NewAcc, Set).
build_independent([_|T], Acc, Set) :-
    build_independent(T, Acc, Set).

adjacent(_, []) :- fail.
adjacent(Node, [H|_]) :-
    edge(Node, H);
    edge(H, Node).
adjacent(Node, [_|T]) :-
    adjacent(Node, T).

% Своя версия append
append_custom([], L, L).
append_custom([H|T], L, [H|R]) :-
    append_custom(T, L, R).