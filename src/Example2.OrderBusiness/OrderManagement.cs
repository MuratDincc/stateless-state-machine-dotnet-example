using Stateless.Graph;
using Stateless;

namespace Example2.OrderBusiness;

public class OrderManagement
{
    enum Trigger
    {
        OrderPaymentComplete,
        OrderShipment,
        OrderShipmentComplete,
        OrderCompleted,
        OrderRefund,
    }

    enum State
    {
        Created,
        Processing,
        Shipped,
        Delivered,
        Complete,
        Refund
    }

    State _initState = State.Created;

    StateMachine<State, Trigger> _machine;

    public OrderManagement()
    {
        _machine = new StateMachine<State, Trigger>(_initState);

        _machine.Configure(State.Created)
            .Permit(Trigger.OrderPaymentComplete, State.Processing);

        _machine.Configure(State.Processing)
            .Permit(Trigger.OrderShipment, State.Shipped);

        _machine.Configure(State.Shipped)
            .Permit(Trigger.OrderShipmentComplete, State.Delivered)
            .Permit(Trigger.OrderCompleted, State.Complete);

        _machine.Configure(State.Delivered)
            .Permit(Trigger.OrderCompleted, State.Complete);

        _machine.Configure(State.Complete)
            .Permit(Trigger.OrderRefund, State.Refund);

        _machine.OnTransitioned(t => Console.WriteLine($"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}({string.Join(", ", t.Parameters)})"));
    }

    public void OrderPaymentComplete()
    {
        _machine.Fire(Trigger.OrderPaymentComplete);
    }

    public void OrderShipment()
    {
        _machine.Fire(Trigger.OrderShipment);
    }

    public void OrderShipmentComplete()
    {
        _machine.Fire(Trigger.OrderShipmentComplete);
    }

    public void OrderCompleted()
    {
        _machine.Fire(Trigger.OrderCompleted);
    }

    public void OrderRefund()
    {
        _machine.Fire(Trigger.OrderRefund);
    }

    public string ToDotGraph()
    {
        return UmlDotGraph.Format(_machine.GetInfo());
    }
}