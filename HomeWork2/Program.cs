using HomeWork2;

var barberShop = new Barbershop(3);
barberShop.OpenBarberShop();

for (int i = 1; i <= 10; i++)
{
    var client = new Client(barberShop, i);
    Thread.Sleep(1000);
}
while(barberShop.NumberOfClient != 0)
{
    Thread.Sleep(1000);
}
barberShop.CloseBarberShop();
