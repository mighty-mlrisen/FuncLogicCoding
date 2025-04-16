man(anatoliy).
man(dimitriy).
man(vlad).
man(kirill).
man(mefodiy).
woman(vladina).
woman(galya).
woman(sveta).
woman(valentina).
woman(zoya).
woman(katrin).
child(dimitriy, anatoliy).
child(dimitriy, galya).
child(vladina, anatoliy).
child(vladina, galya).
child(valentina, anatoliy).
child(valentina, galya).
child(kirill, dimitriy).
child(mefodiy, dimitriy).
child(kirill, sveta).
child(mefodiy, sveta).
child(zoya, vlad).
child(zoya, vladina).
child(katrin, vlad).
child(katrin, vladina).

#consult('family.pl').

men(X):- man(X).
women(X):- woman(X).

children(X):- child(Y,X), write(Y), nl, fail.


mother(X,Y):- 
    woman(X),
    child(Y,X).

mother(X):-
    mother(Y,X),
    write(Y),
    nl,
    fail.

brother(X,Y):- 
    man(X),
    man(Y),
    child(X,Z),
    child(Y,Z),
    X \= Y.


brothers(X):-
    setof(Y, brother(X, Y), Broth),
    write(Broth),
    nl.

parent(X,Y):- child(Y,X).

b_s(X, Y):- X \= Y, parent(Z,X), parent(Z,Y).

b_s(X):- 
    setof(Y, b_s(X,Y), BS),
    write(BS), nl.

father(X, Y):- 
    man(X),
	child(Y, X).

father(X) :-
	father(Y, X), write(Y), nl.


wife(X, Y) :-
	woman(X),
	child(P, X),
	child(P, Y),
	X \= Y.

wife(X) :-
	wife(Y, X), write(Y).

grand_ma(X, Y) :-
    woman(X),
    child(Z, X),
    child(Y, Z).

grand_mas(X) :-
    setof(Y, grand_ma(Y, X), Gmas),
    write(Gmas), nl.


grand_pa_and_da(X, Y) :-
    (   
        man(X), woman(Y), child(Z, X), child(Y, Z) 
    )
    ;  
    ( 
        man(Y), woman(X), child(Z, Y), child(X, Z)
    ).


sibling(X, Y) :-
    parent(Z, X),
    parent(Z, Y),
    X \= Y.

niece(X, Y) :-
    woman(X),          
    parent(P, X),        
    sibling(Y, P).

nieces(X) :-
    setof(N, niece(N, X), NList),  
    write(NList), nl.