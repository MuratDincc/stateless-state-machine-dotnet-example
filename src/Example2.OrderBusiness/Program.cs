using Example2.OrderBusiness;

var orderBusiness = new OrderManagement();

orderBusiness.OrderPaymentComplete();
orderBusiness.OrderShipment();
orderBusiness.OrderShipmentComplete();
orderBusiness.OrderCompleted();
orderBusiness.OrderRefund();

orderBusiness.ToDotGraph();

Console.WriteLine("Press any key...");
Console.ReadKey(true);