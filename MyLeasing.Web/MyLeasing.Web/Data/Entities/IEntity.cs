namespace MyLeasing.Web.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        
        //bool WasDeleted { get; set; } //Por fefeito é falso --> utilizado para o utilizados apagar dados, mas nao apaga da Bd
    }
}
