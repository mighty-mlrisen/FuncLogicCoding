module FavoriteLanguage


let chooseLanguage language =
    match language with
    | "F#" | "Prolog" -> "Подлиза!"
    | "Java" -> "Круто!"
    | "Python" -> "Пойдет"
    | _ -> "Лучше F#"
