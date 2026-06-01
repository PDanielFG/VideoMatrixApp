using Microsoft.AspNetCore.Components;
using VideoMatrixApp.Data;
using VideoMatrixApp.Enums;
using VideoMatrixApp.Models;

namespace VideoMatrixApp.Components.Devices;


public partial class Devices
{
    [Inject]
    public ApplicationDbContext context { get; set; } = default!;

    private List<Device> devices = new();   //Todos los dispositivos que se muestran en la tabla

    //Objeto temporal para crear el dispositivo, es lo que se muestra inicialmente en el formulario
    private Device newDevice = new()
    {
        Name = "",
        IpAddress = "",
        ImageUrl = "",
        DeviceType = DeviceType.Transmitter,
        Status = DeviceStatus.On
    };

    // Para editar 
    private int? editingDeviceId = null;
    private Device editingDevice = new();

    // Carga los dispositivos al iniciar el componente
    protected override void OnInitialized()
    {
        LoadDevices();
    }

    private void LoadDevices()
    {
        devices = context.Devices.ToList();     //LINQ para cargar todos los dispositivos
    }

    // Crear dispositivo
    private void CreateDevice()
    {
        //Evita crear dispositivos sin nombbre
        if (string.IsNullOrWhiteSpace(newDevice.Name))
            return;

        //Aunque lo evite en el frontend, asegurarlo en el backend. Si es un receptor no url de imagen
        if (newDevice.DeviceType == DeviceType.Receiver)
        {
            newDevice.ImageUrl = String.Empty;
        }

        context.Devices.Add(newDevice);     //Agrega el nuevo dispositivo a la base de datos
        context.SaveChanges();      //Guarda los cambios en la base de datos

        LoadDevices();  //Recgarga la lista de dispositivos para mostrar el nuevo dispositivo en la tabla

        //Resetea el formulario para crear un nuevo dispositivo, como hacía en el tutorial
        newDevice = new Device
        {
            Name = "",
            IpAddress = "",
            ImageUrl = "",
            DeviceType = DeviceType.Transmitter,
            Status = DeviceStatus.On
        };
    }

    // Borrar dispositivo
    private void DeleteDevice(int deviceId)
    {
        //Busca el dispositivo en la bd, 
        var device = context.Devices.FirstOrDefault(d => d.Id == deviceId);

        if (device == null)
            return;

        //Busca todas las conexiones de ese dispositivo y las elimina, para luego borrar el dispositivo en si, como haciamos antes 
        var connectionsToDelete = context.Connections.Where(c => c.ReceiverId == deviceId || c.TransmitterId == deviceId);
        context.Connections.RemoveRange(connectionsToDelete);

        //Busca los perfiles relacionados con el dispoitivo y los elimina
        var profileConnectionsToDelete = context.ProfileConnections.Where(pc => pc.ReceiverId == deviceId || pc.TransmitterId == deviceId);
        context.ProfileConnections.RemoveRange(profileConnectionsToDelete);

        context.Devices.Remove(device); //Borra el dispositivo 

        context.SaveChanges();  //Guarda los cambios en la base de datos

        LoadDevices();  //regarla la lista de dispositivos
    }

    // EDIT START
    private void StartEdit(Device device)
    {
        editingDeviceId = device.Id;

        //Crea un objeto temporal para editar
        editingDevice = new Device
        {
            Id = device.Id,
            Name = device.Name,
            IpAddress = device.IpAddress,
            ImageUrl = device.ImageUrl,
            DeviceType = device.DeviceType,
            Status = device.Status
        };
    }

    // Guardar edición
    private void SaveEdit()
    {
        //busca el objeto original que se desea modificar 
        var device = context.Devices.FirstOrDefault(d => d.Id == editingDeviceId);

        if (device == null)
            return;

        //Y modifica los atributos dle objeto original con los del temporal
        device.Name = editingDevice.Name;
        device.IpAddress = editingDevice.IpAddress;
        device.ImageUrl = editingDevice.ImageUrl;
        device.DeviceType = editingDevice.DeviceType;
        device.Status = editingDevice.Status;

        context.SaveChanges();  //Guarda los cambios en la base de datos

        editingDeviceId = null; //Sale del modo edición
        LoadDevices();  //regarla la lista
    }

    // Cancelar edición
    private void CancelEdit()
    {
        editingDeviceId = null;
    }

    //Convierte un valor enum de C# en una clase CSS
    private string GetStatusClass(DeviceStatus status)
    {
        return status switch
        {
            DeviceStatus.On => "status-on",
            DeviceStatus.Off => "status-off",
            DeviceStatus.Standby => "status-standby",
            _ => ""
        };
    }
}