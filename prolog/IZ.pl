:- dynamic edge/2.

% solve(+InputFile, +OutputFile)
solve(InputFile, OutputFile) :-
    retractall(edge(_,_)),
    read_graph(InputFile),
    all_maximal_matchings(Matchings),
    write_matchings(OutputFile, Matchings),
    format('Matchings written to ~w~n', [OutputFile]).

read_graph(File) :-
    open(File, read, In),
    read_lines(In),
    close(In).

read_lines(In) :-
    read_line_to_string(In, Line),
    ( Line == end_of_file -> true
    ; ( parse_edge_line(Line, U, V) -> assertz(edge(U,V)) ; true ),
      read_lines(In)
    ).

% Разбор строки вида "(v1,v2)" в два атома U и V
parse_edge_line(Line, U, V) :-
    string_length(Line, Len), Len > 2,
    InnerLen is Len - 2,
    sub_string(Line, 1, InnerLen, 1, Inner), 
    split_string(Inner, ",", " 	", [S1,S2]),
    atom_string(U, S1),
    atom_string(V, S2).



all_maximal_matchings(MaxMs) :-
    findall(edge(U,V), edge(U,V), Edges),
    findall(M, matching(Edges, M), All),
    include(is_maximal(All), All, MaxMs).

matching(Edges, M) :- match_edges(Edges, [], M).
match_edges([], Acc, Acc).
match_edges([E|Es], Acc, M) :-
    conflict(E, Acc), !,
    match_edges(Es, Acc, M).
match_edges([E|Es], Acc, M) :-
    ( match_edges(Es, [E|Acc], M)
    ; match_edges(Es, Acc, M)
    ).

conflict(edge(U,V), Acc) :-
    ( member(edge(U,_), Acc)
    ; member(edge(_,U), Acc)
    ; member(edge(V,_), Acc)
    ; member(edge(_,V), Acc)
    ).

is_maximal(All, M) :-
    \+ ( member(N, All), N \= M, subset_list(M, N) ).
subset_list([], _).
subset_list([X|Xs], Ys) :- member(X, Ys), subset_list(Xs, Ys).


write_matchings(File, Matchings) :-
    open(File, write, Out),
    write_each(Out, Matchings),
    close(Out).

write_each(_, []).
write_each(Out, [M|Ms]) :-
    write_matching(Out, M), nl(Out),
    write_each(Out, Ms).

write_matching(Out, []) :- write(Out, "").
write_matching(Out, [edge(U,V)]) :-
    format(Out, '(~w,~w)', [U,V]).
write_matching(Out, [edge(U,V)|Es]) :-
    format(Out, '(~w,~w),', [U,V]),
    write_matching(Out, Es).


/*
?- solve('input.txt','output.txt').
*/