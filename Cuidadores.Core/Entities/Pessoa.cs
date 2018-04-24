using System;
using System.ComponentModel.DataAnnotations;

namespace Cuidadores.Core.Entities
{
    public class Pessoa
    {
        public long Id { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Atualizado { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Display(Name = "RG")]
        public string Rg { get; set; }
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        public string Senha { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required]
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public UF? Uf { get; set; }

        [Display(Name = "Você é: ")]
        public TipoPessoa? TipoPessoa { get; set; }

        public string ComprovanteEnderecoFilename { get; set; }
        public int? ComprovanteEnderecoLenght { get; set; }
        public byte[] ComprovanteEnderecoBytes { get; set; }

    }
}
