namespace VideoMatrixApp
{
    //Las conexiones cuando se pulsa guardar perfil
    public class ProfileConnection
    {
        public int Id { get; set; }

        // Perfil al que pertenece
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        // Receptor guardado
        public int ReceiverId { get; set; }
        public Device Receiver { get; set; }

        // Transmisor guardado
        public int TransmitterId { get; set; }
        public Device Transmitter { get; set; }
    }
}
