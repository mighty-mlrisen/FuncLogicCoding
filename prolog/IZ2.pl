:- dynamic edge/2.

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

parse_edge_line(Line, U, V) :-
    string_length(Line, Len), Len > 2,
    InnerLen is Len - 2,
    sub_string(Line, 1, InnerLen, 1, Inner),
    split_string(Inner, ",", " \t", [S1,S2]),
    atom_string(U, S1),
    atom_string(V, S2).



all_maximal_matchings(MaxMs) :-
    findall(edge(U,V), edge(U,V), Edges),
    setof(M, matching_maximal(Edges, [], M), MaxMs).


matching_maximal([], Acc, Acc) :-
    \+ ( edge(U,V),
         \+ member(edge(U,V), Acc),
         \+ conflict(edge(U,V), Acc)
       ).

matching_maximal([E|Es], Acc, M) :-
    conflict(E, Acc), !,
    matching_maximal(Es, Acc, M).

matching_maximal([E|Es], Acc, M) :-
    (   % Вариант взять ребро E
        matching_maximal(Es, [E|Acc], M)
    ;   % Вариант пропустить ребро E
        matching_maximal(Es, Acc, M)
    ).

conflict(edge(U,V), Acc) :-
    ( member(edge(U,_), Acc)
    ; member(edge(_,U), Acc)
    ; member(edge(V,_), Acc)
    ; member(edge(_,V), Acc)
    ).


write_matchings(File, Matchings) :-
    open(File, write, Out),
    write_each(Out, Matchings),
    % посчитать число найденных матчингов:
    length(Matchings, Count),
    format(Out, '~nЧисло максимальных паросочетаний: ~w~n', [Count]),
    close(Out).

write_each(_, []).
write_each(Out, [M|Ms]) :-
    write_matching(Out, M), nl(Out),
    write_each(Out, Ms).

write_matching(Out, []) :- write(Out, '').
write_matching(Out, [edge(U,V)]) :-
    format(Out, '(~w,~w)', [U,V]).
write_matching(Out, [edge(U,V)|Es]) :-
    format(Out, '(~w,~w),', [U,V]),
    write_matching(Out, Es).

