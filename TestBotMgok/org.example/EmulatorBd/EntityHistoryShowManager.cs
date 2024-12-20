namespace ConsoleAppTetsBot.org.example.EmulatorBd;

public class EntityHistoryShowManager
{
    public List<EntityHistoryShow> EntityHistoryShows { get; } = new List<EntityHistoryShow>()
    {
        new EntityHistoryShow
        {
            Id = "001", IsActive = "завершена", AddressOfPlace = "Лодочная 7", NumberCabinet = "405",
            NumberPhone = "1234567802", DesciptionOfProblem = "сломалcя принтер", DateTime = DateTime.Now
        },

        new EntityHistoryShow
        {
            Id = "002", IsActive = "завершена", AddressOfPlace = "Молдавская 5", NumberCabinet = "301",
            NumberPhone = "123213212", DesciptionOfProblem = "Поломка проектора",
            DateTime = DateTime.Now
        },
        new EntityHistoryShow
        {
            Id = "003", IsActive = "завершена", AddressOfPlace = "Молдавская 5", NumberCabinet = "402",
            NumberPhone = "89810374983", DesciptionOfProblem = "Не работает интернет",
            DateTime = DateTime.Now
        },
        new EntityHistoryShow
        {
            Id = "004", IsActive = "завершена", AddressOfPlace = "Свобода 33", NumberCabinet = "317",
            NumberPhone = "89034312343", DesciptionOfProblem = "Сломался компьюер",
            DateTime = DateTime.Now
        }
    };
}