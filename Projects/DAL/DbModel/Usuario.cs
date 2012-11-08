namespace DAL.DbModel
{
    public class Usuario
    {
        public virtual int Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Celular { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public virtual string Resposta { get; set; }
    }
}
