
def write_to_file(filename, data):
    with open(filename, 'w') as f:
        for item in data:
            f.write(f"{item}\n")


# 1. Размещения без повторений
def arrangements_recursive(alphabet, k, current=[], result=[]):
    if len(current) == k:
        result.append(''.join(current))
        return
    for char in alphabet:
        if char not in current:
            arrangements_recursive(alphabet, k, current + [char], result)
    return result


from itertools import permutations


def arrangements_nonrecursive(alphabet, k):
    return [''.join(p) for p in permutations(alphabet, k)]


# 2. Перестановки
def permutations_recursive(alphabet, current=[], result=[]):
    if not alphabet:
        result.append(''.join(current))
        return
    for i, char in enumerate(alphabet):
        permutations_recursive(alphabet[:i] + alphabet[i + 1:], current + [char], result)
    return result


def permutations_nonrecursive(alphabet):
    return [''.join(p) for p in permutations(alphabet)]


# 3. Слова длины 5 с ровно 2 буквами 'a'
def words_with_2a_recursive(alphabet, length=5, a_count=2, current='', result=[]):
    if len(current) == length:
        if current.count('a') == a_count:
            result.append(current)
        return
    for char in alphabet:
        if char == 'a':
            if current.count('a') < a_count:
                words_with_2a_recursive(alphabet, length, a_count, current + char, result)
        else:
            if char not in current.replace('a', ''):
                words_with_2a_recursive(alphabet, length, a_count, current + char, result)
    return result

from itertools import  combinations

def words_with_2a_nonrecursive(alphabet, length=5, a_count=2):
    result = []
    non_a = [c for c in alphabet if c != 'a']
    for positions in combinations(range(length), a_count):
        for other_chars in permutations(non_a, length - a_count):
            word = [''] * length
            for pos in positions:
                word[pos] = 'a'
            for i, char in zip([i for i in range(length) if i not in positions], other_chars):
                word[i] = char
            result.append(''.join(word))
    return result


# 4. Слова длины 6 с ровно 2 буквами, повторяющимися 2 раза
def words_with_doubles_recursive(alphabet, length=6, current='', result=[]):
    if len(current) == length:
        counts = {}
        for c in current:
            counts[c] = counts.get(c, 0) + 1
        doubles = [k for k, v in counts.items() if v == 2]
        if len(doubles) == 2 and all(v in (1, 2) for v in counts.values()):
            result.append(current)
        return
    for char in alphabet:
        if current.count(char) < 2:
            words_with_doubles_recursive(alphabet, length, current + char, result)
    return result


def words_with_doubles_nonrecursive(alphabet, length=6):
    result = []
    for double_chars in combinations(alphabet, 2):
        remaining = [c for c in alphabet if c not in double_chars]
        for other_chars in combinations(remaining, length - 4):
            chars = list(double_chars) * 2 + list(other_chars)
            seen = set()
            for p in permutations(chars):
                word = ''.join(p)
                if word not in seen:
                    seen.add(word)
                    result.append(word)
    return result


# Пример использования
if __name__ == "__main__":
    alphabet = ['a', 'b', 'c', 'd']

    # 1. Размещения без повторений
    arr_rec = arrangements_recursive(alphabet, 3)
    write_to_file('output/arrangements_recursive.txt', arr_rec)

    arr_nonrec = arrangements_nonrecursive(alphabet, 3)
    write_to_file('output/arrangements_nonrecursive.txt', arr_nonrec)

    # 2. Перестановки
    perm_rec = permutations_recursive(alphabet)
    write_to_file('output/permutations_recursive.txt', perm_rec)

    perm_nonrec = permutations_nonrecursive(alphabet)
    write_to_file('output/permutations_nonrecursive.txt', perm_nonrec)

    # 3. Слова с 2 буквами 'a'
    words2a_rec = words_with_2a_recursive(alphabet, 5, 2)
    write_to_file('output/words_2a_recursive.txt', words2a_rec)

    words2a_nonrec = words_with_2a_nonrecursive(alphabet, 5, 2)
    write_to_file('output/words_2a_nonrecursive.txt', words2a_nonrec)

    # 4. Слова с 2 парами повторений
    words_dbl_rec = words_with_doubles_recursive(alphabet, 6)
    write_to_file('output/words_doubles_recursive.txt', words_dbl_rec)

    words_dbl_nonrec = words_with_doubles_nonrecursive(alphabet, 6)
    write_to_file('output/words_doubles_nonrecursive.txt', words_dbl_nonrec)
