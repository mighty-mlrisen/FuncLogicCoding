% Habitat (1 = Land, 2 = Water, 3 = Air)
habitat(lion, 1).
habitat(elephant, 1).
habitat(shark, 2).
habitat(dolphin, 2).
habitat(sparrow, 3).
habitat(parrot, 3).
habitat(cow, 1).
habitat(horse, 1).
habitat(chicken, 1).
habitat(cat, 1).
habitat(dog, 1).
habitat(fox, 1).
habitat(bear, 1).
habitat(crocodile, 2).
habitat(turtle, 2).
habitat(penguin, 2).
habitat(wolf, 1).
habitat(owl, 3).
habitat(rat, 1).
habitat(snake, 1).
habitat(hummingbird, 3).
habitat(eagle, 3).
habitat(bat, 3).

% Size (1 = Small, 2 = Medium, 3 = Large)
size(lion, 3).
size(elephant, 3).
size(shark, 3).
size(dolphin, 2).
size(sparrow, 1).
size(parrot, 1).
size(cow, 3).
size(horse, 3).
size(chicken, 1).
size(cat, 1).
size(dog, 2).
size(fox, 2).
size(bear, 3).
size(crocodile, 3).
size(turtle, 2).
size(penguin, 2).
size(wolf, 2).
size(owl, 1).
size(rat, 1).
size(snake, 2).
size(hummingbird, 1).
size(eagle, 3).
size(bat, 1).

% Diet (1 = Carnivore, 2 = Herbivore, 3 = Omnivore)
diet(lion, 1).
diet(elephant, 2).
diet(shark, 1).
diet(dolphin, 3).
diet(sparrow, 3).
diet(parrot, 3).
diet(cow, 2).
diet(horse, 2).
diet(chicken, 3).
diet(cat, 1).
diet(dog, 3).
diet(fox, 1).
diet(bear, 3).
diet(crocodile, 1).
diet(turtle, 2).
diet(penguin, 3).
diet(wolf, 1).
diet(owl, 1).
diet(rat, 3).
diet(snake, 1).
diet(hummingbird, 3).
diet(eagle, 1).
diet(bat, 3).

% Mammal (1 = Yes, 0 = No)
mammal(lion, 1).
mammal(elephant, 1).
mammal(shark, 0).
mammal(dolphin, 1).
mammal(sparrow, 0).
mammal(parrot, 0).
mammal(cow, 1).
mammal(horse, 1).
mammal(chicken, 0).
mammal(cat, 1).
mammal(dog, 1).
mammal(fox, 1).
mammal(bear, 1).
mammal(crocodile, 0).
mammal(turtle, 0).
mammal(penguin, 0).
mammal(wolf, 1).
mammal(owl, 0).
mammal(rat, 1).
mammal(snake, 0).
mammal(hummingbird, 0).
mammal(eagle, 0).
mammal(bat, 1).

% Fly (1 = Yes, 0 = No)
fly(sparrow, 1).
fly(parrot, 1).
fly(cow, 0).
fly(horse, 0).
fly(chicken, 0).
fly(cat, 0).
fly(dog, 0).
fly(fox, 0).
fly(bear, 0).
fly(crocodile, 0).
fly(turtle, 0).
fly(penguin, 0).
fly(wolf, 0).
fly(owl, 1).
fly(rat, 0).
fly(snake, 0).
fly(hummingbird, 1).
fly(eagle, 1).
fly(bat, 1).

% Domestic (1 = Yes, 0 = No)
domestic(lion, 0).
domestic(elephant, 0).
domestic(shark, 0).
domestic(dolphin, 0).
domestic(sparrow, 0).
domestic(parrot, 1).
domestic(cow, 1).
domestic(horse, 1).
domestic(chicken, 1).
domestic(cat, 1).
domestic(dog, 1).
domestic(fox, 0).
domestic(bear, 0).
domestic(crocodile, 0).
domestic(turtle, 0).
domestic(penguin, 0).
domestic(wolf, 0).
domestic(owl, 0).
domestic(rat, 1).
domestic(snake, 0).
domestic(hummingbird, 0).
domestic(eagle, 0).
domestic(bat, 0).




question1(X1):- write("Does the animal live on land, in water, or in the air?"), nl,
                write("1. Land"), nl,
                write("2. Water"), nl,
                write("3. Air"), nl,
                read(X1).

question2(X2):- write("What is the size of the animal?"), nl,
                write("1. Small"), nl,
                write("2. Medium"), nl,
                write("3. Large"), nl,
                read(X2).

question3(X3):- write("Does the animal eat meat (carnivore), plants (herbivore), or both (omnivore)?"), nl,
                write("1. Carnivore"), nl,
                write("2. Herbivore"), nl,
                write("3. Omnivore"), nl,
                read(X3).

question4(X4):- write("Is the animal a mammal?"), nl,
                write("1. Yes"), nl,
                write("0. No"), nl,
                read(X4).

question5(X5):- write("Is the animal capable of flight?"), nl,
                write("1. Yes"), nl,
                write("0. No"), nl,
                read(X5).

question6(X6):- write("Is the animal domestic?"), nl,
                write("1. Yes"), nl,
                write("0. No"), nl,
                read(X6).




pr :-   question1(X1), question2(X2), question3(X3), question4(X4),
        question5(X5), question6(X6),habitat(X, X2), size(X,X2), 
        diet(X, X3), mammal(X, X4),
        fly(X, X5), domestic(X, X6), write(X).
    