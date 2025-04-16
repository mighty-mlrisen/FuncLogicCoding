module VehiclePassport

open System
open System.Text.RegularExpressions

type VehiclePassport
    (
        series: string,
        number: string,
        lastName: string,
        firstName: string,
        middleName: string,
        vehicleModel: string,
        issueDate: DateTime
    ) =

    do
        let isSeriesValid = Regex("^[A-Z]{2}$").IsMatch(series)
        let isNumberValid = Regex("^\d{6}$").IsMatch(number)
        let isNameValid = 
            Regex("^[А-Яа-яЁё]+$").IsMatch(lastName) &&
            Regex("^[А-Яа-яЁё]+$").IsMatch(firstName) &&
            Regex("^[А-Яа-яЁё]+$").IsMatch(middleName)

        let isModelValid = Regex("^[A-Za-zА-Яа-я0-9\s]+$").IsMatch(vehicleModel)

        let isIssueDateValid = issueDate <= DateTime.Now

        if not (isSeriesValid && isNumberValid && isNameValid && isModelValid && isIssueDateValid) then
            raise (ArgumentException("Данные не прошли валидацию. Проверьте вводимые значения."))

    member this.Series = series
    member this.Number = number
    member this.LastName = lastName
    member this.FirstName = firstName
    member this.MiddleName = middleName
    member this.VehicleModel = vehicleModel
    member this.IssueDate = issueDate

    override this.ToString() =
        sprintf "Паспорт транспортного средства\nВладелец: %s %s %s\nМодель автомобиля: %s\nСерия: %s\nНомер: %s\nДата выдачи: %s"
            this.LastName this.FirstName this.MiddleName
            this.VehicleModel this.Series this.Number
            (this.IssueDate.ToShortDateString())

    override this.Equals(obj) =
        match obj with
        | :? VehiclePassport as other ->
            this.Series = other.Series && this.Number = other.Number
        | _ -> false

    override this.GetHashCode() =
        hash (this.Series, this.Number)
