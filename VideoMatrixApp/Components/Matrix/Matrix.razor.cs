using Microsoft.AspNetCore.Components;
using VideoMatrixApp.Data;
using VideoMatrixApp.Enums;
using VideoMatrixApp.Models;


namespace VideoMatrixApp.Components.Matrix;

public partial class Matrix
{
    [Inject]
    public ApplicationDbContext context { get; set; } = default!;

    private List<Device> receivers = new();
    private List<Device> transmitters = new();
    private List<Connection> connections = new();

    private List<Profile> profiles = new();
    private Device? selectedReceiver;
    private string profileName = "";
    private int selectedProfileId;
    private Device? fullscreenReceiver;

    //Alterna entre pantalla completa y normal, si el receptor ya esta en pantalla completa, lo quita, sino lo pone
    private void ToggleFullscreen(Device receiver)
    {
        if (fullscreenReceiver?.Id == receiver.Id)
            fullscreenReceiver = null;
        else
            fullscreenReceiver = receiver;
    }

    //Al iniciar el componente, llama al método para carga los datos de la base de datos
    protected override void OnInitialized()
    {
        LoadData();
    }

    //carga los datos, los guara en variables
    private void LoadData()
    {
        //LINQ
        //Guarda en la de dispositivos, los receptores, que estén encendidos, .ToList()->Ejecuta la consulta y devuelve una lista con los resultados
        receivers = context.Devices.Where(d => d.DeviceType == DeviceType.Receiver && d.Status == DeviceStatus.On).ToList();

        //Almacena los transmisores, sin importar su estado
        transmitters = context.Devices.Where(d => d.DeviceType == DeviceType.Transmitter).ToList();

        //Hace lo mismo con las conexiones
        connections = context.Connections.ToList();

        //Y con los perfiles
        profiles = context.Profiles.ToList();
    }

    //Para saber el transmisor que tiene asociado un receptor
    private Device? GetTransmitter(Device receiver)
    {
        //Se busca primera conexión del determinado receptor, es decir, el ultimo cambio que ha tenido 
        var connection = connections.FirstOrDefault(c => c.ReceiverId == receiver.Id);

        if (connection == null) // Si no hay conexión, devuelve null
            return null;

        //Una vez con el id de la camara de la conexión, busca el transmisor asociado a ese id y lo devuelve
        return transmitters.FirstOrDefault(t => t.Id == connection.TransmitterId);
    }

    private void SelectReceiver(Device receiver)
    {
        selectedReceiver = receiver;
    }

    //conecta receptor con transmisor
    private void AssignTransmitter(Device transmitter)
    {
        if (selectedReceiver == null)
            return;

        // Busca conexión existente y luego la borra
        var existingConnection = context.Connections.FirstOrDefault(c => c.ReceiverId == selectedReceiver.Id);

        if (existingConnection != null)
        {
            context.Connections.Remove(existingConnection);
        }

        // Nueva conexión
        var newConnection = new Connection
        {
            ReceiverId = selectedReceiver.Id,
            TransmitterId = transmitter.Id
        };

        context.Connections.Add(newConnection); //añade conexión a la base de datos

        context.SaveChanges();  //guarda

        connections = context.Connections.ToList(); //recarga 
    }

    //guarda perfil
    private void SaveProfile()
    {
        if (string.IsNullOrWhiteSpace(profileName))
            return;

        // Crea perfil
        var profile = new Profile
        {
            Name = profileName
        };

        context.Profiles.Add(profile);  //añade perfil a la base de datos

        context.SaveChanges();  //guarda

        foreach (var connection in connections) //recorre las conexiones
        {
            var profileConnection = new ProfileConnection   //y las va guardando
            {
                ProfileId = profile.Id,
                ReceiverId = connection.ReceiverId,
                TransmitterId = connection.TransmitterId
            };

            context.ProfileConnections.Add(profileConnection);
        }

        context.SaveChanges();

        profiles = context.Profiles.ToList();

        profileName = "";
    }

    private void LoadProfile()
    {
        if (selectedProfileId == 0)
            return;

        // Borra conexiones actuales
        context.Connections.RemoveRange(context.Connections);

        context.SaveChanges();

        // Almacena el perfil seleccionado 
        var profileConnections = context.ProfileConnections.Where(pc => pc.ProfileId == selectedProfileId).ToList();

        foreach (var pc in profileConnections)  //misma logica de antes, recorre las conexiones del perfil seleccionado y las va añadiendo a la tabla de conexiones, para que se refleje en la interfaz
        {
            var connection = new Connection
            {
                ReceiverId = pc.ReceiverId,
                TransmitterId = pc.TransmitterId
            };

            context.Connections.Add(connection);
        }

        context.SaveChanges();

        connections = context.Connections.ToList(); //las pasa a string
    }

    private void DeleteProfile()
    {
        if (selectedProfileId == 0)
            return;

        // Obtenemos el perfil seleccionado 
        var profile = context.Profiles.FirstOrDefault(p => p.Id == selectedProfileId);

        if (profile == null)
            return;

        // Obtenemos sus conexiones relacionadas
        var profileConnections = context.ProfileConnections.Where(pc => pc.ProfileId == selectedProfileId);

        // Las borramos
        context.ProfileConnections.RemoveRange(profileConnections);

        // y borramos el perfil
        context.Profiles.Remove(profile);

        context.SaveChanges();

        // Actualizamos los perfiles para que se vea que ha desaparecdo el que acabamos de borrar
        profiles = context.Profiles.ToList();

        selectedProfileId = 0;
    }


    //transorma el C# en css, los enum los pasa a clases css 
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