namespace VideoMatrixApp.Models
{
    //Coenxiones activas en ese momento,  ahora mismo 
    public class Connection
    {
        public int Id { get; set; }

        // Receptor
        public int ReceiverId { get; set; }
        public Device Receiver { get; set; }

        // Transmisor
        public int TransmitterId { get; set; }
        public Device Transmitter { get; set; }
    }
}
