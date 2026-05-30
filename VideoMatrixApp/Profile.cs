namespace VideoMatrixApp
{
    //Le da nombre a ese perfil guardado
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        // Conexiones guardadas en este perfil
        public ICollection<ProfileConnection> Connections { get; set; }
            = new List<ProfileConnection>();
    }
}
