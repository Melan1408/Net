namespace HomeWork2
{
    public class Client
    {
        public string Name { get; set; }
   
        public Client(Barbershop barbershop, int numberOfClient)
        {
            Name = $"Клиент {numberOfClient}";
            var barbershopThread = new Thread(() => EnterBarberShop(barbershop));
            barbershopThread.Start();         
        }

        public void EnterBarberShop(Barbershop barbershop)
        {
            if (barbershop.NumberOfClient <= barbershop.PlaceForWait)
            {
                barbershop.WaitForHaircat.WaitOne();
                Console.WriteLine($"{Name} вошел в барбершоп");
                barbershop.NumberOfClient++;                
                var askBarberThread = new Thread(() => barbershop.AskBarberForHaircut(this));
                askBarberThread.Start();               
            }
            else Console.WriteLine($"{Name} понял что мест нет и ушел");
        }

    }
}
