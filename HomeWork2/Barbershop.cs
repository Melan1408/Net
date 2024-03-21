namespace HomeWork2
{
    public class Barbershop
    {
        private readonly Barber barber;
        public Thread ?BarberThread;
        public readonly Semaphore WaitForHaircat;
        public readonly Semaphore GoForhaircut = new Semaphore(1, 1);      
        public readonly int PlaceForWait;
        public int NumberOfClient { get; set; }
        public bool IsOpened { get; set; } = false;
        public Barbershop(int placeForWait)
        {
            barber = new Barber(this);
            this.PlaceForWait = placeForWait;
            WaitForHaircat = new Semaphore(placeForWait, placeForWait);
        }

        public void OpenBarberShop()
        {
            Console.WriteLine("Барбершоп открылся");
            IsOpened = true;
            BarberThread = new Thread(barber.Job);
            BarberThread.Start();
        }

        public void CloseBarberShop()
        {
            IsOpened = false;
            Console.WriteLine("Барбершоп закрылся");
        }
        public void AskBarberForHaircut(Client client)
        {
            GoForhaircut.WaitOne();
            barber.Client = client;           
        }
    }     
    
}
