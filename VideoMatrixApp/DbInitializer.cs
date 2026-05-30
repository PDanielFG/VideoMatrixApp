namespace VideoMatrixApp
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Si ya hay dispositivos, no hacemos nada
            if (context.Devices.Any())
                return;

            // Transmisores
            var tx1 = new Device
            {
                Name = "Camera 1",
                IpAddress = "192.168.1.10",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/300/200?1"
            };

            var tx2 = new Device
            {
                Name = "Gaming PC",
                IpAddress = "192.168.1.11",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/300/200?2"
            };

            // Receptores
            var rx1 = new Device
            {
                Name = "Screen 1",
                IpAddress = "192.168.1.20",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            var rx2 = new Device
            {
                Name = "Projector",
                IpAddress = "192.168.1.21",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            context.Devices.AddRange(tx1, tx2, rx1, rx2);

            context.SaveChanges();

            // Conexiones actuales
            context.Connections.AddRange(
                new Connection
                {
                    ReceiverId = rx1.Id,
                    TransmitterId = tx1.Id
                },
                new Connection
                {
                    ReceiverId = rx2.Id,
                    TransmitterId = tx2.Id
                }
            );

            context.SaveChanges();
        }
    }
}
