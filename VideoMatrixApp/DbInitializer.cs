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
                Name = "Cámara 1",
                IpAddress = "192.168.1.10",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/id/237/300/200"
            };

            var tx2 = new Device
            {
                Name = "Cámara 2",
                IpAddress = "192.168.1.11",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/id/1015/300/200"
            };

            var tx3 = new Device
            {
                Name = "Cámara 3",
                IpAddress = "192.168.1.12",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/id/1016/300/200"
            };

            var tx4 = new Device
            {
                Name = "Cámara 4",
                IpAddress = "192.168.1.13",
                Status = DeviceStatus.Off,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/id/1031/300/200"
            };

            var tx5 = new Device
            {
                Name = "Cámara 5",
                IpAddress = "192.168.1.14",
                Status = DeviceStatus.Standby,
                DeviceType = DeviceType.Transmitter,
                ImageUrl = "https://picsum.photos/id/1024/300/200"
            };

            
            // Receptores
            var rx1 = new Device
            {
                Name = "Pantalla 1",
                IpAddress = "192.168.1.20",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            var rx2 = new Device
            {
                Name = "Pantalla 2",
                IpAddress = "192.168.1.21",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            var rx3 = new Device
            {
                Name = "Pantalla 3",
                IpAddress = "192.168.1.22",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };


            var rx4 = new Device
            {
                Name = "Pantalla 4",
                IpAddress = "192.168.1.23",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            var rx5 = new Device
            {
                Name = "Pantalla 5",
                IpAddress = "192.168.1.24",
                Status = DeviceStatus.On,
                DeviceType = DeviceType.Receiver,
                ImageUrl = ""
            };

            //Guarda los dispositivos en la bd 
            context.Devices.AddRange(tx1, tx2, tx3, tx4, tx5, rx1, rx2, rx3, rx4, rx5);

            context.SaveChanges();

            // Conexiones actuales
            context.Connections.AddRange(
                new Connection      //pantalla 1 - camara 1
                {
                    ReceiverId = rx1.Id,
                    TransmitterId = tx1.Id
                },
                new Connection  
                {
                    ReceiverId = rx2.Id,
                    TransmitterId = tx2.Id
                },
                new Connection  
                {
                    ReceiverId = rx4.Id,
                    TransmitterId = tx1.Id
                },
                new Connection  
                {
                    ReceiverId = rx3.Id,
                    TransmitterId = tx2.Id
                },
                new Connection  
                {
                    ReceiverId = rx5.Id,
                    TransmitterId = tx4.Id
                }
            );

            context.SaveChanges();
        }
    }
}
