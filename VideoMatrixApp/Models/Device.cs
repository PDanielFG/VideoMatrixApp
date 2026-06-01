using VideoMatrixApp.Enums;

namespace VideoMatrixApp.Models
{

    //Atributos de la bd
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string IpAddress { get; set; } = String.Empty;

        public DeviceStatus Status { get; set; }    //Objeto de tipo enum DeviceStatus 0=apagado, 1=standby, 2=encendido

        public DeviceType DeviceType { get; set; }  //Objeto de tipo enum DeviceType 0=transmisor, 1=receptor

        public string ImageUrl { get; set; } = String.Empty;
    }
}