namespace DAL.DbModel
{
    public class Fornecedor
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string Responsavel { get; set; }
        public virtual string RamoAtuacao { get; set; }
        public virtual string Endereco { get; set; } 
        public virtual int Numero { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Cel { get; set; }
        public virtual string Email { get; set; }
        public virtual string Site { get; set; }
    }
}
