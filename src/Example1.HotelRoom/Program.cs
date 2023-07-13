using Example1.HotelRoom;

var hotelRoom = new HotelRoomManagement("424");

hotelRoom.TakeCard();
hotelRoom.TakeOutCard();

hotelRoom.ToDotGraph();

Console.WriteLine("Press any key...");
Console.ReadKey(true);