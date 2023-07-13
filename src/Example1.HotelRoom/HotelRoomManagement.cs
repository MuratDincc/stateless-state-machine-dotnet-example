using Stateless.Graph;
using Stateless;

namespace Example1.HotelRoom;

public class HotelRoomManagement
{
    enum Trigger
    {
        TakeCard,
        TakeOutCard,
    }

    enum State
    {
        Passive,
        Active
    }

    State _initState = State.Passive;

    StateMachine<State, Trigger> _machine;

    string _roomNumber;

    public HotelRoomManagement(string roomNumber)
    {
        _roomNumber = roomNumber;

        _machine = new StateMachine<State, Trigger>(_initState);

        _machine.Configure(State.Passive)
            .Permit(Trigger.TakeCard, State.Active);

        _machine.Configure(State.Active)
            .Permit(Trigger.TakeOutCard, State.Passive);

        _machine.OnTransitioned(t => Console.WriteLine($"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"));
    }

    public void TakeCard()
    {
        _machine.Fire(Trigger.TakeCard);
    }

    public void TakeOutCard()
    {
        _machine.Fire(Trigger.TakeOutCard);
    }

    public void Print()
    {
        Console.WriteLine("Room Number: {0} Status: {1}", _roomNumber, _machine.State);
    }

    public string ToDotGraph()
    {
        return UmlDotGraph.Format(_machine.GetInfo());
    }
}