namespace ChatRooms.FakeInfrastructure;

public class FakeInfrastructureOptions

{
    public bool GenerateFakeData { get; set; }
    public RecordsCount RecordsCount { get; set; } = new RecordsCount();
}

public class RecordsCount
{
    public int Rooms;
    public int Users;
}