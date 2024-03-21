namespace HomeWork2
{
    public class Barber
    {
        private readonly Barbershop placeOfWork;

        public Client ?Client { get; set; }

        public Barber(Barbershop placeOfWork)
        {
            this.placeOfWork = placeOfWork;
            
        }

        public void Job()
        {
            while (placeOfWork.IsOpened)
            {
                if (placeOfWork.NumberOfClient == 0) Sleep();
                else if (Client != null) Haircut();
            }
        }

        private void Haircut()
        {
                Console.WriteLine($"Барбер стрижет {Client.Name}");
                placeOfWork.WaitForHaircat.Release();
                Thread.Sleep(2000);
                Console.WriteLine($"Барбер подстриг {Client.Name}");
                Console.WriteLine($"{Client.Name} ушел");
                Client = null;
                placeOfWork.GoForhaircut.Release();
                placeOfWork.NumberOfClient--;
        }

        private void Sleep()
        {
            Console.WriteLine("Барбер спит, клиентов нет");
            Thread.Sleep(1000);
        }
    }
}
